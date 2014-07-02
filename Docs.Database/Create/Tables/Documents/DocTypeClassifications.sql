print 'DocTypeClassifications' 
GO 

CREATE TABLE DocTypeClassifications
(
    DocTypeClassificationId		    INT            IDENTITY (1, 1) NOT NULL,
	DocTypeId	            INT            NOT NULL,
    DocDirectionId	        INT            NOT NULL,
    ClassificationId	    INT            NOT NULL,
    IsActive		        BIT            NOT NULL,
	IsInherited				BIT			   NOT NULL DEFAULT(1),
    Version			        ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocTypeClassifications PRIMARY KEY CLUSTERED (DocTypeClassificationId),
	CONSTRAINT [FK_DocTypeClassifications_DocTypes] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocTypes] ([DocTypeId]),
    CONSTRAINT [FK_DocTypeClassifications_DocDirections] FOREIGN KEY ([DocDirectionId]) REFERENCES [dbo].[DocDirections] ([DocDirectionId]),
    CONSTRAINT [FK_DocTypeClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
)
GO 

exec spDescTable  N'DocTypeClassifications', N'Указания за автоматично класифициране на документи.'
exec spDescColumn N'DocTypeClassifications', N'DocTypeClassificationId', N'Уникален системно генериран идентификатор.'
