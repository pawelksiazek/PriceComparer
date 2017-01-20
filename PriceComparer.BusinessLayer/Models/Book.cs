using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparer.BusinessLayer.Models
{
    public class Book : Product
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Isbn { get; set; }
    }
}
