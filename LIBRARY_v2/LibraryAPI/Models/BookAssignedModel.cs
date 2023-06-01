using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LibraryAPI.Models
{
    public class BookAssignedModel
    {
        public int AssignedId { get; set; }
        public DateTime IssueDate { get; set; }
        public int BooksId { get; set; } // FK BooksList
	    public int UserId { get; set; } // FK User
    }
}