﻿using System;
using System.Collections.Generic;

namespace ECommerceApp.Models
{
    public partial class Productsdetail
    {
        public Guid Id { get; set; }
        public Guid? Productid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Views { get; set; }

        public Products Product { get; set; }
    }
}
