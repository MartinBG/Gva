print 'RatingInstances'
GO

CREATE TABLE [dbo].[RatingInstances] (
    [RatingInstanceId]    INT             NOT NULL IDENTITY(1,1),
    [RatingId]            INT             NOT NULL,
    [IssueDate]           DATE            NULL,
    [ValidDate]           DATE            NULL,
    [ExaminerId]          INT             NULL,
    [Limitations]         NVARCHAR (200)  NULL,
    [Subclasses]          NVARCHAR (200)  NULL,
    [Notes]               NVARCHAR (500)  NULL,
    [NotesLatin]          NVARCHAR (500)  NULL,
    [Version]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingInstances]            PRIMARY KEY ([RatingInstanceId]),
    CONSTRAINT [FK_RatingInstances_Ratings]    FOREIGN KEY ([RatingId])          REFERENCES [dbo].[Ratings]        ([RatingId]),
    CONSTRAINT [FK_RatingInstances_Examiners]  FOREIGN KEY ([ExaminerId])        REFERENCES [dbo].[Examiners]      ([ExaminerId])
);
GO

exec spDescTable  N'RatingInstances'                      , N'Класове/Вписани квалификации - история.'
exec spDescColumn N'RatingInstances', N'RatingInstanceId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingInstances', N'RatingId'         , N'Клас/квалификация.'
exec spDescColumn N'RatingInstances', N'IssueDate'        , N'Дата на издаване.'
exec spDescColumn N'RatingInstances', N'ValidDate'        , N'Дата на валидност.'
exec spDescColumn N'RatingInstances', N'ExaminerId'       , N'Инспектор.'
exec spDescColumn N'RatingInstances', N'Limitations'      , N'Ограничения към клас (за AML и ATSML).'
exec spDescColumn N'RatingInstances', N'Subclasses'       , N'Подкласове (за ATSML).'
exec spDescColumn N'RatingInstances', N'Notes'            , N'Забележка.'
exec spDescColumn N'RatingInstances', N'NotesLatin'       , N'Забележка на поддържащ език.'
GO
