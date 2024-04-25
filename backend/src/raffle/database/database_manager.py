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
        cursor = conn.cursor()
        try:
            cursor.execute(sql_create_table)
        except Error as e:
            warn(e)
        cursor.close()
        conn.commit()
        conn.close()

    def add_map(self, name_, name, url, weight):
        sql_add_map = '''INSERT INTO maps(name, workshop_id, image_url, weight)
                    VALUES(?,?,?,?);'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            data_tuple = (name_, name, url, weight)
            cursor.execute(sql_add_map, data_tuple)
        except Error as e:
            warn(e)
        cursor.close()
        conn.commit()
        conn.close()

    def get_maps(self):
        sql_get_map = '''SELECT * FROM maps;'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        values = []
        try:
            values = cursor.execute(sql_get_map).fetchall()
        except Error as e:
            warn(e)
        cursor.close()
        conn.close()
        return values
        
    def remove_map(self, map_id):
        sql_delete_map = '''DELETE FROM maps WHERE workshop_id=?'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            cursor.execute(sql_delete_map, (map_id, ))
            conn.commit()
        except Error as e:
            warn(e)
        cursor.close()
        conn.close()

    def increase_all_other_weights(self, excluded: str):
        sql_increase_weights = '''UPDATE maps SET weight = weight + 1 WHERE
                               workshop_id != ?;'''
        conn = connect(self.database_path)
        cursor = conn.cursor()
        try:
            cursor.execute(sql_increase_weights, (excluded, ))
            conn.commit()
        except Error as e:
            warn(e)
        cursor.close()
        conn.close()
    
    def __init__(self, database_path) -> None:
        print(database_path)
        self.database_path = database_path
        self.create_maps_table()




