from flask import Flask
from flask_cors import CORS
from raffle.routes import add_routes

app = Flask(__name__)

CORS(app, resources={r'/*': {'origins': '*'}})
app.config['CORS_HEADERS'] = 'Content-Type'

add_routes(app)
if __name__ == '__main__':
    app.run(debug=True)