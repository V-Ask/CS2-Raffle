from raffle.app import PUT_TEST, GET_TEST

def test_put(manager):
    if not PUT_TEST:
        return
    resp_code = manager.set_map('3221491619')
    assert resp_code == 200

def test_get(manager):
    if not GET_TEST:
        return
    resp_code, ip = manager.get_server_conn()
    assert resp_code == 200 and ip != 'null'