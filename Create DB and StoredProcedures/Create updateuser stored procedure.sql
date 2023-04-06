-- CREATE 'updateuser' procedure
GO
CREATE PROCEDURE updateuser(@userid BIGINT, @username VARCHAR(20), @userno INT)
AS
UPDATE users
SET UserName= @username, UserNo= @userno
WHERE UserID= @userid