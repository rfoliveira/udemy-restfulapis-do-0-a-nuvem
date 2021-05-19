using System;
using RestWithASPNETUdemy.Models.Base;

namespace RestWithASPNETUdemy.Models
{
    public class Book: BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}