using System;

namespace TodoApi.Helper
{
    class WriteSql
    {
        public static void Start()
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;
            foreach (var color in colors)
            {
                if (color == currentBackground) continue;

                Console.ForegroundColor = ConsoleColor.Red;
                // Console.BackgroundColor = ConsoleColor.Red;;
                // Console.WriteLine("   The foreground color is {0}.", color);

            }

            System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"D:\jinchen\projects\webapi2\sql.sql", true);

            while (true) // Loop indefinitely
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter input DeviceName:"); // Prompt
                Console.ResetColor();
                string DeviceName = Console.ReadLine(); // Get string from user
                if (DeviceName == "exit") // Check string
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("You typed ");
                    Console.Write(DeviceName.Length);
                    Console.WriteLine(" character(s)");
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tock Table Name:");
                Console.ResetColor();
                string TableConfig = Console.ReadLine();
                if (TableConfig == "exit") // Check string
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("You typed ");
                    Console.Write(TableConfig.Length);
                    Console.WriteLine(" character(s)");
                    Console.ResetColor();
                }
                appendSqlIntoFile(file, DeviceName, TableConfig);

            }
            file.Flush();
            file.Close();

            //  using (System.IO.StreamWriter file = 
            //             new System.IO.StreamWriter(@"D:\jinchen\projects\webapi2\20180705_enter_all_device_into_Devices_table_nt4.txt", true))
            //         {
            //             file.WriteLine("Fourth line");
            //         }
        }

        public static void appendSqlIntoFile(System.IO.StreamWriter file, string DeviceName, string TableConfig)
        {
            DeviceName = DeviceName.Trim();
            TableConfig = TableConfig.Trim();

            file.WriteLine($"set @DeviceName = N'{DeviceName}';");
            file.WriteLine($"set @TableConfig = N'{TableConfig}';");
            file.WriteLine("IF NOT EXISTS (SELECT Id FROM dbo.Devices WHERE DeviceName = @DeviceName and TableConfig=@TableConfig)");
            file.WriteLine(" BEGIN");
            file.WriteLine("   Set @sql = @insertStr +'(N'''+@DeviceName+''',N'''+@TableConfig+''', N'''+@Platform+''')';");
            file.WriteLine("   exec (@sql)");
            file.WriteLine("   print @sql;");
            file.WriteLine("   Set @N = @N + 1;");
            file.WriteLine("   print CONVERT(varchar(10), @N ) +@SPACE_STR + @DeviceName + @SPACE_STR + @TableConfig;");
            file.WriteLine(" END");
            file.WriteLine("/**********************************/");

        }
        public static void GenerateSQLFile(string DeviceName, string TableConfig) { 
            using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@"D:\jinchen\projects\webapi2\sql.sql", true))
        {
            appendSqlIntoFile(file, DeviceName, TableConfig);
        }
        }
        public static void ReadFile()
        {
            string DeviceName;
            string TableConfig;

            using (System.IO.StreamReader file =
            new System.IO.StreamReader(@"D:\jinchen\projects\webapi2\source1.txt", true))
            {
                try
                {
                    int counter = 0;
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        //  System.Console.WriteLine(line);

                        string[] sA = line.Split('\t');

                        int ln = sA.Length;
                        if (ln > 0)
                        {
                            DeviceName = sA[0] as string;
                            DeviceName = DeviceName.Trim();
                            if (DeviceName == "")
                            {
                                continue;
                            }
                            
                            if (ln > 1)
                            {
                                for (int i = 1; i < ln; i++)
                                {
                                    TableConfig = sA[i] as string;
                                    TableConfig = TableConfig.Trim();
                                    if (TableConfig == "")
                                    {
                                        continue;
                                    }
                                    Console.Write(DeviceName + "  ");
                                    GenerateSQLFile(DeviceName, TableConfig);
                                    Console.Write(TableConfig + "  ");
                                    Console.WriteLine("  ");
                                    counter++;
                                }

                            }

                        }
                        
                        
                    }

                    Console.WriteLine(counter);
                }
                catch (System.NullReferenceException e)
                {
                    file.Close();
                }
            }
        }
    }
}