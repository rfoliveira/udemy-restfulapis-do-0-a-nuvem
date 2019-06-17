using RestWithASPNETudemy.Models.Base;
using System;

namespace RestWithASPNETudemy.Models
{
    public class Book: BaseEntity
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}
