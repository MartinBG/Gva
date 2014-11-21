print 'RoleClassifications'
GO 

CREATE TABLE RoleClassifications
(
    RoleClassificationId			INT				IDENTITY (1, 1) NOT NULL,
	RoleId					INT				NOT NULL,
	ClassificationId		INT				NOT NULL,
	ClassificationPermissionId		INT				NOT NULL,
    Version					ROWVERSION     NOT NULL,
    CONSTRAINT PK_RoleClassifications PRIMARY KEY CLUSTERED ([RoleClassificationId]),
	CONSTRAINT [FK_RoleClassifications_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
	CONSTRAINT [FK_RoleClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
	CONSTRAINT [FK_RoleClassifications_ClassificationPermissions] FOREIGN KEY ([ClassificationPermissionId]) REFERENCES [dbo].[ClassificationPermissions] ([ClassificationPermissionId]),
)
GO 
