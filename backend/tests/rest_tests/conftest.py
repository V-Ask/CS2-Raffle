from raffle import server_manager
import pytest
from raffle.config import Config

@pytest.fixture
def manager():
    return server_manager.create_server_manager(Config('config.ini'))