from raffle.config import Config
import requests
from abc import ABC, abstractmethod
import sys

config = Config()

class __AbstractServerManager(ABC):
    @abstractmethod
    def start_server(self):
        pass

    @abstractmethod
    def set_map(self):
        pass

class __EmptyServerManager(__AbstractServerManager):
    def __init__(self, missing_properties) -> None:
        self.err_msg = f'Missing Properties "{missing_properties}"'

    def __report_err(self):
        print(self.err_msg, file=sys.stderr)

    def start_server(self):
        self.__report_err()

    def set_map(self):
        self.__report_err()

    def get_server_ip(self):
        self.__report_err()
    

class __ServerManager(__AbstractServerManager):
    def __init__(self, username, password, start_serv_url, set_map_url, get_server_url) -> None:
        self.authentication = (username, password)
        self.start_serv_url = start_serv_url
        self.set_map_url = set_map_url
        self.get_server_url = get_server_url

    def get_server_ip(self):
        resp = requests.get(
            self.get_server_url,
            auth=(self.authentication),
        )
        if resp.status_code != 200:
            print(f'Error: Received status code {resp.status_code} from {self.get_server_url}.', file=sys.stderr)
            return
        output = resp.json()
        return ('%s:%s' % output['ip'], output['ports']['game'])
    
def create_server_manager():
    config_dict = config.__dict__
    missing_properties = []
    for field, value in config_dict:
        if value == '':
            missing_properties.append(field)
    if missing_properties:
        print(f'Missing Dathost Server Properties "{missing_properties}". Please fill these out in your "config.ini".')
        return __EmptyServerManager(missing_properties)
    return __ServerManager(config.username, config.password, config.start_server_url, 
                           config.set_workshop_map_url, config.get_server_url)