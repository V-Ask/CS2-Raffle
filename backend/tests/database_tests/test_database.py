from raffle import database_manager

def test_test():
    a = database_manager.create_conn()
    assert a == a

