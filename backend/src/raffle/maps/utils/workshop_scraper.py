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
    """
    Scrapes a Workshop page and returns the name and thumbnail URL of the item.
    
    Args:
        url (str): The URL of the Workshop item.

    Returns:
        (str, str): A pair of the name and image URL.
    """
    source = requests.get(url).content
    soup = BeautifulSoup(source, 'lxml')
    name_div = soup.find('div', attrs={'class': WORKSHOP_NAME_CLASS})
    name = "None" if name_div is None else name_div.text.strip()
    link_div = soup.find('link', attrs={'rel': WORKSHOP_IMAGE_URL_CLASS})
    image_url = "" if link_div is None else link_div['href']
    return (name, image_url)

def get_workshop_item(url: str):
    """
    Extracts the name, ID, and thumbnail URL from a Steam Workshop URL.

    Args:
        url (str): The URL of the Workshop item.

    Returns:
        WorkshopItem: The information given by url.
    """
    matches = search(WORKSHOP_PATTERN, url)
    if matches is None:
        return None
    id = matches.group(0)[-10:]
    name, image_url = scrape_workshop_page(url)
    return WorkshopItem(name, id, image_url)
    
    