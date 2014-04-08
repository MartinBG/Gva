PRINT 'GvaCaseTypes'
GO 

CREATE TABLE [dbo].[GvaCaseTypes] (
    [GvaCaseTypeId]    INT            NOT NULL,
    [Name]             NVARCHAR (200) NULL,
    [Alias]            NVARCHAR (200) NULL,
    [LotSetId]         INT            NULL,
    CONSTRAINT [PK_GvaCases]         PRIMARY KEY ([GvaCaseTypeId]),
    CONSTRAINT [FK_GvaCases_LotSets] FOREIGN KEY([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId]),
)
GO

exec spDescTable  N'GvaCaseTypes', N'Типове дела на партиди.'
exec spDescColumn N'GvaCaseTypes', N'GvaCaseTypeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaCaseTypes', N'Name'            , N'Име на типа дело.'
exec spDescColumn N'GvaCaseTypes', N'Alias'           , N'Символен идентификатор.'
exec spDescColumn N'GvaCaseTypes', N'LotSetId'        , N'Тип партида.'
GO
