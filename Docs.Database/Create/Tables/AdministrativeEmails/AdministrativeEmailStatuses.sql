print 'AdministrativeEmailStatuses'
GO 

CREATE TABLE [dbo].[AdministrativeEmailStatuses] (
    [AdministrativeEmailStatusId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                        NVARCHAR (200) NOT NULL,
    [Alias]                   NVARCHAR (200) NULL,
    [Version]                     ROWVERSION     NOT NULL,
    CONSTRAINT [PK_AdministrativeEmailStatuses] PRIMARY KEY ([AdministrativeEmailStatusId]),
)
GO
