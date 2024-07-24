from flask import Flask, jsonify, request
from flask_cors import CORS
from flask_jwt_extended import jwt_required, create_access_token, JWTManager
from raffle.maps.map_manager import MapManager
from raffle.server.server_manager import create_server_manager
from raffle.config import Config

PUT_TEST = False
GET_TEST = False

config = Config('config.ini')

app = Flask(__name__)

CORS(app, resources={r'/*': {'origins': '*'}})
app.config['CORS_HEADERS'] = 'Content-Type'

app.config['JWT_SECRET_KEY'] = config.jwt_secret
jwt = JWTManager(app)

map_manager = MapManager(config.database_path)
server_manager = create_server_manager(config)

def add_routes(app):

    @app.route('/submitmap', methods=['POST'])
    @jwt_required()
    def submit_map():
        url = request.get_json()['data']['workshop_url']
        valid = map_manager.add_map(url)
        return "VALID" if valid else "INVALID"

    @app.route('/nonplayed', methods=['GET'])
    @jwt_required()
    def get_nonplayed():
        maps = map_manager.get_nonplayed()
        map_dict = [map.map_dict() for map in maps]
        return jsonify(map_dict)

    @app.route('/played', methods=['GET'])
    @jwt_required()
    def get_played():
        maps = map_manager.get_played()
        map_dict = [map.map_dict() for map in maps]
        return jsonify(map_dict)

    @app.route('/startmap', methods=['PUT'])
    @jwt_required()
    def start_map():
        id = request.get_json()['workshop_id']
        remove = request.get_json()['remove']
        if remove:
            map_manager.remove_map(id)
        map_manager.play_map(id)
        server_manager.set_map(id)
        return jsonify(), 201
    
    @app.route('/removemap', methods=['DELETE'])
    @jwt_required()
    def remove_map():
        id = request.get_json()['workshop_id']
        map_manager.remove_map(id)
        return jsonify(), 201
    
    @app.route('/unplaymap', methods=['PUT'])
    @jwt_required()
    def unplay_map():
        id = request.get_json()['data']['workshop_id']
        map_manager.unplay_map(id)
        return jsonify(), 201
    
    @app.route('/auth', methods=['GET'])
    @jwt_required()
    def auth():
        return jsonify(), 201
    
    @app.route('/login', methods=['POST'])
    def login():
        password = request.json['password']

        if not password:
            return jsonify({'msg': 'Missing password parameter'}), 400
        
        if password != config.server_password:
            return jsonify({'success': False, 'msg': 'Access Denied'}), 401
        
        access_token = create_access_token(identity="user")
        return jsonify({'success': True, 'token': access_token}), 200

add_routes(app)

if __name__ == '__main__':
    app.run(debug=True)
