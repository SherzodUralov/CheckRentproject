using CheckRent.Models;
using CheckRent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace CheckRent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        protected readonly IConfiguration _configuration;

        private readonly IRepositoryRent _repositoryRent;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IRepositoryRent repositoryRent)
        {
            _logger = logger;

            _configuration = configuration;

            _repositoryRent = repositoryRent;   
        }

        public IActionResult Index()
        {
            //var model = _repositoryRent.GetByRents();

            //if (model == null) 
            //{
            //    return View();
            //}

            //model.FirstOrDefault(x => x.Rent_id == "1484349");

            string sqlconnectt = _configuration.GetConnectionString("WebCheck");

            List<Rent> rents = new List<Rent>();

            using (SqlConnection sqlConnection = new SqlConnection(sqlconnectt))
            {
                

                string newQuery = "SELECT*FROM Rents";

                SqlCommand command = new SqlCommand(newQuery, sqlConnection);

                command.CommandType = CommandType.Text;

                sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    rents.Add(new Rent
                    {
                        Rent_id = reader["Rent_id"].ToString(),
                        Tenant_fullname = reader["Tenant_fullname"].ToString(),
                        Start_date = Regex.Replace(reader["Start_date"].ToString(), @"[/s]", "."),
                        End_date = reader["End_date"].ToString(),
                        Faculty = reader["Faculty"].ToString(),
                        Wrote_date = reader["Wrote_date"].ToString()
                    });
                }

                command.Dispose();

                sqlConnection.Close();
            }
            

            return View(rents);
        }
        public IActionResult Delete()
        {
            string sqlconnect = _configuration.GetConnectionString("WebCheck");

            using (SqlConnection sqlConnection = new SqlConnection(sqlconnect)) 
            {
                sqlConnection.Open();

                string deleteQuery = "DELETE FROM Rents";

                SqlCommand command = new SqlCommand(deleteQuery, sqlConnection);
                
                int rowseffect = command.ExecuteNonQuery();

                command.Dispose();

                sqlConnection.Close();

            }
            
                return View();
        }    
        public IActionResult Privacy()
        {
            string excelFilePath = "C:\\Users\\User\\Desktop\\ijara.xlsx";

            string excelConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={excelFilePath};Extended Properties='Excel 12.0;HDR=YES;'";

            /* 
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {
                excelConnection.Open();

                // List nomlarini olish
                DataTable excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // List nomlarini ekranga chiqarish
                Console.WriteLine("List nomlari:");
                foreach (DataRow row in excelSchema.Rows)
                {
                    string sheetName = row["TABLE_NAME"].ToString();
                    Console.WriteLine(sheetName);
                }

                excelConnection.Close();
            }
             */

            string sqlConnectionstring = _configuration.GetConnectionString("WebCheck");

            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {
                excelConnection.Open();

                OleDbCommand excelcommand = new OleDbCommand("SELECT * FROM [Лист1$] WHERE Rent_id IS NOT NULL AND LEN(TRIM(Rent_id)) > 0", excelConnection);

                OleDbDataReader dbDataReader = excelcommand.ExecuteReader();


                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionstring))
                {
                    sqlConnection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                    {
                        bulkCopy.DestinationTableName = "Rents";

                        bulkCopy.WriteToServer(dbDataReader);
                    }

                    sqlConnection.Close();
                }

                dbDataReader.Close();

                excelConnection.Close();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}