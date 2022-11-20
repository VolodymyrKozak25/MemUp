--CREATE DOMAIN positive_or_zero AS int NOT NULL
--	DEFAULT 0
--	CHECK (VALUE >= 0);

CREATE TABLE IF NOT EXISTS users(
	user_id SERIAL PRIMARY KEY,
	user_name VARCHAR(16) UNIQUE NOT NULL,
	MP_balance positive_or_zero,
	day_streak positive_or_zero,
	messages_status boolean DEFAULT TRUE,
	last_login TIMESTAMPTZ DEFAULT NOW()

);

CREATE TABLE IF NOT EXISTS collections(
	collection_id SERIAL PRIMARY KEY,
	user_id int NOT NULL,
	collection_name VARCHAR(64) UNIQUE NOT NULL,
	daily_limit positive_or_zero DEFAULT 10,
	study_queue positive_or_zero DEFAULT 10,
	review_queue positive_or_zero DEFAULT 0,

	CONSTRAINT collection_owner
		FOREIGN KEY (user_id)
			REFERENCES users(user_id)
			ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS mems(
	mem_id SERIAL PRIMARY KEY,
	user_id int NOT NULL,
	collection_id int NOT NULL,
	question_text TEXT NOT NULL,
	answer_text TEXT NOT NULL,
	additional_info TEXT,
	image_path TEXT,
	review_time TIMESTAMPTZ DEFAULT CURRENT_DATE + INTERVAL '1 day',
	status VARCHAR(6) DEFAULT 'study',
	
	CONSTRAINT user_profile
		FOREIGN KEY (user_id)
			REFERENCES users(user_id)
			ON DELETE CASCADE,
	
	CONSTRAINT parent_collection
		FOREIGN KEY (collection_id)
			REFERENCES collections(collection_id)
			ON DELETE CASCADE
);

--DROP TABLE IF EXISTS mems;
--DROP TABLE IF EXISTS collections;
--DROP TABLE IF EXISTS users;

--SELECT * FROM users;
--SELECT * FROM collections;
--SELECT * FROM mems;

--INSERT INTO collections(user_id, collection_name)
--VALUES (1, 'first collection');

-- INSERT INTO users(user_name)
-- VALUES ('John Doe');

INSERT INTO mems (user_id, collection_id, 
				  question_text, answer_text) 
VALUES (1, 1, 'What is 2 + 2?', '4');

--ALTER SEQUENCE mems_mem_id_seq RESTART;
--ALTER SEQUENCE collections_collection_id_seq RESTART;
--ALTER SEQUENCE users_user_id_seq RESTART;

--SELECT TIMESTAMPTZ('2021-12-1') - TIMESTAMPTZ('2020-12-1 13:01:12');