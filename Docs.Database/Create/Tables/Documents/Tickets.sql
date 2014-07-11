print 'Tickets'
GO 

CREATE TABLE Tickets
(
	TicketId	            UNIQUEIDENTIFIER NOT NULL ,

    [DocFileId]             INT NULL,
    [BlobOldKey]            UNIQUEIDENTIFIER  NULL,
    [BlobNewKey]            UNIQUEIDENTIFIER  NULL,

	[DocTypeUri]            NVARCHAR(50) NULL,
	[AbbcdnKey]             UNIQUEIDENTIFIER  NULL,

    [VisualizationMode]     INT NULL,

    CONSTRAINT PK_Tickets PRIMARY KEY CLUSTERED (TicketId),
)
GO 
