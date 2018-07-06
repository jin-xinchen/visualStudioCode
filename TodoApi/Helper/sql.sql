DECLARE @ColumA varchar(250) = null;
DECLARE @Colum2 varchar(250) = null;--	Tock Table Name
DECLARE @Colum3 nvarchar(50) = N'TestOne';
DECLARE @SPACE_STR nvarchar(50) = N'      ';

DECLARE @TableName varchar(250) =  N'dbo.TestTable';
DECLARE @sql varchar(8000);
DECLARE @insertStr varchar(8000);
DECLARE @N int=0;

Set @insertStr ='INSERT INTO '+@TableName+' (test01,test02,test03) VALUES';
/**********************************/
set @ColumA = N'BTl.1';
set @Colum2 = N'BT001';
	IF NOT EXISTS (SELECT Id FROM dbo.TestTable WHERE ColumA = @ColumA and Colum2=@Colum2)
	BEGIN
		INSERT INTO TestTable (ColumA,Colum2,Colum3) 
		VALUES (@ColumA, @Colum2, @Colum3)
		Set @N = @N + 1;
 		print CONVERT(varchar(10), @N ) +'   ' + @ColumA +'   ' + @Colum2;
	END

/**********************************/
set @ColumA = N'BTl.2';
set @Colum2 = N'BT002';

	IF NOT EXISTS (SELECT Id FROM dbo.TestTable WHERE ColumA = @ColumA and Colum2=@Colum2)
	BEGIN
	 print @ColumA;
       Set @sql = @insertStr +'(N'''+@ColumA+''',N'''+@Colum2+''', N'''+@Colum3+''')';
	   print @sql;
        exec (@sql)
		Set @N = @N + 1;
 		print CONVERT(varchar(10), @N ) +'   ' + @ColumA +'   ' + @Colum2;
	END

/**********************************/
set @ColumA = N'BT.3';
set @Colum2 = N'BT001';

	IF NOT EXISTS (SELECT Id FROM dbo.TestTable WHERE ColumA = @ColumA and Colum2=@Colum2)
	BEGIN
       Set @sql = @insertStr +'(N'''+@ColumA+''',N'''+@Colum2+''', N'''+@Colum3+''')';
       exec (@sql)
	 print @sql;
		Set @N = @N + 1;
 		print CONVERT(varchar(10), @N ) +'   ' + @ColumA +'   ' + @Colum2;
	END
