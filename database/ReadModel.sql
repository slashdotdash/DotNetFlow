CREATE TABLE SchemaInfo (Version BIGINT  NOT NULL PRIMARY KEY)
CREATE TABLE Submissions (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, SubmittedAt DATETIME, UsersName NVARCHAR(1000), Title NVARCHAR(1000), HtmlContent NTEXT)
INSERT INTO SchemaInfo (version) VALUES ('20110524213700')