print 'Tickets'
GO 

CREATE TABLE Tickets
(
	TicketId	            UNIQUEIDENTIFIER NOT NULL ,

    [DocFileId]             INT NOT NULL,
    [OldKey]                UNIQUEIDENTIFIER  NOT NULL,
    [NewKey]                UNIQUEIDENTIFIER  NULL,
    [VisualizationMode]     INT NULL,

    CONSTRAINT PK_Tickets PRIMARY KEY CLUSTERED (TicketId),
)
GO 
