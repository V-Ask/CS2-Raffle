from configparser import ConfigParser
import os

class Config:
    def __init__(self, name) -> None:
        config = ConfigParser()
        if not os.path.exists(name):
            print(f'No Config-file on disc, please fill out configs for proper server management')
            config['Host Settings'] = {
                'username': '',
                'password': '',
                'put_server_url': '',
                'get_server_url': ''

            }
            config.write(open(name, 'w'))

        config.read(name)
        self.username = config.get('Host Settings', 'username')
        self.password = config.get('Host Settings', 'password')
        self.put_server_url = config.get('Host Settings', 'put_server_url')
        self.get_server_url = config.get('Host Settings', 'get_server_url')