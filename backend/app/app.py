from flask import Flask
from flask_cors import CORS, cross_origin
from map_pool import MapPool
import configparser


config = configparser.ConfigParser()

config.read('config.ini')

def create_app():
    app = Flask(__name__)
    Cors = CORS(app)

    CORS(app, resources={r'/*': {'origins': '*'}})
    app.config['CORS_HEADERS'] = 'Content-Type'

    @app.route('/submitmap', methods=['POST'])
    def submit_map():
        return '1'
    
    @app.route('/randommaps', methods=['GET'])
    def get_random_map_pool():
        return '2'
    
    @app.route('/startmap', methods=['GET'])
    def start_map():
        return '3'

    return app

@crochet.run_in_reactor
def scrape_with_crochet(baseURL):
    dispatcher.connect(_crawler)

async def start_server(map_id):
    if map_id == "":
        return jsonify({'error': 'Invalid Map ID'}), 400

    url = "https://dathost.net/api/0.1/game-servers/647896ef0407c5f75bcc0ce8"
    userpass = base64.b64encode((config.get('Dathost Settings', 'email') + config.get('Dathost Settings', 'password')).encode('ascii')).decode('ascii')
    headers = {
        "Authorization": f"Basic {userpass}",
        "accept": "application/json",
        "Content-Type": "application/x-www-form-urlencoded"
    }
    data = f"autostop=false&csgo_settings.workshop_id={map_id}&cs2_settings.workshop_single_map_id={map_id}"
    
    async with aiohttp.ClientSession() as session:
        async with session.put(url, headers=headers, data=data) as response:
            if response.status == 200:
                return jsonify({'status': 'success'}), 200
            else:
                response_text = await response.text()
                return jsonify({'error': 'Failed to start server', 'message': response_text}), response.status
