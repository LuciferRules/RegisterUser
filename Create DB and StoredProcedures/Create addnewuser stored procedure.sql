-- CREATE 'addnewuser' procedure
GO
CREATE PROCEDURE addnewuser(@username VARCHAR(20), @userno INT)
AS
INSERT INTO users(UserName, UserNo) 
VALUES (@username, @userno)

