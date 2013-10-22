PRINT 'GvaApplications'
GO 

CREATE TABLE [dbo].[GvaApplications] (
    [GvaApplicationId]    INT  NOT NULL IDENTITY,
    [DocId]               INT  NOT NULL,
    [LotId]               INT  NOT NULL,
    CONSTRAINT [PK_GvaApplications]      PRIMARY KEY ([GvaApplicationId]),
    CONSTRAINT [FK_GvaApplications_Lots] FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaApplications_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId])
)
GO

exec spDescTable  N'GvaApplications', N'Заявления.'
exec spDescColumn N'GvaApplications', N'GvaApplicationId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaApplications', N'DocId'            , N'Преписка.'
exec spDescColumn N'GvaApplications', N'LotId'            , N'Партида.'
GO
