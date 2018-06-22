using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            System.Console.WriteLine("-------------------------------------> DbContext");
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }

         public System.Collections.Generic.List<TodoItem> GetTodoItems(string date)
        {


            System.String sql = $"SELECT * FROM View_items WHERE race_date = CONVERT(datetime, '{date}', 120) AND name <> '' ORDER BY purse_usa DESC";

            var result = TodoItems
                .FromSql(sql)
                .ToList();
            return result;
        }

          public int GetWelcomInfo(string id, out string greeting, out string testDate)
        {
            int ver = -1;
            greeting = "";
            testDate = "";

            try
            {
                var sqlId = new SqlParameter("id", id);
                var sqlOutCode = new SqlParameter("ver", SqlDbType.Int);
                sqlOutCode.Direction = ParameterDirection.Output;

                var sqlOutGreeting = new SqlParameter("greeting", SqlDbType.NVarChar);
                sqlOutGreeting.Direction = ParameterDirection.Output;
                sqlOutGreeting.Size = 500;

                
               
                var sqlOutTestDate= new SqlParameter("testDate", SqlDbType.VarChar);
                sqlOutTestDate.Direction = ParameterDirection.Output;
                sqlOutTestDate.Size = 10;

                var result = this.Database.ExecuteSqlCommand("exec dbo.nyra_GetWelcomeInfo @id, @ver OUT, @greeting OUT,  @testDate OUT",
                    sqlId, sqlOutCode, sqlOutGreeting, sqlOutTestDate);

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