from raffle import server_manager
import pytest
from raffle.config import Config

@pytest.fixture
def config():
    return Config(r'tests/rest_tests/rest_config.ini')

@pytest.fixture
def manager(config):
    return server_manager.create_server_manager(config)