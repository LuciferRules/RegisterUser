﻿﻿CREATE TABLE users (
	UserID BIGINT NOT NULL PRIMARY KEY IDENTITY,
	UserName VARCHAR(20) NOT NULL,
	UserNo INT NOT NULL,
	CreateDate  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
	
);
-- ADD RANDOM VALUE INTO THE 'users'
INSERT INTO users (UserName, UserNo, CreateDate)
VALUES
('Bill Gates', 000001, '2023-04-05 17:45:16.743'),
('Elon Musk', 000002, '2023-04-05 17:45:16.743'),
('Will Smith', 000003, '2023-04-05 17:45:16.743'),
('Bob Marley', 000004, '2023-04-05 17:45:16.743'),
('Cristiano Ronaldo', 000005, '2023-04-05 17:45:16.743'),
('Boris Johnson', 000006, '2023-04-05 17:45:16.743');