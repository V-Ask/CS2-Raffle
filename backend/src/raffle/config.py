from configparser import ConfigParser
import os

class Config:
    def __init__(self) -> None:
        config = ConfigParser()
        if not os.path.exists('config.ini'):
            print(f'No Config-file on disc, please fill out configs for proper server management')
            config['Host Settings'] = {
                'username': '',
                'password': '',
                'start_server_url': '',
                'set_workshop_map_url': '',
                'get_server_url': ''
            }
            config.write(open('config.ini', 'w'))

        config.read('config.ini')
        self.username = config.get('Host Settings', 'username')
        self.password = config.get('Host Settings', 'password')
        self.start_server_url = config.get('Host Settings', 'start_server_url')
        self.set_workshop_map_url = config.get('Host Settings', 'set_workshop_map_url')
        self.get_server_url = config.get('Host Settings', 'get_server_url')