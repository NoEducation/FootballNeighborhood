CREATE TABLE Permission(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(1024) NULL
);

CREATE TABLE [Role](
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR (256) NOT NULL,
	[Description] NVARCHAR(1024) NULL
);

CREATE TABLE RolePermission(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	RoleId INT NOT NULL,
	PermissionId INT NULL,
	
	CONSTRAINT FK_Permission_RolePermission FOREIGN KEY(PermissionId)
		REFERENCES [Permission](Id),

	CONSTRAINT FK_Role_RolePermission FOREIGN KEY(RoleId)
		REFERENCES [Role](Id),
);

CREATE TABLE [User](
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	RoleId INT NOT NULL,
	Login NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) NOT NULL,
	Password NVARCHAR(512) NOT NULL,
	Phone NVARCHAR(10) NULL,
	IsActive Bit NOT NULL,
	IsConfirmed Bit NOT NULL,
	Name NVARCHAR(255) NOT NULL,
	Surname NVARCHAR (255) NOT NULL,
	BirthDate DATETIMEOFFSET(7) NOT NULL,
	Gender smallint NOT NULL,
	Description TEXT NULL,
	AddedDate DATETIMEOFFSET(7) NOT NULL,
	AddedByUserId INT NULL,
	ModifedDate DATETIMEOFFSET(7) NULL,
	ModifiedByUserId INT NULL,

	CONSTRAINT UQ_UserLogin UNIQUE(Login),
	CONSTRAINT UQ_UserEmail UNIQUE(Email),

	CONSTRAINT FK_Role_User_AddedByUserId FOREIGN KEY(RoleId) 
		REFERENCES [Role](Id),
	CONSTRAINT FK_User_User_AddedByUserId FOREIGN KEY(AddedByUserId) 
		REFERENCES [User](Id),
	CONSTRAINT FK_User_User_ModifiedByUserId FOREIGN KEY(ModifiedByUserId) 
		REFERENCES [User](Id),
);