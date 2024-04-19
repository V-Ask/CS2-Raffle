import sqlite3

def create_conn():
    conn = sqlite3.connect('test.db')
    return conn