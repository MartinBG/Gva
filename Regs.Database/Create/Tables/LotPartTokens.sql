PRINT 'LotPartTokens'
GO 

CREATE TABLE [dbo].[LotPartTokens] (
    [LotPartId]    INT           NOT NULL,
    [Token]        NVARCHAR(200) NOT NULL,
    [CreateToken]  NVARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_LotPartTokens] PRIMARY KEY ([LotPartId], [Token], [CreateToken]),
    CONSTRAINT [FK_LotPartTokens_LotParts] FOREIGN KEY([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId]),
)
GO

exec spDescTable  N'LotPartTokens', N'Токени на част от партида.'
exec spDescColumn N'LotPartTokens', N'LotPartId'     , N'Идентификатор на част.'
exec spDescColumn N'LotPartTokens', N'Token'         , N'Токен.'
exec spDescColumn N'LotPartTokens', N'CreateToken'   , N'Източник на токена.'
GO
