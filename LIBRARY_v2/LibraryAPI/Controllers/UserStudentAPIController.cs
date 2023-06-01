using LibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAPI.Controllers
{
    public class UserStudentAPIController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                
        [Route("api/UserStudentAPI/GetStudentLoginDetails/{username}/{password}")]
        public UserModel GetStudentLoginDetails(string username, string password)
        {
            UserModel student = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("LOGIN_STUDENT_SP", con))
                {
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentUsername", username);
                    cmd.Parameters.AddWithValue("@StudentPassword", password);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            student.UserId = int.Parse(sdr["UserId"].ToString());
                            student.UserName = sdr["UserName"].ToString();
                            student.UserPassword = sdr["UserPassword"].ToString();
                            student.IsActive = (bool)sdr["IsActive"];
                            student.UserTypeId = int.Parse(sdr["UserTypeId"].ToString());
                        }
                    }
                    con.Close();
                }
            }

            return student;
        }


        [Route("api/UserStudentAPI/GetBooksByStudentId/{studentId}")]
        public List<BooksListModel> GetBooksByStudentId(int studentId)
        {
            List<BooksListModel> books = new List<BooksListModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("READ_BOOKS_STUDENT_Id_SP", con))
                {
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            books.Add(new BooksListModel
                            {
                                BookId = int.Parse(sdr["BookId"].ToString()),
                                BookName = sdr["BookName"].ToString(),
                                BookAuthor = sdr["BookAuthor"].ToString(),
                                BookDate = (DateTime)sdr["BookDate"],
                                BookPublicationName = sdr["BookPublicationName"].ToString(),
                                BookYOP = int.Parse(sdr["BookYOP"].ToString()),
                                BookQty = int.Parse(sdr["BookQty"].ToString())
                            });
                        }
                    }
                    con.Close();
                }
            }

            return books;
        }


    }
}
