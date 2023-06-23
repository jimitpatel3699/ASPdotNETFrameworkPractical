using Practical12.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Practical12.Controllers
{
    public class EmployeeTestThreeController : Controller
    {
        private string _connectionstring= ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        public ActionResult Index()
        {
            List<EmployeeTestThree> employees = GetEmployees(); 
            return View(employees);
        }
        public ActionResult Create(int? id) 
        {
            var designations = GetDesignations();
            if (id==0 || id==null)
            {
                EmployeeTestThree employees = GetEmployees(id ?? 0);
                employees.Designations = designations;
                ViewBag.btnval = "Create";
                return View(employees);
            }
            else
            {
                ViewBag.btnval = "Update";
                EmployeeTestThree employees = GetEmployees(id ??0);
                employees.Designations = designations;
                return View(employees);
            }
        }
        [HttpPost]
        public ActionResult Create(EmployeeTestThree employee)
        {
            if(DateTime.Now.Year-employee.DOB.Year<18)
            {
                ModelState.AddModelError("invaliddate", "Invalid DOB");
            }
            if(employee.Id!=null ||employee.Id!=0)
            {
                ViewBag.btnval = "Update";
            }
            else
            {
                ViewBag.btnval = "Create";
            }
            if(ModelState.IsValid)
            {
                int status=0;
                if(employee.Id!=0)
                {
                    status = UpdateEmployee(employee);
                }
                else
                {
                    status = CreateEmployee(employee);
                }
                if (status > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if(id==0 || id== null)
            {
                HttpNotFound();
            }
            int status= DeleteEmployee(id);
            return RedirectToAction("Index");    
        }
        public ActionResult Queries()
        {
            EmployeeTestThree employee = null;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM EmployeeTestThree ORDER BY Salary DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", con);
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    employee = new EmployeeTestThree()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName")),
                        MiddleName = dataReader.IsDBNull(dataReader.GetOrdinal("MiddleName")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MiddleName")),
                        LastName = dataReader.GetString(dataReader.GetOrdinal("LastName")),
                        Salary = dataReader.GetDecimal(dataReader.GetOrdinal("Salary")),
                       
                    };
                    break;
                }
            }
            return View(employee);
        }
            [NonAction]
        private List<Designation> GetDesignations()
        {
            List<Designation> Designations = new List<Designation>();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Designations", con);
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Designations.Add(new Designation()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        DesignationName = dataReader.GetString(dataReader.GetOrdinal("DesignationName")),
                    });
                }
            }
            return Designations;
        }

        [NonAction]
        private List<EmployeeTestThree> GetEmployees()
        {
            List<EmployeeTestThree> employees = new List<EmployeeTestThree>();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("SELECT EmployeeTestThree.Id, FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, EmployeeTestThree.DesignationId, Designations.DesignationName FROM EmployeeTestThree INNER JOIN Designations ON EmployeeTestThree.DesignationId = Designations.Id", con);
                
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    employees.Add(new EmployeeTestThree()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName")),
                        MiddleName = dataReader.IsDBNull(dataReader.GetOrdinal("MiddleName")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MiddleName")),
                        LastName = dataReader.GetString(dataReader.GetOrdinal("LastName")),
                        DOB = dataReader.GetDateTime(dataReader.GetOrdinal("DOB")),
                        Mobile = dataReader.GetString(dataReader.GetOrdinal("MobileNumber")),
                        Salary = dataReader.GetDecimal(dataReader.GetOrdinal("Salary")),
                        Address = dataReader.IsDBNull(dataReader.GetOrdinal("Address")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("Address")),
                        Designation = dataReader.GetInt32(dataReader.GetOrdinal("DesignationId")),
                        DesignationName= dataReader.GetString(dataReader.GetOrdinal("DesignationName"))
                    });
                }
            }
            return employees;
        }
        [NonAction]
        private EmployeeTestThree GetEmployees(int id)
        {
            EmployeeTestThree employees =new EmployeeTestThree();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                
                SqlCommand command = new SqlCommand("SELECT * FROM EmployeeTestThree WHERE Id=@Id", con);
                command.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                     employees = new EmployeeTestThree()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        FirstName = dataReader.GetString(dataReader.GetOrdinal("FirstName")),
                        MiddleName = dataReader.IsDBNull(dataReader.GetOrdinal("MiddleName")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MiddleName")),
                        LastName = dataReader.GetString(dataReader.GetOrdinal("LastName")),
                        DOB = dataReader.GetDateTime(dataReader.GetOrdinal("DOB")),
                        Mobile = dataReader.GetString(dataReader.GetOrdinal("MobileNumber")),
                         Salary = dataReader.GetDecimal(dataReader.GetOrdinal("Salary")),
                         Designation = dataReader.GetInt32(dataReader.GetOrdinal("DesignationId")),
                         Address = dataReader.IsDBNull(dataReader.GetOrdinal("Address")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("Address")),
                    };
                 
                }
            }
            return employees;
        }
        [NonAction]
        private int CreateEmployee(EmployeeTestThree employee)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("INSERT INTO EmployeeTestThree(FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId) VALUES(@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address, @Salary, @Designation)", con);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", (object)employee.MiddleName ?? DBNull.Value);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DOB", employee.DOB.Date);
                command.Parameters.AddWithValue("@MobileNumber", employee.Mobile);
                command.Parameters.AddWithValue("@Salary", employee.Salary);

                command.Parameters.AddWithValue("@Address", (object)employee.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@Designation", employee.Designation);

                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
        [NonAction]
        private int UpdateEmployee(EmployeeTestThree employee)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("UPDATE EmployeeTestThree SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, DOB = @DOB, MobileNumber = @Mobile, Address = @Address, Salary = @Salary, DesignationId = @Designation WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", (object)employee.MiddleName ?? DBNull.Value);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                command.Parameters.AddWithValue("@Salary", employee.Salary);

                command.Parameters.AddWithValue("@Address", (object)employee.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@Designation", employee.Designation);

                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
        [NonAction]
        private int DeleteEmployee(int id)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("DELETE FROM EmployeeTestThree WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", id);
                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
    }
}