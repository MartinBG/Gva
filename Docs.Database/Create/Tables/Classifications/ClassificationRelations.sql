print 'ClassificationRelations'
GO 

CREATE TABLE [dbo].[ClassificationRelations]
(
    ClassificationRelationId    INT         IDENTITY (1, 1) NOT NULL,
    ClassificationId            INT         NOT NULL,
    ParentClassificationId      INT         NULL,
    RootClassificationId        INT         NULL,
    Version                     ROWVERSION  NOT NULL,
    CONSTRAINT PK_ClassificationRelations PRIMARY KEY CLUSTERED (ClassificationRelationId),
    CONSTRAINT [FK_ClassificationRelations_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
    CONSTRAINT [FK_ClassificationRelations_ClassificationsParent] FOREIGN KEY ([ParentClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
    CONSTRAINT [FK_ClassificationRelations_ClassificationsRoot] FOREIGN KEY ([RootClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
)
GO 

exec spDescTable  N'ClassificationRelations', N'Свързаност на класификационните схеми.'
exec spDescColumn N'ClassificationRelations', N'ClassificationRelationId', N'Уникален системно генериран идентификатор.'
