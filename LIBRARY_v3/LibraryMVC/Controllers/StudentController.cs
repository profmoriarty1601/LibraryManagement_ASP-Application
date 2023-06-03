using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;
using LibraryMVC.Enums;
using LibraryMVC.Services;

namespace LibraryMVC.Controllers
{
    public class StudentController : Controller
    {
        string apiUrl = "https://localhost:44368/api/UserStudentAPI";
        HttpClient client = new HttpClient();        

        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(UserModel student)
        {
            string URL = apiUrl + "/GetStudentLoginDetails";

            Session["UserName"] = student.UserName.ToString();
            Session["UserPassword"] = student.UserPassword.ToString();

            string Updated_URL = URL + "/" + student.UserName + "/" + student.UserPassword;

            HttpResponseMessage response = client.GetAsync(Updated_URL).Result;

            string json = JsonConvert.SerializeObject(response.Content);

            Console.WriteLine(Updated_URL);
            Console.WriteLine(json);

            if (response.IsSuccessStatusCode)
            {
                student = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);

                if (student.IsActive == true && student.UserTypeId == 2)
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Login Successful as student !");
                    return View("StudentDashBoard");
                }
                else
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Either not active student user or not student type !");
                    return View();
                }
            }

            return View(student);
        }

        [Route("Student/StudentDashBoard")]
        public ActionResult StudentDashBoard()
        {
            if (Session["UserName"] != null || Session["UserPassword"] != null)
            {
                return View("StudentLogin");
            }

            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, "Wrong username or password ! !");
            return View();
        }

        [Route("Student/StudentDashBoard/GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            return View();
        }

        [Route("Student/StudentDashBoard/GetAllBooks")]
        public ActionResult GetAllBooks(List<UserModel> students)
        {
            string URL = apiUrl + "/GetStudents";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            string json = JsonConvert.SerializeObject(response.Content);

            if (response.IsSuccessStatusCode)
            {
                students = JsonConvert.DeserializeObject<List<UserModel>>(response.Content.ReadAsStringAsync().Result);
                return View(students);
            }
            else
            {
                return RedirectToAction("StudentLogin", "Student");
            }
        }

    }

}