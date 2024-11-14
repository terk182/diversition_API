using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Products.Models.Requests
{
    public class ReviewsRequest
    {

        public int reviewID { get; set; }
        public int userID { get; set; }
        public int ProductID { get; set; }
        public string reviewText { get; set; }
        public int rating { get; set; }
        public DateTime createdDate { get; set; }
    }
}
