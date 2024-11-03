using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}