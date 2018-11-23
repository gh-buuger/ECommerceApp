using System;
using System.Collections.Generic;

namespace ECommerceApp.Models
{
    public partial class OrdersProducts
    {
        public OrdersProducts()
        {
            OrderProductAttributes = new HashSet<OrderProductAttributes>();
        }

        public Guid Id { get; set; }
        public Guid? Orderid { get; set; }
        public Guid Productid { get; set; }
        public string Productname { get; set; }
        public decimal Productprice { get; set; }
        public decimal Finalprice { get; set; }
        public decimal Productstax { get; set; }
        public int Productqty { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
        public ICollection<OrderProductAttributes> OrderProductAttributes { get; set; }
    }
}
