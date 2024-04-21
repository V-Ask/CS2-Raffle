from raffle import database_manager

def test_create_tables(connection):
    cursor = connection.cursor()
    sql = '''SELECT name FROM sqlite_master WHERE
                type="table" AND name="maps"'''
    database_manager.create_maps_table(connection)
    tables = cursor.execute(sql).fetchall()
    assert not tables is None
