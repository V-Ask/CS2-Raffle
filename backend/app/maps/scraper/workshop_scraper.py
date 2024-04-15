import scrapy

class WorkshopMap(scrapy.Item):
    name = scrapy.Field()
    image_url = scrapy.Field()

class WorkshopScraper(scrapy.Spider):
    name = "lucky_wheel"
    allowed_domains = ["steamcommunity.com"]
    start_urls = []

    myBaseUrl = ''
    def __init__(self, category='', **kwargs):
        self.myBaseUrl = category
        self.start_urls.append(self.myBaseUrl)
        super().__init__(**kwargs)
    
    custom_settings = {'FEED_URI': 'scraper_output/output.json', 'CLOSESPIDER_TIMEOUT': 15}

    def parse(self, response):
        name = response.xpath('//div[@class="workshopItemTitle"]/text()').get()
        image_url = response.xpath('//img[@id="previewImageMain"][@class="workshopItemPreviewImageMain"]/@src').get()
        yield WorkshopMap(name=name, image_url=image_url)
