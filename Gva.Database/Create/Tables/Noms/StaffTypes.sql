print 'StaffTypes'
GO

CREATE TABLE [dbo].[StaffTypes] (
    [StaffTypeId]    INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_StaffTypes] PRIMARY KEY ([StaffTypeId])
);
GO

exec spDescTable  N'StaffTypes'                  , N'Типове персонал (направление на квалификацията)'
exec spDescColumn N'StaffTypes', N'StaffTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'StaffTypes', N'Code'         , N'Код.'
exec spDescColumn N'StaffTypes', N'Name'         , N'Наименование.'
GO
