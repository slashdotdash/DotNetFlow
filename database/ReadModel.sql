CREATE TABLE Users (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, RegisteredAt DATETIME, FullName NVARCHAR(200), Email NVARCHAR(1000), HashedPassword NCHAR(40), PasswordSalt NCHAR(12), Website NVARCHAR(1000), Twitter NVARCHAR(200))
CREATE INDEX IX_Users_Email ON Users (Email) 
ALTER TABLE Users ADD CONSTRAINT IX_Users_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530133800')
CREATE TABLE RegisteredEmailAddresses (UserId UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY, Email NVARCHAR(1000))
CREATE INDEX IX_RegisteredEmailAddresses_Email ON RegisteredEmailAddresses (Email) 
ALTER TABLE RegisteredEmailAddresses ADD CONSTRAINT IX_RegisteredEmailAddresses_Unique_Email UNIQUE(Email) 
INSERT INTO SchemaInfo (version) VALUES ('20110530140000')
