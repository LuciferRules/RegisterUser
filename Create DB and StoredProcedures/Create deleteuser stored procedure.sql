-- CREATE 'deleteuser' procedure
GO
CREATE PROCEDURE deleteuser(@userid BIGINT)
AS
DELETE FROM users
WHERE UserID= @userid