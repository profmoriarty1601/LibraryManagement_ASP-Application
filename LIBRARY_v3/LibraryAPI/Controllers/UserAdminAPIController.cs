using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    public class UserAdminAPIController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        [Route("api/UserAdminAPI/GetAdmins")]
        public List<UserModel> GetAdmins()
        {
            List<UserModel> admins = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("READ_ADMIN_USERS_SP", con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            admins.Add(new UserModel
                            {
                                UserId = int.Parse(sdr["UserId"].ToString()),
                                UserName = sdr["UserName"].ToString(),
                                UserPassword = sdr["UserPassword"].ToString(),
                                IsActive = (bool)sdr["IsActive"],
                                //sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("IsConfirmed"));
                                UserTypeId = int.Parse(sdr["UserTypeId"].ToString())
                            });
                        }
                    }
                    con.Close();
                }
            }

            return admins;
        }


        [Route("api/UserAdminAPI/GetAdminById/{id}")]
        public UserModel GetAdminById(int id)
        {
            UserModel admin = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("READ_ADMIN_ById_SP", con))
                {
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminId", id);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            admin.UserId = int.Parse(sdr["UserId"].ToString());
                            admin.UserName = sdr["UserName"].ToString();
                            admin.UserPassword = sdr["UserPassword"].ToString();
                            admin.IsActive = (bool)sdr["IsActive"];
                            //sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("IsConfirmed"));
                            admin.UserTypeId = int.Parse(sdr["UserTypeId"].ToString());
                        }
                    }
                    con.Close();
                }
            }

            return admin;
        }


        [Route("api/UserAdminAPI/GetAdminLoginDetails/{username}/{password}")]
        public UserModel GetAdminLoginDetails(string username, string password)
        {
            UserModel admin = new UserModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("LOGIN_ADMIN_SP", con))
                {
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminUsername", username);
                    cmd.Parameters.AddWithValue("@AdminPassword", password);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            admin.UserId = int.Parse(sdr["UserId"].ToString());
                            admin.UserName = sdr["UserName"].ToString();
                            admin.UserPassword = sdr["UserPassword"].ToString();
                            admin.IsActive = (bool)sdr["IsActive"];
                            //sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("IsConfirmed"));
                            admin.UserTypeId = int.Parse(sdr["UserTypeId"].ToString());
                        }
                    }
                    con.Close();
                }
            }

            return admin;
        }

        [Route("api/UserAdminAPI/GetStudents")]
        public List<UserModel> GetStudents()
        {
            List<UserModel> students = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("READ_STUDENT_USERS_SP", con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            students.Add(new UserModel
                            {
                                UserId = int.Parse(sdr["UserId"].ToString()),
                                UserName = sdr["UserName"].ToString(),
                                UserPassword = sdr["UserPassword"].ToString(),
                                IsActive = (bool)sdr["IsActive"],
                                //sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("IsConfirmed"));
                                UserTypeId = int.Parse(sdr["UserTypeId"].ToString())
                            });
                        }
                    }
                    con.Close();
                }
            }

            return students;
        }

        [Route("api/UserAdminAPI/GetBooksByStudentId/{studentId}")]
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