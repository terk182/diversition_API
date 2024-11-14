using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Products.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Errors { get; set; }
        public string Messsage { get; set; }

        public T Data { get; set; }         // ข้อมูลที่ต้องการส่งกลับ (ชนิดข้อมูลเป็น Generic)

        public BaseResponse()
        {
            Success = false;  // ค่าเริ่มต้นของความสำเร็จเป็น false
        }
    }
}
