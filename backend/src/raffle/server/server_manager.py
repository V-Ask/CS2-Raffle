import requests
import warnings

class ServerManager:
    def __init__(self, username, password, put_server_url, get_server_url) -> None:
        self.authentication = (username, password)
        self.put_server_url = put_server_url
        self.get_server_url = get_server_url

    def get_server_conn(self):
        if self.get_server_url == '':
            warnings.warn(f'No GET Server URL provided. Please fill out your configs.')
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
        if self.put_server_url == '':
            warnings.warn(f'No PUT Server URL provided. Please fill out your configs.')
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
        return resp
    
def create_server_manager(config):
    return ServerManager(config.username, config.password, 
                           config.put_server_url, config.get_server_url)