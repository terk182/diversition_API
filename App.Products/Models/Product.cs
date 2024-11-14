using App.Products.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Products.Models
{
    [SwaggerSchema("SwaggerSchema.Description on class", ReadOnly = true, Nullable = false)]
    public class Product
    {
        
        [SwaggerSchema("Represents a Ticket type")]

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public List<ReviewsRequest> review {  get; set; }
    }
}
