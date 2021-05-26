using System.Collections.Generic;
using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Data.VO
{
    public class PersonVO : ISupportHypermedia
    {
        public long? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Genre { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>(); 
        public bool Enabled { get; set; }
    }
}