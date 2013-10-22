print 'AdministrativeEmailTypes'
GO 

CREATE TABLE [dbo].[AdministrativeEmailTypes] (
    [AdministrativeEmailTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                      NVARCHAR (200) NOT NULL,
    [Alias]                 NVARCHAR (200) NULL,
    [Subject]                   NVARCHAR (500) NOT NULL,
    [Body]                      NVARCHAR (MAX) NULL,
    [Version]                   ROWVERSION     NOT NULL,
    CONSTRAINT [PK_AdministrativeEmailTypes] PRIMARY KEY ([AdministrativeEmailTypeId]),
)
GO
