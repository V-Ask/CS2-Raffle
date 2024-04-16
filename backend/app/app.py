from flask import Flask, jsonify
from flask_cors import CORS, cross_origin
from app.maps.map_pool import MapPool
import configparser

config = configparser.ConfigParser()

config.read('config.ini')

app = Flask(__name__)

CORS(app, resources={r'/*': {'origins': '*'}})
app.config['CORS_HEADERS'] = 'Content-Type'

@app.route('/test')
def test():
    return jsonify({'hello': 'world'})

@app.route('/submitmap', methods=['POST'])
def submit_map():
    return '1'

@app.route('/randommaps', methods=['GET'])
def get_random_map_pool():
    return '2'

@app.route('/startmap', methods=['GET'])
def start_map():
    return '3'

if __name__ == '__main__':
    app.run(debug=True)
