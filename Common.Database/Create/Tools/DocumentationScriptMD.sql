--•Select the database for which you want to generate document. 
--•Select Result to File (Ctrl+Shift+F) from Query - Results To menu. 
--•Execute (F5) the script.
--•In the Save Result dialogue box, type a file name (db.md) in the File Name text box and select All Files (*.*) in Save As Type dropdown. 
--•install pandoc from http://johnmacfarlane.net/pandoc/installing.html#windows
--•run >pandoc -s -S db.md -o db.docx

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


Print '# ' + DB_name() + '  '

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

--

print '# Спецификация на таблиците  '
print '    '
/*
print '## Списък с таблици '
print '  '
print '+-----+--------+------+  ' 
print '| Sr  | Object | Type |  ' 
print '+-----+--------+------+  ' 
While(@i <= @maxi)
begin
	select @Output =  '| ' + Cast((@i) as varchar) + ' | ' + name + ' | ' + Type + ' |  ' 
			from #Tables where id = @i
	
	print @Output
	set @i = @i + 1
end
print '+-----+--------+------+  ' 
print '  '
*/

set @i = 1
While(@i <= @maxi)
begin
	--table header
	select @Output =  '## ' + case when Type = 'Table' then 'Таблица' else 'Обект' end + ' `' + name + '` ',  @description = [description]
			from #Tables where id = @i
	
	print @Output
	print '    '
	print '**Описание**'
	print '    '
	print isnull(@description, '') + '  '
	print '    ' 	
	print '&nbsp;    ' 	
	print '    ' 	


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

	print '**Колони**'
	print '    '
	print '| **&nbsp;№&nbsp;** | **Наименование** | **Тип на данните** | **Null** | **Описание** |  ' 
	print '|------:|:-------------|:---------------|:----:|------------------------------------------|  ' 	
	
	While(@j <= @maxj)
	begin
		select @Output = '| ' + Cast((@j) as nvarchar) + ' | `' + isnull(name,'')  + '` | `' +  upper(isnull(Type,'')) + '` | ' + isnull(Nullable,'N') + ' | ' + isnull([description],'') + ' |  ' 
			from #Columns  where id = @j
		
		print 	@Output 	
		
		Set @j = @j + 1;
	end

	print '    ' 	
	print '&nbsp;    ' 	
	print '    ' 	

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

		print '**Референтни връзки**  '
		print '    '
		print '| **&nbsp;№&nbsp;** | **Наименование** | **Колони** | **Реферира към** |  ' 
		print '|--:|:-------------|:-------|:-------------|  ' 

		While(@j <= @maxj)
		begin

			select @Output = '| ' + Cast((@j) as nvarchar) + ' | `' + isnull(name,'')  + '` | `' +  isnull(col,'') + '` | `[' + isnull(refObj,'N') + '].[' +  isnull(refCol,'N') + ']` |  ' 
				from #FK  where id = @j

			print @Output
			Set @j = @j + 1;
		end
		print '    ' 	
		print '&nbsp;    ' 	
		print '    ' 	
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

		print '**Конструкции за стойности по подразбиране**  '
		print '    '

		print '| **&nbsp;№&nbsp;** | **Наименование** | **Колони** | **Стойност** |  ' 
		print '|--:|:-------------|:-------|:---------|  ' 

		While(@j <= @maxj)
		begin

			select @Output = '| ' + Cast((@j) as nvarchar) + ' | `' + isnull(name,'')  + '` | `' +  isnull(col,'') + '` | `' +  isnull(definition,'') + '` |  ' 
				from #Constraint  where id = @j

			print @Output
			Set @j = @j + 1;
		end

		print '    ' 	
		print '&nbsp;    ' 	
		print '    ' 	
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

		print '**Конструкции за проверка**  '
		print '    '
		
		print '| **&nbsp;№&nbsp;** | **Наименование** | **Колони** | **Дефиниция** |  ' 
		print '|--:|:-------------|:-------|:----------|  ' 

		While(@j <= @maxj)
		begin

			select @Output = '| ' + Cast((@j) as nvarchar) + ' | `' + isnull(name,'')  + '` | `' +  isnull(col,'') + '` | `' +  isnull(definition,'') + '` |  ' 
				from #Constraint  where id = @j
			print @Output 
			Set @j = @j + 1;
		end
		print '    ' 	
		print '&nbsp;    ' 	
		print '    ' 	
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

		print '**Тригери**  '
		print '    '

		print '| **&nbsp;№&nbsp;** | **Наименование** | **Описание** |  ' 
		print '|--:|:-------------|:---------|  ' 

		While(@j <= @maxj)
		begin
			select @Output = '| ' + Cast((@j) as nvarchar) + ' | `' + isnull(name,'')  + '` |  |  ' 
				from #Constraint  where id = @j
			print @Output 
			Set @j = @j + 1;
		end

		print '    ' 	
		print '&nbsp;    ' 	
		print '    ' 	
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

		print '**Индекси**  '
		print '    '

		print '| **&nbsp;№&nbsp;** | **Наименование** | **Тип** | **Колони** |  ' 
		print '|--:|:-------------|:----|:-------|  ' 

		set @Output = ''
		set @last = ''
		set @current = ''
		While(@j <= @maxj)
		begin
			select @current = isnull(name,'') from #Indexes  where id = @j
					 
			if @last <> @current  and @last <> ''
				begin	
				print '| ' + Cast((@sr) as nvarchar) + ' | `' + @last + '` | `' + @typ + '` | ' + @Output  + ' |  ' 
				set @Output  = ''
				set @sr = @sr + 1
				end
			
				
			select @Output = @Output + + ' `' + cols + '` ' , @typ = type
					from #Indexes  where id = @j
			
			set @last = @current 	
			Set @j = @j + 1;
		end
		if @Output <> ''
				begin	
				print '| ' + Cast((@sr) as nvarchar) + ' | `' + @last + '` | `' + @typ + '` | ' + @Output  + ' |  ' 
				end

		print '    ' 	
		print '&nbsp;    ' 	
		print '    ' 	
	end

    Set @i = @i + 1;
	--Print @Output 
end

drop table #Tables
drop table #Columns
drop table #FK
drop table #Constraint
drop table #Indexes 
set nocount off
