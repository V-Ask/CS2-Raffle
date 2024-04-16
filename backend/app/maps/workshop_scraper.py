import requests
from bs4 import BeautifulSoup
from re import compile, search

WORKSHOP_PATTERN = compile(r'https://steamcommunity\.com/sharedfiles/filedetails/\?id=(\d+)')
WORKSHOP_IMAGE_URL_CLASS = 'image_src'
WORKSHOP_NAME_CLASS = 'workshopItemTitle'

class WorkshopItem:
    def __init__(self, name: str, id: str, image_url: str) -> None:
        self.name = name
        self.id = id
        self.image_url = image_url
    
    def __str__(self) -> str:
        return f'{self.name}, {self.id}, {self.image_url}'

def scrape_workshop_page(url: str):
    source = requests.get(url).content
    soup = BeautifulSoup(source, 'lxml')
    name = soup.find('div', attrs={'class': WORKSHOP_NAME_CLASS}).text.strip()
    image_url = soup.find('link', attrs={'rel': WORKSHOP_IMAGE_URL_CLASS})['href']
    return (name, image_url)


def get_workshop_item(url: str):
    matches = search(WORKSHOP_PATTERN, url)
    if matches is None:
        return None
    id = matches.group(0)[-10:]
    name, image_url = scrape_workshop_page(url)
    return WorkshopItem(name, id, image_url)
    
    