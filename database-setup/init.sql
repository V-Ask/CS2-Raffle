CREATE DOMAIN MAP_ID AS TEXT CHECK(VALUE ~* '[0-9]{8}$');

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(50) NOT NULL UNIQUE,
    password_salt VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(200) NOT NULL,
    username VARCHAR(20) NOT NULL
);

CREATE TABLE maps (
    id MAP_ID PRIMARY KEY,
    name TEXT,
    image VARCHAR(100),
    description TEXT,
    weight INT DEFAULT 1
);

CREATE TABLE collections (
    id SERIAL PRIMARY KEY,
    collection_name TEXT NOT NULL,
    author_id SERIAL NOT NULL REFERENCES users(id)
);

CREATE TABLE collections_maps (
    map_id CHAR REFERENCES maps(id) ON UPDATE CASCADE,
    collection_id SERIAL REFERENCES collections(id) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT collections_maps_pk PRIMARY KEY (map_id, collection_id)
);