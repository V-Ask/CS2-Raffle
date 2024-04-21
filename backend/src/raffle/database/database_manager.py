from sqlite3 import connect, Connection, Error
from raffle.maps.utils.map_pool import Map
from warnings import warn

def create_conn(path):
    if path == '':
        warn('Database Path Missing. Please fill out information in config.ini.')
        return None
    return connect(path)

def create_maps_table(conn: Connection):
    if conn is None:
        return
    sql_create_table = '''CREATE TABLE IF NOT EXISTS maps (
                            workshop_id text PRIMARY KEY,
                            name text,
                            image_url text,
                            weight integer NOT NULL
                        );'''
    try:
        cursor = conn.cursor()
        cursor.execute(sql_create_table)
    except Error as e:
        warn(e)

def add_map(conn: Connection, map: Map):
    sql_add_map = '''INSERT INTO maps(workshop_id, name, image_url, weight)
                VALUES(?,?,?,?)'''
    try:
        cursor = conn.cursor()
        cursor.execute(sql_add_map, (map.id, map.name, map.image_url, map.weight))
    except Error as e:
        warn(e)