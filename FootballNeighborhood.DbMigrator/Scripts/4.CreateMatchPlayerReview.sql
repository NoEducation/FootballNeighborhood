CREATE TABLE MatchPlayerReview(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	MatchId INT NOT NULL,
	ReviewedUserId INT NOT NULL,
	ReviewedByUserId INT NOT NULL,
	Score SMALLINT NOT NULL,
	[Description] TEXT NULL,
	AddedDate DATETIMEOFFSET(7) NOT NULL,
	AddedByUserId INT NULL,
	ModifiedDate DATETIMEOFFSET(7) NULL,
	ModifiedByUserId INT NULL,

	CONSTRAINT FK_ReviewedUser_MatchPlayerReview FOREIGN KEY(ReviewedUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_ReviewedByUser_MatchPlayerReview FOREIGN KEY(ReviewedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_Match_MatchPlayerReview FOREIGN KEY(MatchId) 
		REFERENCES [Match](Id),
	CONSTRAINT FK_User_MatchPlayer_AddedByUserId FOREIGN KEY(AddedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_MatchPlayer_ModifiedByUserId FOREIGN KEY(ModifiedByUserId) 
		REFERENCES [User](Id),
);

ALTER TABLE MatchPlayer
	ADD MatchReviewScore SMALLINT NULL;