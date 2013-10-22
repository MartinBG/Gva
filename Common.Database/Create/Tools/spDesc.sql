IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDescTable')
	BEGIN
		DROP  Procedure  spTableDesc
	END

GO

CREATE Procedure spDescTable
	(
		@Table nvarchar(200)
		, @value nvarchar(500)
	)
AS
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Table

GO



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDescColumn')
	BEGIN
		DROP  Procedure  spDescColumn
	END

GO

CREATE Procedure spDescColumn
	(
		@Table nvarchar(200)
		, @column nvarchar(200)
		, @value nvarchar(500)
	)
AS
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@value , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@Table, @level2type=N'COLUMN',@level2name=@column

GO
