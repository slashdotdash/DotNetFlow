CREATE TABLE SchemaInfo (Version BIGINT  NOT NULL PRIMARY KEY)
CREATE TABLE Submissions (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, SubmittedAt DATETIME, UsersName NVARCHAR(1000), Title NVARCHAR(140), HtmlContent NVARCHAR(2000))
INSERT INTO SchemaInfo (version) VALUES ('20110524213700')
