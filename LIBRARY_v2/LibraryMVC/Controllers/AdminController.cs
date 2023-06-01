using LibraryMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class AdminController : Controller
    {
        string apiUrl = "https://localhost:44368/api/UserAdminAPI";
        HttpClient client = new HttpClient();

        public ActionResult GetAdmins()
        {
            List<UserModel> admins = new List<UserModel>();

            string URL = apiUrl + "/GetAdmins";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            string json = JsonConvert.SerializeObject(response.Content);

            if (response.IsSuccessStatusCode)
            {
                admins = JsonConvert.DeserializeObject<List<UserModel>>(response.Content.ReadAsStringAsync().Result);
            }

            return View(admins);
        }

        public ActionResult GetAdminById(int id)
        {
            UserModel admin = new UserModel();
            
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;

            string json = JsonConvert.SerializeObject(response.Content);

            if (response.IsSuccessStatusCode)
            {
                admin = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
            }

            return View(admin);
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(UserModel admin)
        {
            string URL = apiUrl + "/GetAdminLoginDetails";

            Session["UserName"] = admin.UserName.ToString();
            Session["UserPassword"] = admin.UserPassword.ToString();

            string Updated_URL = URL + "/" + admin.UserName + "/" + admin.UserPassword;

            HttpResponseMessage response = client.GetAsync(Updated_URL).Result;

            string json = JsonConvert.SerializeObject(response.Content);

            Console.WriteLine(Updated_URL);
            Console.WriteLine(json);

            if (response.IsSuccessStatusCode)
            {
                admin = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);

                if(admin.IsActive == true && admin.UserTypeId == 1)
                {
                    return RedirectToAction("AdminDashBoard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(admin);
        }

        [Route("Admin/AdminDashBoard")]
        public ActionResult AdminDashBoard()
        {
            if (Session["UserName"] != null && Session["UserPassword"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        [Route("Admin/AdminDashBoard/GetAllStudents")]
        public ActionResult GetAllStudents()
        {
            return View();
        }

        [Route("Admin/AdminDashBoard/GetAllStudents")]
        public ActionResult GetAllStudents(List<UserModel> students) 
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
                return RedirectToAction("AdminLogin", "Admin");
            }            
        }

    }
}