using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        [Required, MinLength(6)]
        public string Author { get; set; }
    }
}
