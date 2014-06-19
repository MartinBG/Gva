print 'MosvElectronicServiceProviders'
GO 

CREATE TABLE MosvElectronicServiceProviders
(
    MosvElectronicServiceProviderId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
    Code				NVARCHAR (50)  NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_MosvElectronicServiceProviders PRIMARY KEY CLUSTERED (MosvElectronicServiceProviderId),
)
GO 

