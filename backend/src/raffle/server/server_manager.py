import requests
from abc import ABC, abstractmethod
import warnings


class __AbstractServerManager(ABC):
    @abstractmethod
    def get_server_conn(self):
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

    def get_server_conn(self):
        self.__report_err()
        return ''
    

class ServerManager(__AbstractServerManager):
    def __init__(self, username, password, put_server_url, get_server_url) -> None:
        self.authentication = (username, password)
        self.put_server_url = put_server_url
        self.get_server_url = get_server_url

    def get_server_conn(self):
        resp = requests.get(
            self.get_server_url,
            auth=self.authentication,
        )
        if resp.status_code != 200:
            warnings.warn(f'Error: Received status code {resp.status_code} from {self.get_server_url}:\n\t {resp.text}')
            return
        output = resp.json()
        return resp.status_code, ('connect %s:%s; password %s' % (output['ip'], output['ports']['game'], output['cs2_settings']['password']))
    
    def set_map(self, workshop_id):
        headers = {
                "Accept": "application/json",
                "Content-Type": "application/x-www-form-urlencoded"
            }
        data = f"cs2_settings.maps_source=workshop_single_map&autostop=false&csgo_settings.workshop_id={workshop_id}&cs2_settings.workshop_single_map_id={workshop_id}"
        resp = requests.put(
            self.put_server_url, 
            data=data, 
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