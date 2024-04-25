from flask import Flask, jsonify, request
from flask_cors import CORS
from raffle.maps.map_manager import MapManager
from raffle.server.server_manager import create_server_manager
from raffle.config import Config

PUT_TEST = False
GET_TEST = False

config = Config('config.ini')

app = Flask(__name__)

CORS(app, resources={r'/*': {'origins': '*'}})
app.config['CORS_HEADERS'] = 'Content-Type'

map_manager = MapManager(config.database_path)
server_manager = create_server_manager(config)

def add_routes(app):
    @app.route('/test')
    def test():
        return {'hello': request.get_json()['world']}

    @app.route('/submitmap', methods=['POST'])
    def submit_map():
        request_data = request.get_json()
        url = request_data['workshop_url']
        map_manager.add_map(url)
        return "200"

    @app.route('/allmaps', methods=['GET'])
    def get_all_maps():
        maps = map_manager.get_maps()
        print(maps)
        return jsonify(maps)

    @app.route('/randommaps', methods=['GET'])
    def get_random_map_pool():
        length = request.get_json()['reel_length']
        reel = map_manager.generate_reel(length)
        return jsonify(reel)

    @app.route('/startmap', methods=['PUT'])
    def start_map():
        id = request.get_json()['workshop_id']
        map_manager.play_map(id)
        server_manager.set_map(id)
        return "200"

add_routes(app)
if __name__ == '__main__':
    app.run(debug=True)
