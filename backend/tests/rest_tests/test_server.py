from raffle.server.server_manager import EmptyServerManager
from raffle.app import PUT_TEST, GET_TEST

def test_put(manager):
    if isinstance(manager, EmptyServerManager) or not PUT_TEST:
        return
    resp_code = manager.set_map('3221491619')
    assert resp_code == 200

def test_get(manager):
    if isinstance(manager, EmptyServerManager) or not GET_TEST:
        return
    resp_code, ip = manager.get_server_conn()
    assert resp_code == 200 and ip != 'null'