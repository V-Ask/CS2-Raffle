import scrapy

class WorkshopMap(scrapy.Item):
    name = scrapy.Field()
    image_url = scrapy.Field()
    id = scrapy.Field()

class LuckyWheelSpider(scrapy.Spider):
    name = "lucky_wheel"
    allowed_domains = ["steamcommunity.com"]
    start_urls = ["https://steamcommunity.com/sharedfiles/filedetails/?id=3220951917&searchtext="]

    def parse(self, response):
        name = response.xpath('//div[@class="workshopItemTitle"]/text()').get()
        image_url = response.xpath('//img[@id="previewImageMain"][@class="workshopItemPreviewImageMain"]/@src').get()
        print(image_url)
