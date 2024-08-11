CREATE TABLE MatchPlayerReview(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	MatchPlayerId INT NOT NULL,
	ReviewedPlayerId INT NOT NULL,
	Score SMALLINT NOT NULL,
	[Description] TEXT NULL,
	AddedDate DATETIMEOFFSET(7) NOT NULL,
	AddedByUserId INT NULL,
	ModifiedDate DATETIMEOFFSET(7) NULL,
	ModifiedByUserId INT NULL,

	CONSTRAINT FK_MatchPlayer_MatchPlayerReview FOREIGN KEY(MatchPlayerId) 
		REFERENCES [MatchPlayer](Id),
	CONSTRAINT FK_ReviewedPlayer_MatchPlayerReview FOREIGN KEY(ReviewedPlayerId) 
		REFERENCES [MatchPlayer](Id),
	CONSTRAINT FK_User_MatchPlayerReview_AddedByUserId FOREIGN KEY(AddedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_MatchPlayerReview_ModifiedByUserId FOREIGN KEY(ModifiedByUserId) 
		REFERENCES [User](Id),
);

ALTER TABLE MatchPlayer
	ADD MatchReviewScore SMALLINT NULL;

ALTER TABLE MatchPlayer
	ADD MatchReviewDescription TEXT NULL;

ALTER TABLE MatchPlayer
	ADD MatchOwnerReviewScore SMALLINT NULL;