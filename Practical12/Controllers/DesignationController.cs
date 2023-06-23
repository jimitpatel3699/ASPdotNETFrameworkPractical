using Practical12.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12.Controllers
{
    public class DesignationController : Controller
    {
        private string _connectionstring = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        public ActionResult Index()
        {
            List<Designation> designations = GetDesignations();
            return View(designations);
        }

        public ActionResult Details(int id)
        {
            Designation designation = GetDesignations(id);
            return View(designation);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Designation designationViewModel)
        {
            if (ModelState.IsValid)
            {
                var designation = new Designation { DesignationName = designationViewModel.DesignationName };
                CreateDesignation(designation);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Designation designation = GetDesignations(id);
            if (designation != null)
            {
                var DesignationViewModel = new Designation { Id = designation.Id, DesignationName = designation.DesignationName };
                return View(DesignationViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designation DesignationViewModel)
        {
            if (ModelState.IsValid)
            {
                var designation = new Designation { Id = DesignationViewModel.Id, DesignationName = DesignationViewModel.DesignationName };
                UpdateDesignation(designation);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            Designation designation = GetDesignations(id);
            if (designation != null)
            {
                var DesignationViewModel = new Designation { Id = designation.Id, DesignationName = designation.DesignationName };
                return View(DesignationViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            DeleteDesignation(id);
            return RedirectToAction("Index");
        }

        public ActionResult Queries()
        {
            IList<Designation> designations = new List<Designation>();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("SELECT DISTINCT(Designations.DesignationName), COUNT(EmployeeTestThree.Id) AS EmployeesCount FROM Designations INNER JOIN EmployeeTestThree ON Designations.Id = EmployeeTestThree.DesignationId GROUP BY Designations.DesignationName HAVING COUNT(EmployeeTestThree.Id) > 1", con);
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    designations.Add(new Designation()
                    {
                        DesignationName = dataReader.GetString(dataReader.GetOrdinal("DesignationName")),
                        EmployeesCount = dataReader.GetInt32(dataReader.GetOrdinal("EmployeesCount"))
                    });
                }
            }
            return View(designations);
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
        private Designation GetDesignations(int id)
        {
            Designation Designations = new Designation();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Designations WHERE Id=@Id", con);
                command.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Designations = new Designation()
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                        DesignationName = dataReader.GetString(dataReader.GetOrdinal("DesignationName")),                       
                    };

                }
            }
            return Designations;
        }
        [NonAction]
        private int CreateDesignation(Designation Designation)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Designations(DesignationName) VALUES(@DesignationName)", con);
                command.Parameters.AddWithValue("@DesignationName", Designation.DesignationName);
                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
        [NonAction]
        private int UpdateDesignation(Designation Designation)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("UPDATE Designations SET DesignationName = @DesignationName WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", Designation.Id);
                command.Parameters.AddWithValue("@DesignationName", Designation.DesignationName);
  
                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
        [NonAction]
        private int DeleteDesignation(int id)
        {
            int affectedRows = 0;
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Designations WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", id);
                con.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            return affectedRows;
        }
    }
}