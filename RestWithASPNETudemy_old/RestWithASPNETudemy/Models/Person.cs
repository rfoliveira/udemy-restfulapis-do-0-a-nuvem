﻿using RestWithASPNETudemy.Models.Base;

namespace RestWithASPNETudemy.Models
{
    public class Person: BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Genre { get; set; }
    }
}