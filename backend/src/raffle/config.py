from configparser import ConfigParser
import os

class Config:
    def __init__(self) -> None:
        config = ConfigParser()
        if not os.path.exists('config.ini'):
            print(f'No Config-file on disc, please fill out configs for proper server management')
            config['Host Settings'] = {
                'api_url': '',
                'username': '',
                'password': ''
            }
            config.write(open('config.ini', 'w'))

        config.read('config.ini')
        self.api_url = config.get('Host Settings', 'api_url')
        self.username = config.get('Host Settings', 'username')
        self.password = config.get('Host Settings', 'password')