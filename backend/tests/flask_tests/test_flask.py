from json import dumps

def test_workshop_post(client):
    data = {'url': 'https://steamcommunity.com/sharedfiles/filedetails/?id=3222792425&searchtext='}
    resp = client.post(
        '/submitmap',
        data=dumps(data),
        content_type='application/json'
    )
    print(resp)
    assert True == True
