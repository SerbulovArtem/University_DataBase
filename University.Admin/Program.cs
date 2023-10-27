using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Admin.UI;

namespace University.Admin
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IConfiguration conection_configuration = new ConfigurationBuilder()
        .AddJsonFile("D:\\University\\SQL\\SQL_DB_LAST\\University\\University.Admin\\config.json")
        .Build();
            string connectionString = conection_configuration.GetConnectionString("DefaultConnection");
            var app = new AminMenu(connectionString);
            app.RunStartPage();
        }
    }
}
