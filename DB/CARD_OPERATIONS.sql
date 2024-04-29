----create all needed db functionalities for card that is needed
use iInsurance;
--FOR CARD CURD, USING TRANSACTION TO ROLLBACK FAULTY QUERIES


CREATE PROCEDURE ADD_CARD @card_owner NVARCHAR(255), @card_no BIGINT, @sec_code INT, @valid DATE
AS
BEGIN
	
	BEGIN TRANSACTION
	
		INSERT INTO card_info(card_owner,card_no,security_code,valid_through)
		VALUES(@card_owner,@card_no,@sec_code,@valid);

	COMMIT TRANSACTION
END;

CREATE PROCEDURE UPDATE_CARD @card_id INT,@card_owner NVARCHAR(255), @card_no BIGINT, @sec_code INT, @valid DATE
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE card_info 
		SET card_owner=@card_owner,card_no=@card_no,security_code=@sec_code,valid_through=@valid
		WHERE card_id=@card_id
	COMMIT TRANSACTION

END;

CREATE PROCEDURE DELETE_CARD @card_id INT
AS
BEGIN
	BEGIN TRANSACTION
		DELETE FROM card_info where card_id=@card_id;
	COMMIT TRANSACTION
END;


CREATE PROCEDURE GET_CARDS
AS
BEGIN
	SELECT * from card_info;
END;