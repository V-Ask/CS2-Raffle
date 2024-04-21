import pytest
import os
from raffle import database_manager

@pytest.fixture
def connection():
    if os.path.exists('test_database.db'):
        os.remove('tests/database_tests/test_database.db')
    return database_manager.create_conn('tests/database_tests/test_database.db')

@pytest.fixture
def tabled_connection():
    if os.path.exists('test_database_with_table.db'):
        os.remove('tests/database_tests/test_database_with_table.db')
    conn = database_manager.create_conn('tests/database_tests/test_database.db')
    database_manager.create_maps_table(conn)
    return conn