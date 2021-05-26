using RestWithASPNETUdemy.Models.Base;

namespace RestWithASPNETUdemy.Models
{
    public class Person: BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Genre { get; set; }   
        public bool Enabled { get; set; }
    }
}