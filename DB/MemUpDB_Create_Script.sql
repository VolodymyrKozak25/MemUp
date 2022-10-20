-- Database: MemUpDB

-- DROP DATABASE IF EXISTS "MemUpDB";

CREATE DATABASE "MemUpDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1251'
    LC_CTYPE = 'English_United States.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE "MemUpDB"
    IS 'Educational Project.';