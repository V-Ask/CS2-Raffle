from raffle import server_manager
import pytest
from raffle.config import config

@pytest.fixture
def manager():
    return server_manager.create_server_manager(config)