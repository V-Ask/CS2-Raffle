from sqlite3 import Error, connect
from warnings import warn

class Database:
    
    def create_maps_table(self):
        if self.database_path is None:
            return
        conn = connect(self.database_path)
        sql_create_table = '''CREATE TABLE IF NOT EXISTS maps (
                                name text,
                                workshop_id text PRIMARY KEY,
                                image_url text,
                                weight integer NOT NULL
                            );'''
        sql_create_played_table = '''CREATE TABLE IF NOT EXISTS played_maps (
                                name text,
                                workshop_id text PRIMARY KEY,
                                image_url text,
                                weight integer NOT NULL
                            );'''
        cursor = conn.cursor()
        try:
            cursor.execute(sql_create_table)
            cursor.execute(sql_create_played_table)
        except Error as e:
            warn(str(e))
        cursor.close()
        conn.commit()
        conn.close()

    def add_map(self, name, id, url, weight):
        sql_add_map = '''INSERT INTO maps(name, workshop_id, image_url, weight)
                    VALUES(?,?,?,?);'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            data_tuple = (name, id, url, weight)
            cursor.execute(sql_add_map, data_tuple)
        except Error as e:
            warn(str(e))
        cursor.close()
        conn.commit()
        conn.close()
    
    def get_maps(self, map_table: str):
        sql_get_map = f'''SELECT * FROM {map_table};'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        values = []
        try:
            values = cursor.execute(sql_get_map).fetchall()
        except Error as e:
            warn(str(e))
        cursor.close()
        conn.close()
        return values

    def get_nonplayed_maps(self):
        return self.get_maps('maps')

    def get_played_maps(self):
        return self.get_maps('played_maps')

    def del_map(self, played: bool, id: str):
        pool = 'played_maps' if played else 'maps'
        sql_delete_map = f'''DELETE FROM {pool} WHERE workshop_id=?'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            cursor.execute(sql_delete_map, (id, ))
            conn.commit()
        except Error as e:
            warn(str(e))
        cursor.close()
        conn.close()
        
    def play_map(self, name, id, url, weight):
        self.del_map(False, id)
        sql_add_played = '''INSERT INTO played_maps(name, workshop_id, image_url, weight)
                    VALUES(?,?,?,?);'''
        sql_increase_weights = '''UPDATE maps SET weight = weight + 1'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            cursor.execute(sql_add_played, (name, id, url, weight))
            cursor.execute(sql_increase_weights)
            conn.commit()
        except Error as e:
            warn(str(e))
        cursor.close()
        conn.close()

    def unplay_map(self, name, id, url, weight):
        self.del_map(True, id)
        self.add_map(name, id, url, weight)
    
    def __init__(self, database_path) -> None:
        self.database_path = database_path
        self.create_maps_table()




