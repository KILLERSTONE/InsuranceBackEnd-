--create all needed db functionalities that is needed
use iInsurance;
--FOR LOGIN 

drop function valid_email;
CREATE FUNCTION valid_email(@email nvarchar(255))
RETURNS BIT
AS
BEGIN
	DECLARE @res BIT;

	IF @email LIKE '_%@%.%'
        SET @res = 1;
    ELSE
        SET @res = 0;

    RETURN @res;
END;

CREATE PROCEDURE user_login 
    @username NVARCHAR(255),
    @password NVARCHAR(255),
    @user_id INT OUTPUT  
AS
BEGIN
    DECLARE @login_status BIT;

    SET @user_id = NULL;

    SELECT @user_id = user_id 
    FROM user_info 
    WHERE username = @username AND password = @password;

    IF @user_id IS NOT NULL
    BEGIN
        SET @login_status = 1;
    END
    ELSE
    BEGIN
        SET @login_status = 0;
    END

    -- Insert login log
    INSERT INTO login_log (user_id, login_time, login_success)
    VALUES (@user_id, GETDATE(), @login_status);
END;
