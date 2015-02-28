USE Forum
GO

--Task 1
SELECT
	Title
FROM Questions
ORDER BY Title

--Task 2
SELECT
	Content,
	CreatedOn
FROM Answers
WHERE createdOn BETWEEN DATEFROMPARTS(2012, 06, 15) AND DATEFROMPARTS(2013, 03, 22)
ORDER BY createdOn, Id

--Task 3
SELECT
	Username,
	LastName,
	IIF(PhoneNumber IS NULL, 0, 1) AS [Has Phone]
FROM Users
ORDER BY LastName, Id

--Task 4
SELECT
	q.Title AS [Question Title],
	u.Username AS Author
FROM Questions q
JOIN Users u
	ON u.Id = q.UserId
ORDER BY q.Id

--Task 5
SELECT
	a.Content AS [Answer Content],
	a.CreatedOn,
	ua.Username AS [Answer Author],
	q.Title AS [Question Title],
	c.Name AS [Category Name]
FROM Answers a
JOIN Questions q
	ON a.QuestionId = q.Id
JOIN Categories c
	ON q.CategoryId = c.Id
JOIN Users u
	ON q.UserId = u.Id
JOIN Users ua
	ON a.UserId = ua.Id
ORDER BY c.Name, ua.Username, a.CreatedOn

--Task 6
SELECT
	c.Name,
	q.Title,
	q.CreatedOn
FROM Categories c
LEFT OUTER JOIN Questions q
	ON c.Id = q.CategoryId
ORDER BY c.Name, q.Title

--Task 7
SELECT
	u.Id,
	u.Username,
	u.FirstName,
	u.PhoneNumber,
	u.RegistrationDate,
	u.Email
FROM Users u
LEFT OUTER JOIN Questions q
	ON q.UserId = u.Id
WHERE PhoneNumber IS NULL
AND q.Id IS NULL
ORDER BY u.RegistrationDate

--Task 8
SELECT
	MIN(a.createdon) AS [MinDate],
	MAX(a.createdOn) AS [MaxDate]
FROM Answers a
WHERE YEAR(a.createdOn) BETWEEN 2012 AND 2014

--Task 9
SELECT TOP 10
	a.Content AS Content,
	a.CreatedOn,
	u.Username
FROM Answers a
JOIN Users u
	ON a.UserId = u.Id
ORDER BY a.createdon

--Task 10
SELECT
	a.Content AS [Answer Content],
	q.Title AS Question,
	c.Name AS Category
FROM Answers a
JOIN Questions q
	ON a.Questionid = q.Id
JOIN Categories c
	ON q.CategoryId = c.Id
WHERE a.IsHidden = 1
AND (
MONTH(a.CreatedOn) = (SELECT TOP 1
	MONTH(createdon)
FROM Answers
ORDER BY CreatedOn DESC)
OR MONTH(a.CreatedOn) = (SELECT TOP 1
	MONTH(createdon)
FROM Answers
ORDER BY CreatedOn)
)
AND YEAR(a.CreatedOn) = (SELECT TOP 1
	YEAR(CreatedOn)
FROM Answers
ORDER BY CreatedOn DESC)
ORDER BY c.Name

--Task 11
--false solution - same categories but different order
--SELECT c.Name AS Category, Count(a.Id) AS [Answers Count]
--FROM Answers a LEFT OUTER JOIN Questions q
--	ON a.QuestionId = q.Id RIGHT OUTER JOIN Categories c
--	ON q.CategoryId = c.Id
--GROUP BY c.Id, c.Name
--ORDER BY Count(a.Id) DESC

SELECT
	c.Name AS Category,
	COUNT(a.Id) AS [Answers Count]
FROM Categories c
LEFT OUTER JOIN Questions q
	ON q.CategoryId = c.Id
LEFT OUTER JOIN Answers a
	ON a.QuestionId = q.Id
GROUP BY c.Name
ORDER BY COUNT(a.Id) DESC

--Task 12
SELECT
	c.Name AS Category,
	u.Username,
	u.PhoneNumber AS [PhoneNumber],
	COUNT(a.Id) AS [Answers Count]
FROM Categories c
LEFT OUTER JOIN Questions q
	ON q.CategoryId = c.Id
LEFT OUTER JOIN Answers a
	ON a.QuestionId = q.Id
JOIN Users u
	ON a.UserId = u.Id
GROUP BY	c.Name,
			u.Username,
			u.PhoneNumber
HAVING COUNT(a.Id) <> 0
AND MIN(u.Phonenumber) IS NOT NULL
ORDER BY COUNT(a.Id) DESC

--Task 13
USE Forum
GO

CREATE TABLE Towns
(
    Id INT IDENTITY (1,1) NOT NULL,
	Name NVARCHAR(max) ,
	CONSTRAINT PK_Towns PRIMARY KEY (Id)
);

ALTER TABLE Users
ADD
 TownId INT,
 CONSTRAINT FK_Users_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id)
GO

--Execute the following SQL script (it should pass without any errors):
INSERT INTO Towns (Name)
	VALUES ('Sofia'), ('Berlin'), ('Lyon')
UPDATE Users
SET TownId = (SELECT
	Id
FROM Towns
WHERE Name = 'Sofia')
INSERT INTO Towns
	VALUES ('Munich'), ('Frankfurt'), ('Varna'), ('Hamburg'), ('Paris'), ('Lom'), ('Nantes')

--Task 13.3
BEGIN TRAN
UPDATE Users
SET TownId = (SELECT
	ID
FROM Towns
WHERE Name = 'Paris')
WHERE DATEPART(WEEKDAY, (RegistrationDate)) = '6'

COMMIT TRAN

--Task 13.4
BEGIN TRAN
UPDATE Answers
SET QuestionId = (SELECT
	Id
FROM Questions
WHERE Title = 'Java += operator')
WHERE DATEPART(MONTH, CreatedOn) = 2
AND (DATEPART(WEEKDAY, CreatedOn) = '1'
OR DATEPART(WEEKDAY, CreatedOn) = '2')
COMMIT TRAN

--Task 13.5
BEGIN TRAN
CREATE TABLE #AnswerIds(
	AnswerId INT
);

INSERT INTO #AnswerIds
	SELECT
		a.Id
	FROM Answers a
	WHERE (SELECT
		SUM(v.Value)
	FROM Answers aa
	JOIN Votes v
		ON a.Id = v.AnswerId)
	< 0

DELETE FROM Votes
WHERE AnswerId IN (SELECT
		AnswerId
	FROM #AnswerIds)

DELETE FROM Answers
WHERE Id IN (SELECT
		AnswerId
	FROM #AnswerIds)
ROLLBACK TRAN

--Task 13.6
BEGIN TRAN
SELECT
	*
FROM Questions
WHERE UserId = (SELECT
	Id
FROM Users
WHERE Username = 'darkcat')
INSERT INTO Questions (Title, Content, CategoryId, UserId, CreatedOn)
	VALUES ('Fetch NULL values in PDO query', 'When I run the snippet, NULL values are converted to empty strings. How can fetch NULL values?', (SELECT Id FROM Categories WHERE Name = 'Databases'), (SELECT Id FROM Users WHERE Username = 'darkcat'), GETDATE())
SELECT
	*
FROM Questions
WHERE UserId = (SELECT
	Id
FROM Users
WHERE Username = 'darkcat')
ROLLBACK TRAN

--Task 13.7
SELECT
	t.Name AS Town,
	u.Username,
	COUNT(a.Id) AS [AnswersCount]
FROM Answers a
FULL OUTER JOIN Users u
	ON a.UserId = u.Id
FULL OUTER JOIN Towns t
	ON u.TownId = t.Id
GROUP BY	t.Name,
			u.Username
ORDER BY [AnswersCount] DESC, u.Username
 

--Task 14.1

CREATE VIEW AllQuestions
AS
SELECT
	u.Id AS UId,
	u.Username,
	u.FirstName,
	u.LastName,
	u.Email,
	u.PhoneNumber,
	u.RegistrationDate,
	q.Id AS QId,
	q.Title,
	q.Content,
	q.CategoryId,
	q.UserId,
	q.CreatedOn
FROM Users u
LEFT OUTER JOIN Questions q
	ON q.UserId = u.Id
GO

SELECT
	*
FROM AllQuestions

--Task 14.2
IF(object_Id(N'fn_ListUsersQuestions') IS NOT NULL)
DROP FUNCTION fn_ListUsersQuestions
GO

CREATE FUNCTION fn_ListUsersQuestions()
RETURNS @returntable TABLE 
(
	UserName NVARCHAR(MAX),
	Questions NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE userCursor CURSOR FOR 
		SELECT  Username FROM Users
		ORDER BY Username;
	OPEN userCursor;
	DECLARE @username NVARCHAR(MAX);
	FETCH NEXT FROM userCursor INTO @username;

	WHILE @@fetch_status = 0
		BEGIN
			DECLARE @questions NVARCHAR(MAX) = NULL;
			SELECT 
				--@questions = IIF(@questions IS NULL, Title, ', ' + Title)
				@questions = CASE
				WHEN @questions IS NULL THEN CONVERT(NVARCHAR(MAX), Title, 112)
				ELSE @questions + ', ' + CONVERT(NVARCHAR(MAX), Title, 112)
			END
			FROM AllQuestions
			WHERE @username = Username
			ORDER BY Title DESC;

			INSERT @returntable
			VALUES( @username, @questions )
			FETCH NEXT FROM userCursor INTO @username;
		END
	CLOSE userCursor;
	DEALLOCATE userCursor;
RETURN
END
GO

SELECT * FROM fn_ListUsersQuestions()