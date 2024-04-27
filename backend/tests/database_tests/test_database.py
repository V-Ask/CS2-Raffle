import pytest
from sqlite3 import connect
from raffle.maps.utils import map_pool

test_map = map_pool.Map('TestMap', '0123456789', 'www.test.somewhere', -1)

def test_create_tables(path):
    connection = connect(path)
    cursor = connection.cursor()
    sql = '''SELECT name FROM sqlite_master WHERE
                type="table" AND name="maps";'''
    tables = cursor.execute(sql).fetchall()
    cursor.close()
    connection.close()
    assert not tables is None

def test_insert(path, database):
    connection = connect(path)
    database.add_map(test_map.name, test_map.id, test_map.image_url, test_map.weight)
    cursor = connection.cursor()
    sql = '''SELECT * FROM maps WHERE
          workshop_id = ?;
                '''
    value = cursor.execute(sql, (test_map.id,)).fetchone()
    cursor.close()
    connection.close()
    (name, id, url, weight) = value
    assert id == test_map.id
    assert name == test_map.name
    assert url == test_map.image_url
    assert weight == test_map.weight

def test_delete(path, database):
    connection = connect(path)
    database.add_map(test_map.name, test_map.id, test_map.image_url, test_map.weight)
    database.remove_map(test_map.name, test_map.id, test_map.image_url, test_map.weight)
    cursor = connection.cursor()
    sql = '''SELECT count(*) FROM maps WHERE workshop_id = ?'''
    length = cursor.execute(sql, (test_map.id, )).fetchone()
    cursor.close()
    assert length[0] <= 0

def test_get_maps(path, database):
    connection = connect(path)
    test_maps = [
    map_pool.Map('TestMap', '1', 'www.test.somewhere', -1),
    map_pool.Map('TestMap', '2', 'www.test.somewhere', -1),
    map_pool.Map('TestMap', '3', 'www.test.somewhere', -1),
    map_pool.Map('TestMap', '4', 'www.test.somewhere', -1),
    map_pool.Map('TestMap', '5', 'www.test.somewhere', -1)
    ]
    [database.add_map(map.name, map.id, map.image_url, map.weight) for map in test_maps]
    cursor = connection.cursor()
    sql = '''SELECT count(*) FROM maps'''
    length = cursor.execute(sql).fetchone()
    cursor.close()
    connection.close()
    assert length[0] == len(test_maps)

@pytest.mark.filterwarnings('ignore::UserWarning')
def test_eliminate_dupes(path, database):
    connection = connect(path)
    dupe_map = map_pool.Map('ThisIsADuplicateId', '0123456789', 'www.test.somewhere', -1)
    database.add_map(test_map.name, test_map.id, test_map.image_url, test_map.weight)
    database.add_map(dupe_map.name, dupe_map.id, dupe_map.image_url, dupe_map.weight)
    cursor = connection.cursor()
    sql = '''SELECT count(*) FROM maps'''
    value = cursor.execute(sql).fetchone()
    cursor.close()
    connection.close()
    assert value[0] == 1
    
