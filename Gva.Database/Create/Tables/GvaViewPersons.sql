PRINT 'GvaViewPersons'
GO 

CREATE TABLE [dbo].[GvaViewPersons] (
    [LotId]          INT           NOT NULL,
    [Lin]            NVARCHAR(50)  NOT NULL,
    [LinType]        NVARCHAR(50)  NOT NULL,
    [Uin]            NVARCHAR(50)  NULL,
    [Names]          NVARCHAR(MAX) NOT NULL,
    [BirtDate]       DATETIME2 (7) NOT NULL,
    [Organization]   NVARCHAR(50)  NULL,
    [Employment]     NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaViewPersons]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewPersons_Lots] FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersons', N'Физически лица.'
exec spDescColumn N'GvaViewPersons', N'LotId', N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaViewPersons', N'Lin'           , N'Личен идентификационен номер.'
exec spDescColumn N'GvaViewPersons', N'LinType'       , N'Тип лин.'
exec spDescColumn N'GvaViewPersons', N'Uin'           , N'Единен граждански номер.'
exec spDescColumn N'GvaViewPersons', N'Names'         , N'Имена.'
exec spDescColumn N'GvaViewPersons', N'BirtDate'      , N'Дата на раждане.'
exec spDescColumn N'GvaViewPersons', N'Organization'  , N'Фирма.'
exec spDescColumn N'GvaViewPersons', N'Employment'    , N'Длъжност.'
GO
