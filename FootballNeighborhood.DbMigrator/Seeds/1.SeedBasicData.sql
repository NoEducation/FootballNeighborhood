INSERT INTO [dbo].[Permission]
           ([Name]
           ,[Description])
     VALUES
           ('SaveMatch','Update and edit match'),
           ('EditAnotherUserMatch','Update match not belonging  to user'),
           ('ViewMatches','View matches'),
           ('DeleteMatch','Remove match'),
           ('AssignToMatch','Assign to match'),
           ('UnassignFromMatch','Unassign to match'),
           ('SaveUser','Eidt and update user'),
           ('DeleteUser','Delete user'),
           ('ViewUsers','View users');

SET IDENTITY_INSERT dbo.[Role] on;  

INSERT INTO [dbo].[Role]
           (
		   [Id]
		   ,[Name]
           ,[Description])
     VALUES
           (1,'Admin' ,'Super user that can manipulate all data'),
           (2,'Player' ,'Basic user that can assing to match'),
           (3,'MatchOrganizer' ,'User with privileges to create matches')

SET IDENTITY_INSERT dbo.[Role] off;  

DECLARE @AdminId INT = 1;
DECLARE @PlayerId INT = 2;
DECLARE @MatchOrganizerId INT =3;

INSERT INTO [dbo].[RolePermission]
           ([RoleId]
           ,[PermissionId])
     VALUES
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'SaveMatch')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'EditAnotherUserMatch')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'ViewMatches')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'DeleteMatch')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'AssignToMatch')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'SaveUser')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'DeleteUser')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'ViewUsers')),
           (@AdminId ,(SELECT Id FROM Permission WHERE [Name] = 'UnassignFromMatch')),
           (@PlayerId ,(SELECT Id FROM Permission WHERE [Name] = 'ViewMatches')),
           (@PlayerId ,(SELECT Id FROM Permission WHERE [Name] = 'AssignToMatch')),
		   (@MatchOrganizerId ,(SELECT Id FROM Permission WHERE [Name] = 'ViewMatches')),
           (@MatchOrganizerId ,(SELECT Id FROM Permission WHERE [Name] = 'AssignToMatch')),
           (@MatchOrganizerId ,(SELECT Id FROM Permission WHERE [Name] = 'UnassignFromMatch')),
		   (@MatchOrganizerId ,(SELECT Id FROM Permission WHERE [Name] = 'SaveMatch'));


INSERT INTO [dbo].[User]
           ([RoleId]
           ,[Login]
           ,[Email]
           ,[Password]
           ,[Phone]
           ,[IsActive]
           ,[IsConfirmed]
           ,[Name]
           ,[Surname]
           ,[BirthDate]
           ,[Gender]
           ,[Description]
           ,[AddedDate]
           ,[AddedByUserId]
           ,[ModifiedDate]
           ,[ModifiedByUserId])
     VALUES
           (1
           ,'admin'
           ,'admin@admin.pl'
           ,'LcIx9wktSVybEeWfpjYhEapYomi+0RUl7Uf1ttRISPk='
           ,NULL
           ,1
           ,1
           ,'Admin'
           ,'Admin'
           ,null
           ,null
           ,null
           ,SYSDATETIME()
           ,null
           ,null,
           null)
GO




