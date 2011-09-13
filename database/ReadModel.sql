CREATE TABLE Submissions (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, SubmittedAt DATETIME, UsersName NVARCHAR(1000), Title NVARCHAR(140), HtmlContent NVARCHAR(2000))
INSERT INTO SchemaInfo (version) VALUES ('20110524213700')
CREATE TABLE Users (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, RegisteredAt DATETIME, FullName NVARCHAR(200), Username NVARCHAR(20), Email NVARCHAR(1000), HashedPassword NCHAR(60), Website NVARCHAR(1000), Twitter NVARCHAR(20))
CREATE INDEX IX_Users_Email ON Users (Email) 
ALTER TABLE Users ADD CONSTRAINT IX_Users_Unique_Username UNIQUE(Username) 
ALTER TABLE Users ADD CONSTRAINT IX_Users_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530133800')
CREATE TABLE RegisteredEmailAddresses (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, Email NVARCHAR(1000))
CREATE INDEX IX_RegisteredEmailAddresses_Email ON RegisteredEmailAddresses (Email) 
ALTER TABLE RegisteredEmailAddresses ADD CONSTRAINT IX_RegisteredEmailAddresses_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530140000')
CREATE TABLE Items (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, PublishedAt DATETIME, SubmittedByUser NVARCHAR(1000), Title NVARCHAR(140), HtmlContent NVARCHAR(2000))
INSERT INTO SchemaInfo (version) VALUES ('20110620213300')
CREATE TABLE RegisteredUsernames (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, Username NVARCHAR(20))
CREATE INDEX IX_RegisteredUsernames_Username ON RegisteredUsernames (Username) 
ALTER TABLE RegisteredUsernames ADD CONSTRAINT IX_RegisteredUsernames_Unique_Username UNIQUE(Username) 
INSERT INTO SchemaInfo (version) VALUES ('20110707075300')
