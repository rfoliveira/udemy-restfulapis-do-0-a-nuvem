using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BookVO : ISupportHypermedia
    {
        public int? Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>(); 
    }
}