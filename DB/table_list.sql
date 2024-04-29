
create database iInsurance;

use iInsurance;
select * from INFORMATION_SCHEMA.TABLES
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
drop table user_info;
CREATE TABLE user_info(
	user_id INT IDENTITY(1,1) PRIMARY KEY,
	email NVARCHAR(255),
	username NVARCHAR(255),
	password NVARCHAR(255),
);

ALTER TABLE user_info
ADD CONSTRAINT CHECK_UNIQUE_EMAIL UNIQUE(email); -- ensure all users in the table are unqiue to each email

ALTER TABLE user_info 
ADD CONSTRAINT CHECK_EMAIL_FORMAT CHECK(email LIKE '_%@%.%') -- ensure the email follows the format it should

ALTER TABLE user_info 
ADD CONSTRAINT CHECK_PASSWORD_LENGTH CHECK(LEN(password)>=8); -- the min length of password in modern application is set to 8

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
drop table card_info;
CREATE TABLE card_info (
	card_id INT IDENTITY(1,1) PRIMARY KEY,
    card_owner VARCHAR(255) NOT NULL, 
    card_no BIGINT NOT NULL, 
    security_code INT NOT NULL,
    valid_through DATE NOT NULL
);

ALTER TABLE card_info
ADD CONSTRAINT CHECK_CARD_LEN CHECK (LEN(card_no) = 16); --Checking the length of card is correct or not

ALTER TABLE card_info
ADD CONSTRAINT CHECK_CVV_LEN CHECK(LEN(security_code)=3); --Checking CVV code length

ALTER TABLE card_info
ADD CONSTRAINT CHECK_UNIQUE_CARD UNIQUE (card_no);--Checking whether each card added into table is unique or not

ALTER TABLE card_info
ADD CONSTRAINT CHECK_VALIDITY CHECK (valid_through > GETDATE()); --Checking whether the card is valid till current date

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE policies(
	user_id INT FOREIGN KEY REFERENCES user_info(user_id),
	policy_id INT IDENTITY(1,1) PRIMARY KEY,
	policy_name NVARCHAR(255) NOT NULL,
	policy_detail NVARCHAR(MAX),
	policy_insurer NVARCHAR(255) NOT NULL,
	policy_tpa NVARCHAR(255),
	policy_from DATE NOT NULL,
	policy_to DATE NOT NULL,
)

ALTER TABLE policies
ADD CONSTRAINT CHECK_VALID_POLICY CHECK (policy_to>=policy_from);

ALTER TABLE policies
ADD CONSTRAINT DEFAULT_POLICY_FROM DEFAULT GETDATE() FOR policy_from;

ALTER TABLE policies
ADD CONSTRAINT UNIQUE_POLICY_NAME UNIQUE(policy_name);

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
drop table login_log;
CREATE TABLE login_log (
    login_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT,
    login_time DATETIME DEFAULT GETDATE(),
    login_success BIT DEFAULT 0,
    CONSTRAINT FOREIGN_KEY_USER FOREIGN KEY(user_id) REFERENCES user_info(user_id)
);
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
