using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace HPPMDotNetCore.Models
{
    [Table("tbl_Product")]
    public class ProductDataModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }

    public class ProductListResponseModel
    {
        public List<ProductDataModel> ProductList { get; set; }
    }

    [Table("tbl_ProductOrder")]
    public class ProductOrderDataModel
    {
        [Key]
        public int ProductOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }

    public class ProductOrderListResponseModel
    {
        public List<OrderModel> Orders { get; set; }
    }

    public class OrderModel
    {
        public string ProductName { get; set;}
        public decimal ProductPrice { get; set;}
        public int ProductQuantity { get; set;}
    }
    
}

