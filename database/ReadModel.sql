CREATE TABLE Submissions (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, SubmittedAt DATETIME, UserId UNIQUEIDENTIFIER, Username NVARCHAR(20), FullName NVARCHAR(200), Title NVARCHAR(140), HtmlContent NVARCHAR(2000))
CREATE INDEX IX_Submissions_SubmittedAt ON Submissions (SubmittedAt) 
INSERT INTO SchemaInfo (version) VALUES ('20110524213700')
CREATE TABLE Users (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, RegisteredAt DATETIME, FullName NVARCHAR(200), Username NVARCHAR(20), Email NVARCHAR(1000), HashedPassword NCHAR(60), Website NVARCHAR(1000), Twitter NVARCHAR(20))
CREATE INDEX IX_Users_Username_Email ON Users (Username, Email) 
CREATE INDEX IX_Users_Username ON Users (Username) 
CREATE INDEX IX_Users_Email ON Users (Email) 
ALTER TABLE Users ADD CONSTRAINT IX_Users_Unique_Username UNIQUE(Username) 
ALTER TABLE Users ADD CONSTRAINT IX_Users_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530133800')
CREATE TABLE RegisteredEmailAddresses (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, Email NVARCHAR(1000))
CREATE INDEX IX_RegisteredEmailAddresses_Email ON RegisteredEmailAddresses (Email) 
ALTER TABLE RegisteredEmailAddresses ADD CONSTRAINT IX_RegisteredEmailAddresses_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530140000')
CREATE TABLE Items (ItemId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, PublishedAt DATETIME, SubmittedByUserId UNIQUEIDENTIFIER, SubmittedByUsername NVARCHAR(20), SubmittedByFullName NVARCHAR(200), Title NVARCHAR(140), HtmlContent NVARCHAR(2000))
CREATE INDEX IX_Items_PublishedAt ON Items (PublishedAt) 
CREATE INDEX IX_Items_SubmittedByUserId_PublishedAt ON Items (SubmittedByUserId, PublishedAt) 
CREATE INDEX IX_Items_Title ON Items (Title) 
INSERT INTO SchemaInfo (version) VALUES ('20110620213300')
CREATE TABLE RegisteredUsernames (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, Username NVARCHAR(20))
CREATE INDEX IX_RegisteredUsernames_Username ON RegisteredUsernames (Username) 
ALTER TABLE RegisteredUsernames ADD CONSTRAINT IX_RegisteredUsernames_Unique_Username UNIQUE(Username) 
INSERT INTO SchemaInfo (version) VALUES ('20110707075300')
