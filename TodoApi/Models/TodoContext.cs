using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoApi.Helper;
using System;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        static ulong Amount=0;
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
    
            // System.Console.WriteLine("------------> DbContext");
            Console.WriteLine($"DbContextOptions<TodoContext> options==>{options.GetHashCode()} {options.GetType().FullName}");
            Console.WriteLine($"TodoContext:{++Amount}");
            Console.WriteLine();

            double d = System.Math.PI; 
            double d1 = d.Round(4);
        }
        ~TodoContext(){
            Console.WriteLine($"TodoContext:{--Amount}");
        }
        
        public int MyTest { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

         public System.Collections.Generic.List<TodoItem> GetTodoItems(string date)
        {


            System.String sql = $"SELECT * FROM View_items WHERE race_date = CONVERT(datetime, '{date}', 120) AND name <> '' ORDER BY purse_usa DESC";

            var result = TodoItems
                .FromSql(sql)
                .ToList();
            return result;
        }

          public int GetWelcomInfo(string id, out string greeting, out string testDate, out string test)
        {
            int ver = -1;
            greeting = "";
            testDate = "";
            test="";

            try
            {
                var sqlId = new SqlParameter("id", id);
                var sqlOutCode = new SqlParameter("ver", SqlDbType.Int);
                sqlOutCode.Direction = ParameterDirection.Output;

                var sqlOutGreeting = new SqlParameter("greeting", SqlDbType.NVarChar);
                sqlOutGreeting.Direction = ParameterDirection.Output;
                sqlOutGreeting.Size = 500;

                var sqlOutTest = new SqlParameter("test",  SqlDbType.VarChar, 250);
                sqlOutTest.Direction = ParameterDirection.Output;
               
                var sqlOutTestDate= new SqlParameter("testDate", SqlDbType.VarChar);
                sqlOutTestDate.Direction = ParameterDirection.Output;
                sqlOutTestDate.Size = 10;

                var result = this.Database.ExecuteSqlCommand(
                    "exec dbo.nyra_GetWelcomeInfo @id, @ver OUT, @greeting OUT,  @testDate OUT, @test OUT",
                    sqlId, sqlOutCode, sqlOutGreeting, sqlOutTestDate,sqlOutTest);

                ver = (int)sqlOutCode.Value;
                greeting = (string)sqlOutGreeting.Value;
                testDate = (string)sqlOutTestDate.Value;
            }

            catch (System.Exception ex)
            {
                throw ex;
            }

            return ver;
        }

    }
}