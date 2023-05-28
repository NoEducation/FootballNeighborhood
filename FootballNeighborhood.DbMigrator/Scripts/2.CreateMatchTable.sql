CREATE TABLE [Match](
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	OwnerId INT NOT NULL,
	[Name] NVARCHAR(512) NULL,
	[IsFinished] BIT NOT NULL,
	[StartDateTime] DATETIMEOFFSET(7),
	[EndDateTime] DATETIMEOFFSET(7),
	[City] NVARCHAR(256) NOT NULL,
	AddressLine NVARCHAR(256) NOT NULL,
	[AllowedPlayers] SMALLINT NOT NULL,
	[MinPlayers] SMALLINT NULL,
	[ShowEmailAddress] BIT NOT NULL,
	[ShowPhoneNumber] BIT NOT NULL,
    AddedDate DATETIMEOFFSET(7) NOT NULL,
	AddedByUserId INT NULL,
	ModifiedDate DATETIMEOFFSET(7) NULL,
	ModifiedByUserId INT NULL,

	CONSTRAINT UQ_MatchName UNIQUE([Name]),
	CONSTRAINT FK_User_Match FOREIGN KEY(OwnerId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_Match_AddedByUserId FOREIGN KEY(AddedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_Match_ModifiedByUserId FOREIGN KEY(ModifiedByUserId) 
		REFERENCES [User](Id),
);


CREATE TABLE MatchPlayer(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	MatchId INT NOT NULL,
	UserId INT NOT NULL,
	PlayerType SMALLINT NOT NULL,
	AddedDate DATETIMEOFFSET(7) NOT NULL,
	AddedByUserId INT NULL,
	ModifiedDate DATETIMEOFFSET(7) NULL,
	ModifiedByUserId INT NULL,

	CONSTRAINT FK_Match_MatchPlayer FOREIGN KEY(MatchId) 
		REFERENCES [Match](Id),
	CONSTRAINT FK_User_MatchPlayer FOREIGN KEY(UserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_MatchPlayer_AddedByUserId FOREIGN KEY(AddedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_MatchPlayer_ModifiedByUserId FOREIGN KEY(ModifiedByUserId) 
		REFERENCES [User](Id),
)