using App.Products.Models;
using App.Products.Models.Requests;
using App.Products.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Products.Services
{
    public interface IProductService
    {
        List<Product> GetAllProduct();
        Product GetAllProductById(int productId);
        void AddProduct(Product product);
        bool productnameExists(string productname);
        List<ReviewsRequest> GetAllreview();
        BaseResponse<ReviewsRequest> addreview(ReviewsRequest request);
        List<ReviewsRequest> GetreviewByProductId(int productId);
    }
}
