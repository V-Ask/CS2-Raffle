import pytest
from raffle.maps.workshop_scraper import get_workshop_item

@pytest.fixture
def scrape():
    return get_workshop_item('https://steamcommunity.com/sharedfiles/filedetails/?id=3222792425&searchtext=')

@pytest.fixture
def bad_scrape():
    return get_workshop_item('www.google.com')