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
            config['Database Settings'] = {
                'path': ''
            }
            config['Server Settings'] = {
                'server_password': ''
            }
            config.write(open(name, 'w'))

        config.read(name)
        self.username = config.get('Host Settings', 'username', fallback='')
        self.password = config.get('Host Settings', 'password', fallback='')
        self.put_server_url = config.get('Host Settings', 'put_server_url', fallback='')
        self.get_server_url = config.get('Host Settings', 'get_server_url', fallback='')
        self.database_path = config.get('Database Settings', 'path', fallback='')
        self.server_password = config.get('Server Settings', 'server_password', fallback='')