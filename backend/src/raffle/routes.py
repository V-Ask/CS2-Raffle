from flask import jsonify
import configparser

config = configparser.ConfigParser()

config.read('config.ini')

def add_routes(app):
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
