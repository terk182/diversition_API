using App.Auth.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Auth.Services
{
    public class UserService
    {
        private readonly string _filePath;

        public UserService(IConfiguration configuration)
        {
            // ดึง path ของไฟล์ JSON จาก appsettings.json
            _filePath = configuration["UserFilePath"];

            // ตรวจสอบว่าไฟล์ JSON มีอยู่หรือไม่ ถ้าไม่มีก็สร้างไฟล์ว่างขึ้นมา
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<User> GetAllUsers()
        {

            var jsonData = File.ReadAllText(_filePath);
            // ตรวจสอบกรณีที่ไฟล์ว่างเปล่า
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<User>();
            }
            return JsonSerializer.Deserialize<List<User>>(jsonData);
        }

        public void AddUser(User user)
        {
            var users = GetAllUsers();

            // ตั้งค่า ID ให้กับผู้ใช้ใหม่ (ใช้ ID ที่มากที่สุด + 1)
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;

            users.Add(user);

            var jsonData = JsonSerializer.Serialize(users);
            File.WriteAllText(_filePath, jsonData);
        }

        public bool UsernameExists(string username)
        {
            var users = GetAllUsers();
            return users.Any(u => u.Username == username);
        }
    }
}
