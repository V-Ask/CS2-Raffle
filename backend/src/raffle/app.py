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
        return {'hello': request.args.get('world')}

    @app.route('/submitmap', methods=['POST'])
    def submit_map():
        request_data = request.get_json()
        url = request_data['workshop_url']
        map_manager.add_map(url)
        return 200

    @app.route('/nonplayed', methods=['GET'])
    def get_nonplayed():
        maps = map_manager.get_nonplayed()
        map_dict = [map.map_dict() for map in maps]
        return jsonify(map_dict)

    @app.route('/played', methods=['GET'])
    def get_played():
        maps = map_manager.get_played()
        map_dict = [map.map_dict() for map in maps]
        return jsonify(map_dict)

    @app.route('/randommaps', methods=['GET'])
    def get_random_map_pool():
        length = request.get_json()['reel_length']
        reel = map_manager.generate_reel(length)
        reel_dict = [map.map_dict() for map in reel]
        return jsonify(reel_dict)

    @app.route('/startmap', methods=['PUT'])
    def start_map():
        id = request.get_json()['workshop_id']
        map_manager.play_map(id)
        server_manager.set_map(id)
        return 200
    
    @app.route('/removemap', methods=['DELETE'])
    def remove_map():
        id = request.get_json()['workshop_id']
        map_manager.remove_map(id)
        return 200

add_routes(app)
if __name__ == '__main__':
    app.run(debug=True)
