print 'ElectronicServiceProviders'
GO 

CREATE TABLE ElectronicServiceProviders
(
    ElectronicServiceProviderId		INT            IDENTITY (1, 1) NOT NULL,
	Code				NVARCHAR (50)  NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Bulstat				NVARCHAR (50)  NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
	EndPointAddress		NVARCHAR (500) NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_ElectronicServiceProviders PRIMARY KEY CLUSTERED (ElectronicServiceProviderId),
)
GO 
