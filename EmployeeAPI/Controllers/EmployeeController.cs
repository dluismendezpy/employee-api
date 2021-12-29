using EmployeeAPI.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //GET Data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT EmployeeID, EmployeeName, Department,
                             CONVERT(varchar(10), DateOfJoining, 120) AS DateOfJoining, PhotoFileName                    
                             FROM dbo.Employee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        // INSERT Data
        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string query = @"INSERT INTO dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName)
                             VALUES (
                                        '" + employee.EmployeeName + @"',
                                        '" + employee.Department + @"',
                                        '" + employee.DateOfJoining + @"',
                                        '" + employee.PhotoFileName + @"'
                                    )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Data Saved Successfully!!");
        }

        // UPDATE Data
        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string query = @"UPDATE dbo.Employee 
                             SET EmployeeName = '" + employee.EmployeeName + @"',
                                 Department = '" + employee.Department + @"',
                                 DateOfJoining = '" + employee.DateOfJoining + @"'
                             WHERE EmployeeID = " + employee.EmployeeID + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Data Updated Successfully!!");
        }

        // DELETE Data
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM dbo.Employee 
                             WHERE EmployeeID = " + id + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Data Remove Successfully!!");
        }
    }
}
