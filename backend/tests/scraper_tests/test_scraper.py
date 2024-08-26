def test_not_none(scrape):
    assert not scrape is None

def test_scrape_name(scrape):
    expected = 'de_shoji'
    assert expected == scrape.name

def test_scrape_id(scrape):
    expected = '3222792425'
    assert expected == scrape.id

def test_scrape_image_url(scrape):
    expected = 'https://steamuserimages-a.akamaihd.net/ugc/2500135638377138712/E15B291D84A867ECC76B1375DF963FE786D7B80B/?imw=512&&ima=fit&impolicy=Letterbox&imcolor=%23000000&letterbox=false'
    assert expected == scrape.image_url

def test_bad_scrape(bad_scrape):
    assert bad_scrape is None