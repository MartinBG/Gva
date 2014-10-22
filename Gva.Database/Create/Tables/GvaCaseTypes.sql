PRINT 'GvaCaseTypes'
GO 

CREATE TABLE [dbo].[GvaCaseTypes] (
    [GvaCaseTypeId]    INT            NOT NULL,
    [Name]             NVARCHAR (200) NULL,
    [Alias]            NVARCHAR (200) NULL,
    [LotSetId]         INT            NULL,
    [ClassificationId] INT            NULL,
    [IsDefault]        BIT            NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_GvaCases]                 PRIMARY KEY ([GvaCaseTypeId]),
    CONSTRAINT [FK_GvaCases_LotSets]         FOREIGN KEY ([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId]),
    CONSTRAINT [FK_GvaCases_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId])
)
GO

exec spDescTable  N'GvaCaseTypes', N'Типове дела на партиди.'
exec spDescColumn N'GvaCaseTypes', N'GvaCaseTypeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaCaseTypes', N'Name'            , N'Име на типа дело.'
exec spDescColumn N'GvaCaseTypes', N'Alias'           , N'Символен идентификатор.'
exec spDescColumn N'GvaCaseTypes', N'LotSetId'        , N'Тип партида.'
exec spDescColumn N'GvaCaseTypes', N'ClassificationId', N'Класификация отговаряща на типа дело.'
exec spDescColumn N'GvaCaseTypes', N'IsDefault'       , N'Партовете за които няма запис в GvaLotFiles се считат за принадлежащи на default-ния caseType от техния LotSet.'
exec spDescColumn N'GvaCaseTypes', N'IsActive'        , N'Маркер за активност.'
GO
