PRINT 'GvaViewPersonReportsChecks'
GO 

CREATE TABLE [dbo].[GvaViewPersonReportsChecks] (
	[GvaViewPersonReportsChecksId] INT NOT NULL IDENTITY,
    [ReportLotId]                  INT NOT NULL,
    [ReportPartIndex]              INT NOT NULL,
    [CheckLotId]                   INT NOT NULL,
    [CheckPartIndex]               INT NOT NULL,
    CONSTRAINT [PK_GvaViewPersonReportsChecks]                      PRIMARY KEY ([GvaViewPersonReportsChecksId], [ReportLotId], [CheckLotId], [ReportPartIndex], [CheckPartIndex]),
    CONSTRAINT [FK_GvaViewPersonReportsChecks_GvaViewPersonReports] FOREIGN KEY ([ReportLotId], [ReportPartIndex])  REFERENCES [dbo].[GvaViewPersonReports] ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonReportsChecks_GvaViewPersonChecks]  FOREIGN KEY ([CheckLotId], [CheckPartIndex])    REFERENCES [dbo].[GvaViewPersonChecks] ([LotId], [PartIndex])
  )
GO
