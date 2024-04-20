from flask import Flask, jsonify, request
from flask_cors import CORS
import configparser

PUT_TEST = False
GET_TEST = False

config = configparser.ConfigParser()

config.read('config.ini')

app = Flask(__name__)

CORS(app, resources={r'/*': {'origins': '*'}})
app.config['CORS_HEADERS'] = 'Content-Type'

def add_routes(app):
    @app.route('/test')
    def test():
        return jsonify({'hello': 'world'})

    @app.route('/submitmap', methods=['POST'])
    def submit_map():
        request_data = request.get_json()
        url = request_data['url']
        return request_data

    @app.route('/randommaps', methods=['GET'])
    def get_random_map_pool():
        return '2'

    @app.route('/startmap', methods=['GET'])
    def start_map():
        return '3'
    
add_routes(app)
if __name__ == '__main__':
    app.run(debug=True)
