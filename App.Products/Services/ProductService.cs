using App.Products.Models;
using App.Products.Models.Requests;
using App.Products.Models.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Products.Services
{
    public class ProductService:IProductService
    {
        private readonly string _filePath;
        private readonly string _reviewPath;

        public ProductService(IConfiguration configuration)
        {
            // ดึง path ของไฟล์ JSON จาก appsettings.json
            _filePath = configuration["productFilePath"];
            _reviewPath = configuration["reviewsFilePath"];
            // ตรวจสอบว่าไฟล์ JSON มีอยู่หรือไม่ ถ้าไม่มีก็สร้างไฟล์ว่างขึ้นมา
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
            if (!File.Exists(_reviewPath))
            {
                File.WriteAllText(_reviewPath, "[]");
            }
        }

        public List<Product> GetAllProduct()
        {

            var jsonData = File.ReadAllText(_filePath);
            // ตรวจสอบกรณีที่ไฟล์ว่างเปล่า
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<Product>();
            }
            var data = JsonSerializer.Deserialize<List<Product>>(jsonData);
            foreach (var item in data)
            {
                item.review = GetreviewByProductId(item.ProductID);
            }
                
            return data;
        }
        public Product GetAllProductById(int productId)
        {
            var data = GetAllProduct().Where(x => x.ProductID == productId).FirstOrDefault();
            return data;
        }
        public void AddProduct(Product product)
        {
            var _products = GetAllProduct();

            // ตั้งค่า ID ให้กับผู้ใช้ใหม่ (ใช้ ID ที่มากที่สุด + 1)
            product.ProductID = _products.Any() ? _products.Max(u => u.ProductID) + 1 : 1;

            _products.Add(product);

            var jsonData = JsonSerializer.Serialize(_products);
            File.WriteAllText(_filePath, jsonData);
        }

        public bool productnameExists(string productname)
        {
            var products = GetAllProduct();
            return products.Any(u => u.ProductName == productname);
        }

        public List<ReviewsRequest> GetAllreview()
        {

            var jsonData = File.ReadAllText(_reviewPath);
            // ตรวจสอบกรณีที่ไฟล์ว่างเปล่า
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<ReviewsRequest>();
            }
            return JsonSerializer.Deserialize<List<ReviewsRequest>>(jsonData);
        }
        public BaseResponse<ReviewsRequest> addreview(ReviewsRequest request)
        {
            var responce = new BaseResponse<ReviewsRequest>();
            var reviews = GetAllreview();

            // ตั้งค่า ID ให้กับผู้ใช้ใหม่ (ใช้ ID ที่มากที่สุด + 1)
            request.reviewID = reviews.Any() ? reviews.Max(u => u.reviewID) + 1 : 1;

            reviews.Add(request);

            var jsonData = JsonSerializer.Serialize(reviews);
            File.WriteAllText(_reviewPath, jsonData);
            responce.Success = true;
            responce.Messsage = "บันทึก review สำเร็จ";
            responce.Data = request;
            return responce;
        }

        public List<ReviewsRequest> GetreviewByProductId(int productId)
        {
        
            var reviews = GetAllreview();
            var review = reviews.Where(x => x.ProductID == productId).ToList();
          
            return review;
        }


    }
}

