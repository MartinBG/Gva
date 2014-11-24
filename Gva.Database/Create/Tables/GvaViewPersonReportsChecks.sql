PRINT 'GvaViewPersonReportsChecks'
GO 

CREATE TABLE [dbo].[GvaViewPersonReportsChecks] (
    [ReportLotId]                  INT NOT NULL,
    [ReportPartIndex]              INT NOT NULL,
    [CheckLotId]                   INT NOT NULL,
    [CheckPartIndex]               INT NOT NULL,
    CONSTRAINT [PK_GvaViewPersonReportsChecks]                      PRIMARY KEY ([ReportLotId], [ReportPartIndex], [CheckLotId], [CheckPartIndex]),
    CONSTRAINT [FK_GvaViewPersonReportsChecks_GvaViewPersonReports] FOREIGN KEY ([ReportLotId], [ReportPartIndex])  REFERENCES [dbo].[GvaViewPersonReports] ([LotId], [PartIndex])
  )
GO
