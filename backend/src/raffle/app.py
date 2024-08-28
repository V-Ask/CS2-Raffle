from datetime import timedelta, datetime, timezone

from flask import Flask, jsonify, request
from flask_cors import CORS
from flask_jwt_extended import jwt_required, create_access_token, JWTManager, get_jwt, get_jwt_identity, set_access_cookies
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
app.config['JWT_ACCESS_TOKEN_EXPIRES'] = timedelta(hours=1)

jwt = JWTManager(app)

map_manager = MapManager(config.database_path)
server_manager = create_server_manager(config)

@app.after_request
def refresh_jwt(response):
    try:
        exp_timestamp = get_jwt()['exp']
        now = datetime.now(timezone.utc)
        target_timestamp = datetime.timestamp(now + timedelta(minutes=30))
        if target_timestamp > exp_timestamp:
            access_token  = create_access_token(identity=get_jwt_identity())
            set_access_cookies(response, access_token)
        return response
    except (RuntimeError, KeyError):
        return response

def add_routes(app):
    @app.route('/test')
    def hello():
        return "hi", 201

    @app.route('/')
    def test():
        return "What are you doing here??"

    @app.route('/submitmap', methods=['POST'])
    @jwt_required()
    def submit_map():
        url = request.get_json()['data']['workshop_url']
        map = map_manager.add_map(url)
        return jsonify(map.map_dict())

    @app.route('/nonplayed', methods=['GET'])
    @jwt_required()
    def get_nonplayed():
        maps = map_manager.get_nonplayed()
        map_dict = {map.id:map.map_dict() for map in maps}
        return jsonify(map_dict)

    @app.route('/played', methods=['GET'])
    @jwt_required()
    def get_played():
        maps = map_manager.get_played()
        map_dict = {map.id:map.map_dict() for map in maps}
        return jsonify(map_dict)

    @app.route('/startmap', methods=['POST'])
    @jwt_required()
    def start_map():
        id = request.get_json()['data']['workshop_id']
        resp = server_manager.set_map(id)
        return jsonify(resp.text), resp.status_code
    
    @app.route('/deletemap', methods=['DELETE'])
    @jwt_required()
    def del_map():
        id = request.get_json()['workshop_id']
        played = request.get_json()['played']
        map_manager.del_map(played, id)
        return jsonify(), 201
    
    @app.route('/playmap', methods=['PUT'])
    @jwt_required()
    def play_map():
        id = request.get_json()['data']['workshop_id']
        map_manager.play_map(id)
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
