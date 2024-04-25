import pytest
from sqlite3 import connect
from raffle import database_manager

@pytest.fixture
def path():
    return 'tests/database_tests/test_database_with_table.db'

@pytest.fixture(autouse=True)
def database(path):
    database = database_manager.Database(path)
    return database

@pytest.fixture(autouse=True)
def reset_db(path: str):
    connection = connect(path)
    cursor = connection.cursor()
    rows_sql = '''SELECT count(*) FROM sqlite_master WHERE type="table"
                     AND name="maps";'''
    length = cursor.execute(rows_sql).fetchone()
    if length[0] > 0:
        delete_all_sql = '''DELETE FROM maps;'''
        cursor.execute(delete_all_sql)
    cursor.close()
    connection.commit()
    connection.close()