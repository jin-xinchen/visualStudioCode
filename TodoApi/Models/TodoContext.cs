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
    }
}