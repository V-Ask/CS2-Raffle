from flask import Flask
from flask_cors import CORS, cross_origin
from map_pool import MapPool

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
