from raffle.server.server_manager import ServerManager, EmptyServerManager

def test_put(manager):
    print(manager)
    if isinstance(manager, EmptyServerManager):
        return
    resp_code = manager.set_map('3224424564')
    assert resp_code == 200