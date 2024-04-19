import requests
from abc import ABC, abstractmethod
import warnings


class __AbstractServerManager(ABC):
    @abstractmethod
    def get_server_ip(self):
        pass

    @abstractmethod
    def set_map(self, workshop_id):
        pass

class EmptyServerManager(__AbstractServerManager):
    def __init__(self, missing_properties) -> None:
        self.err_msg = f'Missing Properties "{missing_properties}"'

    def __report_err(self):
        warnings.warn(self.err_msg)

    def set_map(self, _):
        self.__report_err()

    def get_server_ip(self):
        self.__report_err()
        return ''
    

class ServerManager(__AbstractServerManager):
    def __init__(self, username, password, put_server_url, get_server_url) -> None:
        self.authentication = (username, password)
        self.put_server_url = put_server_url
        self.get_server_url = get_server_url

    def get_server_ip(self):
        resp = requests.get(
            self.get_server_url,
            auth=self.authentication,
        )
        if resp.status_code != 200:
            warnings.warn(f'Error: Received status code {resp.status_code} from {self.get_server_url}:\n\t {resp.text}')
            return
        output = resp.json()
        return ('%s:%s' % output['ip'], output['ports']['game'])
    
    def set_map(self, workshop_id):
        payload = payload = "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"csgo_settings.maps_source\"\r\n\r\nworkshop_single_map\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"csgo_settings.workshop_start_map_id\"\r\n\r\n%s\r\n-----011000010111000001101001--" % workshop_id
        headers = {"content-type": "multipart/form-data; boundary=---011000010111000001101001"}
        resp = requests.put(
            self.put_server_url, 
            data=payload, 
            headers=headers,
            auth=self.authentication)
        if resp.status_code != 200:
            warnings.warn(f'Error: Received status code {resp.status_code} from {self.get_server_url}:\n\t {resp.text}')
            return resp.status_code
        return resp.status_code
    
def create_server_manager(config) -> __AbstractServerManager:
    config_dict = config.__dict__
    missing_properties = []
    for field, value in config_dict.items():
        if value == '':
            missing_properties.append(field)
    if missing_properties:
        warnings.warn(f'Missing Dathost Server Properties "{missing_properties}". Please fill these out in your "config.ini".')
        return EmptyServerManager(missing_properties)
    return ServerManager(config.username, config.password, 
                           config.put_server_url, config.get_server_url)