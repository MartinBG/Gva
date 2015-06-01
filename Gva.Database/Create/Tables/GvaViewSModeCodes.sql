PRINT 'GvaViewSModeCodes'
GO 

CREATE TABLE [dbo].[GvaViewSModeCodes] (
    [LotId]                  INT           NOT NULL,
    [TypeId]                 INT           NOT NULL,
    [CodeHex]                NVARCHAR(50)  NOT NULL,
    [Note]                   NVARCHAR(MAX) NULL,
    [AircraftId]             INT           NULL,
    [Applicant]              NVARCHAR(50)  NULL,
    [TheirNumber]            NVARCHAR(50)  NULL,
    [TheirDate]              DATETIME2     NULL,
    [CaaNumber]              NVARCHAR(50)  NULL,
    [CaaDate]                DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewSModeCodes]            PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewSModeCodes_Lots]       FOREIGN KEY ([LotId])   REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewSModeCodes_NomValues]  FOREIGN KEY ([TypeId])  REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_IDX_GvaViewSModeCodes_CodeHex]
ON [dbo].[GvaViewSModeCodes](CodeHex)
GO

exec spDescTable  N'GvaViewSModeCodes', N'S mode кодове.'
exec spDescColumn N'GvaViewSModeCodes', N'LotId'             , N'Идентификатор на партида на s mode кода.'
exec spDescColumn N'GvaViewSModeCodes', N'TypeId'            , N'Тип на кода (за ВС, РВД или военнен).'
exec spDescColumn N'GvaViewSModeCodes', N'CodeHex'           , N'S Mode кода в шестнайсетична бройна система.'
exec spDescColumn N'GvaViewSModeCodes', N'Note'              , N'Бележки.'
exec spDescColumn N'GvaViewSModeCodes', N'AircraftId'        , N'Идентификационен номер на ВС на което е разпределен този код.'
exec spDescColumn N'GvaViewSModeCodes', N'Applicant'         , N'Заявител.'
exec spDescColumn N'GvaViewSModeCodes', N'TheirNumber'       , N'Техен номер.'
exec spDescColumn N'GvaViewSModeCodes', N'TheirDate'         , N'Тяхна дата.'
exec spDescColumn N'GvaViewSModeCodes', N'CaaNumber'         , N'ГВА номер.'
exec spDescColumn N'GvaViewSModeCodes', N'CaaDate'           , N'ГВА дата.'
GO
