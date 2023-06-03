using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BooksListModel
    {
        public int BookId  { get; set;}
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime BookDate { get; set; }
        public string BookPublicationName { get; set; }
        public int BookYOP { get; set; }
        public int BookQty { get; set; }

    }
}