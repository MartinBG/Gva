--•Select the database for which you want to generate document. 
--•Select Result to File (Ctrl+Shift+F) from Query - Results To menu. 
--•Execute (F5) the script.
--•In the Save Result dialogue box, type a file name (filename.htm/filename.html) in the File Name text box and select All Files (*.*) in Save As Type dropdown. 

--//SQL Database documentation script
--//Author: Nitin Patel, Email: nitinpatel31@gmail.com
--//Date:18-Feb-2008
--//Description: T-SQL script to generate the database document for SQL server 2000/2005
--//http://www.codeproject.com/KB/database/SQL_DB_DOCUMENTATION.aspx
Declare @i Int, @maxi Int
Declare @j Int, @maxj Int
Declare @sr int
Declare @Output nvarchar(4000)
--Declare @tmpOutput nvarchar(max)
Declare @SqlVersion nvarchar(5)
Declare @last nvarchar(155), @current nvarchar(255), @typ nvarchar(255), @description nvarchar(4000)

create Table #Tables  (id int identity(1, 1), Object_id int, Name nvarchar(155), Type nvarchar(20), [description] nvarchar(4000))
create Table #Columns (id int identity(1,1), Name nvarchar(155), Type nvarchar(155), Nullable nvarchar(2), [description] nvarchar(4000))
create Table #Fk(id int identity(1,1), Name nvarchar(155), col nvarchar(155), refObj nvarchar(155), refCol nvarchar(155))
create Table #Constraint(id int identity(1,1), Name nvarchar(155), col nvarchar(155), definition nvarchar(1000))
create Table #Indexes(id int identity(1,1), Name nvarchar(155), Type nvarchar(25), cols nvarchar(1000))

 If (substring(@@VERSION, 1, 25 ) = 'Microsoft SQL Server 2005')
	set @SqlVersion = '2005'
else if (substring(@@VERSION, 1, 26 ) = 'Microsoft SQL Server  2000')
	set @SqlVersion = '2000'
else 
	set @SqlVersion = '2005'


Print '<head>'	
Print '<title>::' + DB_name() + '::</title>'
Print '<style>'
    
Print '		body {'
Print '		font-family:verdana;'
Print '		font-size:9pt;'
Print '		}'
		
Print '		td {'
Print '		font-family:verdana;'
Print '		font-size:9pt;'
Print '		}'
		
Print '		th {'
Print '		font-family:verdana;'
Print '		font-size:9pt;'
Print '		background:#d3d3d3;'
Print '		}'
Print '		table'
Print '		{'
Print '		background:#d3d3d3;'
Print '		}'
Print '		tr'
Print '		{'
Print '		background:#ffffff;'
Print '		}'
Print '	</style>'
Print '</head>'
Print '<body>'

set nocount on
	if @SqlVersion = '2000' 
		begin
		insert into #Tables (Object_id, Name, Type, [description])
			--FOR 2000
			select object_id(table_name),  '[' + table_schema + '].[' + table_name + ']',  
			case when table_type = 'BASE TABLE'  then 'Table'   else 'View' end,
			cast(p.value as nvarchar(4000))
			from information_schema.tables t
			left outer join sysproperties p on p.id = object_id(t.table_name) and smallid = 0 and p.name = 'MS_Description' 
			order by table_type, table_schema, table_name
		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Tables (Object_id, Name, Type, [description])
		--FOR 2005
		Select o.object_id,  '[' + s.name + '].[' + o.name + ']', 
				case when type = 'V' then 'View' when type = 'U' then 'Table' end,  
				cast(p.value as nvarchar(4000))
				from sys.objects o 
					left outer join sys.schemas s on s.schema_id = o.schema_id 
					left outer join sys.extended_properties p on p.major_id = o.object_id and minor_id = 0 and p.name = 'MS_Description' 
				where type in ('U', 'V') 
				order by type, s.name, o.name
		end
Set @maxi = @@rowcount
set @i = 1

print '<table border="0" cellspacing="0" cellpadding="0" width="550px" align="center"><tr><td colspan="3" style="height:50;font-size:14pt;text-align:center;"><a name="index"></a><b>Index</b></td></tr></table>'
print '<table border="0" cellspacing="1" cellpadding="0" width="550px" align="center"><tr><th>Sr</th><th>Object</th><th>Type</th></tr>' 
While(@i <= @maxi)
begin
	select @Output =  '<tr><td align="center">' + Cast((@i) as varchar) + '</td><td><a href="#' + Type + ':' + name + '">' + name + '</a></td><td>' + Type + '</td></tr>' 
			from #Tables where id = @i
	
	print @Output
	set @i = @i + 1
end
print '</table><br />'

set @i = 1
While(@i <= @maxi)
begin
	--table header
	select @Output =  '<tr><th align="left"><a name="' + Type + ':' + name + '"></a><b>' + Type + ':' + name + '</b></th></tr>',  @description = [description]
			from #Tables where id = @i
	
	print '<br /><br /><br /><table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td align="right"><a href="#index"></a></td></tr>'
	print @Output
	print '</table><br />'
	print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Description</b></td></tr><tr><td>' + isnull(@description, '') + '</td></tr></table><br />' 

	--table columns
	truncate table #Columns 
	if @SqlVersion = '2000' 
		begin
		insert into #Columns  (Name, Type, Nullable, [description])
		--FOR 2000
		Select c.name, 
					type_name(xtype) + (
					case when (type_name(xtype) = 'nvarchar' or type_name(xtype) = 'nnvarchar' or type_name(xtype) ='char' or type_name(xtype) ='nchar')
						then '(' + cast(length as nvarchar) + ')' 
					 when type_name(xtype) = 'decimal'  
							then '(' + cast(prec as nvarchar) + ',' + cast(scale as nvarchar)   + ')' 
					else ''
					end				
					), 
					case when isnullable = 1 then 'Y' else 'N'  end, 
					cast(p.value as nvarchar(8000))
				from syscolumns c
					inner join #Tables t on t.object_id = c.id
					left outer join sysproperties p on p.id = c.id and p.smallid = c.colid and p.name = 'MS_Description' 
				where t.id = @i
				order by c.colorder
		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Columns  (Name, Type, Nullable, [description])
		--FOR 2005	
		Select c.name, 
					type_name(user_type_id) + (
					case when (type_name(user_type_id) = 'nvarchar' or type_name(user_type_id) = 'nnvarchar' or type_name(user_type_id) ='char' or type_name(user_type_id) ='nchar')
						then '(' + cast(max_length as nvarchar) + ')' 
					 when type_name(user_type_id) = 'decimal'  
							then '(' + cast([precision] as nvarchar) + ',' + cast(scale as nvarchar)   + ')' 
					else ''
					end				
					), 
					case when is_nullable = 1 then 'Y' else 'N'  end,
					cast(p.value as nvarchar(4000))
		from sys.columns c
				inner join #Tables t on t.object_id = c.object_id
				left outer join sys.extended_properties p on p.major_id = c.object_id and p.minor_id  = c.column_id and p.name = 'MS_Description' 
		where t.id = @i
		order by c.column_id
		end
	Set @maxj =   @@rowcount
	set @j = 1

	print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Table Columns</b></td></tr></table>' 
	print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Datatype</th><th>Null</th><th>Description</th></tr>' 
	
	While(@j <= @maxj)
	begin
		select @Output = '<tr><td width="20px" align="center">' + Cast((@j) as nvarchar) + '</td><td width="150px">' + isnull(name,'')  + '</td><td width="120px">' +  upper(isnull(Type,'')) + '</td><td width="30px" align="center">' + isnull(Nullable,'N') + '</td><td width="290px">' + isnull([description],'') + '</td></tr>' 
			from #Columns  where id = @j
		
		print 	@Output 	
		Set @j = @j + 1;
	end

	print '</table><br />'

	--reference key
	truncate table #FK
	if @SqlVersion = '2000' 
		begin
		insert into #FK  (Name, col, refObj, refCol)
	--		FOR 2000
		select object_name(constid), s.name,  object_name(rkeyid) ,  s1.name  
				from sysforeignkeys f
					inner join sysobjects o on o.id = f.constid
					inner join syscolumns s on s.id = f.fkeyid and s.colorder = f.fkey
					inner join syscolumns s1 on s1.id = f.rkeyid and s1.colorder = f.rkey
					inner join #Tables t on t.object_id = f.fkeyid
				where t.id = @i
				order by 1
		end	
	else if @SqlVersion = '2005' 
		begin
		insert into #FK  (Name, col, refObj, refCol)
--		FOR 2005
		select f.name, COL_NAME (fc.parent_object_id, fc.parent_column_id) , object_name(fc.referenced_object_id) , COL_NAME (fc.referenced_object_id, fc.referenced_column_id)     
		from sys.foreign_keys f
			inner  join  sys.foreign_key_columns  fc  on f.object_id = fc.constraint_object_id	
			inner join #Tables t on t.object_id = f.parent_object_id
		where t.id = @i
		order by f.name
		end
	
	Set @maxj =   @@rowcount
	set @j = 1
	if (@maxj >0)
	begin

		print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Refrence Keys</b></td></tr></table>' 
		print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Column</th><th>Reference To</th></tr>' 

		While(@j <= @maxj)
		begin

			select @Output = '<tr><td width="20px" align="center">' + Cast((@j) as nvarchar) + '</td><td width="130px">' + isnull(name,'')  + '</td><td width="130px">' +  isnull(col,'') + '</td><td>[' + isnull(refObj,'N') + '].[' +  isnull(refCol,'N') + ']</td></tr>' 
				from #FK  where id = @j

			print @Output
			Set @j = @j + 1;
		end

		print '</table><br />'
	end

	--Default Constraints 
	truncate table #Constraint
	if @SqlVersion = '2000' 
		begin
		insert into #Constraint  (Name, col, definition)
		select object_name(c.constid), col_name(c.id, c.colid), s.text
				from sysconstraints c
					inner join #Tables t on t.object_id = c.id
					left outer join syscomments s on s.id = c.constid
				where t.id = @i 
				and 
				convert(nvarchar,+ (c.status & 1)/1)
				+ convert(nvarchar,(c.status & 2)/2)
				+ convert(nvarchar,(c.status & 4)/4)
				+ convert(nvarchar,(c.status & 8)/8)
				+ convert(nvarchar,(c.status & 16)/16)
				+ convert(nvarchar,(c.status & 32)/32)
				+ convert(nvarchar,(c.status & 64)/64)
				+ convert(nvarchar,(c.status & 128)/128) = '10101000'
		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Constraint  (Name, col, definition)
		select c.name,  col_name(parent_object_id, parent_column_id), c.definition 
		from sys.default_constraints c
			inner join #Tables t on t.object_id = c.parent_object_id
		where t.id = @i
		order by c.name
		end
	Set @maxj =   @@rowcount
	set @j = 1
	if (@maxj >0)
	begin

		print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Default Constraints</b></td></tr></table>' 
		print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Column</th><th>Value</th></tr>' 

		While(@j <= @maxj)
		begin

			select @Output = '<tr><td width="20px" align="center">' + Cast((@j) as nvarchar) + '</td><td width="230px">' + isnull(name,'')  + '</td><td width="130px">' +  isnull(col,'') + '</td><td>' +  isnull(definition,'') + '</td></tr>' 
				from #Constraint  where id = @j

			print @Output
			Set @j = @j + 1;
		end

	print '</table><br />'
	end


	--Check  Constraints
	truncate table #Constraint
	if @SqlVersion = '2000' 
		begin
		insert into #Constraint  (Name, col, definition)
			select object_name(c.constid), col_name(c.id, c.colid), s.text
				from sysconstraints c
					inner join #Tables t on t.object_id = c.id
					left outer join syscomments s on s.id = c.constid
				where t.id = @i 
				and ( convert(nvarchar,+ (c.status & 1)/1)
					+ convert(nvarchar,(c.status & 2)/2)
					+ convert(nvarchar,(c.status & 4)/4)
					+ convert(nvarchar,(c.status & 8)/8)
					+ convert(nvarchar,(c.status & 16)/16)
					+ convert(nvarchar,(c.status & 32)/32)
					+ convert(nvarchar,(c.status & 64)/64)
					+ convert(nvarchar,(c.status & 128)/128) = '00101000' 
				or convert(nvarchar,+ (c.status & 1)/1)
					+ convert(nvarchar,(c.status & 2)/2)
					+ convert(nvarchar,(c.status & 4)/4)
					+ convert(nvarchar,(c.status & 8)/8)
					+ convert(nvarchar,(c.status & 16)/16)
					+ convert(nvarchar,(c.status & 32)/32)
					+ convert(nvarchar,(c.status & 64)/64)
					+ convert(nvarchar,(c.status & 128)/128) = '00100100')

		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Constraint  (Name, col, definition)
			select c.name,  col_name(parent_object_id, parent_column_id), definition 
			from sys.check_constraints c
				inner join #Tables t on t.object_id = c.parent_object_id
			where t.id = @i
			order by c.name
		end
	Set @maxj =   @@rowcount
	
	set @j = 1
	if (@maxj >0)
	begin

		print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Check  Constraints</b></td></tr></table>' 
		print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Column</th><th>Definition</th></tr>' 

		While(@j <= @maxj)
		begin

			select @Output = '<tr><td width="20px" align="center">' + Cast((@j) as nvarchar) + '</td><td width="230px">' + isnull(name,'')  + '</td><td width="130px">' +  isnull(col,'') + '</td><td>' +  isnull(definition,'') + '</td></tr>' 
				from #Constraint  where id = @j
			print @Output 
			Set @j = @j + 1;
		end

		print '</table><br />'
	end


	--Triggers 
	truncate table #Constraint
	if @SqlVersion = '2000' 
		begin
		insert into #Constraint  (Name)
			select tr.name
			FROM sysobjects tr
				inner join #Tables t on t.object_id = tr.parent_obj
			where t.id = @i and tr.type = 'TR'
			order by tr.name
		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Constraint  (Name)
			SELECT tr.name
			FROM sys.triggers tr
				inner join #Tables t on t.object_id = tr.parent_id
			where t.id = @i
			order by tr.name
		end
	Set @maxj =   @@rowcount
	
	set @j = 1
	if (@maxj >0)
	begin

		print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Triggers</b></td></tr></table>' 
		print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Description</th></tr>' 

		While(@j <= @maxj)
		begin
			select @Output = '<tr><td width="20px" align="center">' + Cast((@j) as nvarchar) + '</td><td width="130px">' + isnull(name,'')  + '</td><td></td></tr>' 
				from #Constraint  where id = @j
			print @Output 
			Set @j = @j + 1;
		end

		print '</table><br />'
	end

	--Indexes 
	truncate table #Indexes
	if @SqlVersion = '2000' 
		begin
		insert into #Indexes  (Name, type, cols)
			select i.name, case when i.indid = 0 then 'Heap' when i.indid = 1 then 'Clustered' else 'Nonclustered' end , c.name 
			from sysindexes i
				inner join sysindexkeys k  on k.indid = i.indid  and k.id = i.id
				inner join syscolumns c on c.id = k.id and c.colorder = k.colid
				inner join #Tables t on t.object_id = i.id
			where t.id = @i and i.name not like '_WA%'
			order by i.name, i.keycnt
		end
	else if @SqlVersion = '2005' 
		begin
		insert into #Indexes  (Name, type, cols)
			select i.name, case when i.type = 0 then 'Heap' when i.type = 1 then 'Clustered' else 'Nonclustered' end,  col_name(i.object_id, c.column_id)
				from sys.indexes i 
					inner join sys.index_columns c on i.index_id = c.index_id and c.object_id = i.object_id 
					inner join #Tables t on t.object_id = i.object_id
				where t.id = @i
				order by i.name, c.column_id
		end

	Set @maxj =   @@rowcount
	
	set @j = 1
	set @sr = 1
	if (@maxj >0)
	begin

		print '<table border="0" cellspacing="0" cellpadding="0" width="600px"><tr><td><b>Indexes</b></td></tr></table>' 
		print '<table border="0" cellspacing="1" cellpadding="0" width="600px"><tr><th></th><th>Name</th><th>Type</th><th>Columns</th></tr>' 
		set @Output = ''
		set @last = ''
		set @current = ''
		While(@j <= @maxj)
		begin
			select @current = isnull(name,'') from #Indexes  where id = @j
					 
			if @last <> @current  and @last <> ''
				begin	
				print '<tr><td width="20px" align="center">' + Cast((@sr) as nvarchar) + '</td><td width="130px">' + @last + '</td><td width="130px">' + @typ + '</td><td>' + @Output  + '</td></tr>' 
				set @Output  = ''
				set @sr = @sr + 1
				end
			
				
			select @Output = @Output + cols + '<br />' , @typ = type
					from #Indexes  where id = @j
			
			set @last = @current 	
			Set @j = @j + 1;
		end
		if @Output <> ''
				begin	
				print '<tr><td width="20px" align="center">' + Cast((@sr) as nvarchar) + '</td><td width="130px">' + @last + '</td><td width="130px">' + @typ + '</td><td>' + @Output  + '</td></tr>' 
				end

		print '</table><br />'
	end

    Set @i = @i + 1;
	--Print @Output 
end


Print '</body>'
Print '</html>'

drop table #Tables
drop table #Columns
drop table #FK
drop table #Constraint
drop table #Indexes 
set nocount off