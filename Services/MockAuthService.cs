using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobappg4v2.Model;

namespace Mobappg4v2.Services
{
    public class MockAuthService
    {
        private static readonly List<UserModel> _users = new List<UserModel>
        {
            new UserModel 
            { 
                UserId = "1", 
                Email = "seller@test.com", 
                Password = "password123", 
                Role = UserRole.Seller,
                IsVerified = true,
                CreatedDate = DateTime.Now
            },
            new UserModel 
            { 
                UserId = "2", 
                Email = "customer@test.com", 
                Password = "password123", 
                Role = UserRole.Customer,
                IsVerified = true,
                CreatedDate = DateTime.Now
            }
        };

        public async Task<UserModel> SignIn(string email, string password)
        {
            // Simulate network delay
            await Task.Delay(1000);

            var user = _users.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            
            if (user == null)
                throw new Exception("User not found");

            if (user.Password != password)
                throw new Exception("Invalid password");

            return user;
        }

        public async Task<UserModel> RegisterSeller(string email, string password)
        {
            await Task.Delay(1000);

            if (_users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Email already registered");

            var newUser = new UserModel
            {
                UserId = (_users.Count + 1).ToString(),
                Email = email,
                Password = password,
                Role = UserRole.Seller,
                CreatedDate = DateTime.Now,
                IsVerified = false
            };

            _users.Add(newUser);
            return newUser;
        }

        public async Task<UserModel> RegisterCustomer(string email, string password)
        {
            await Task.Delay(1000);

            if (_users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Email already registered");

            var newUser = new UserModel
            {
                UserId = (_users.Count + 1).ToString(),
                Email = email,
                Password = password,
                Role = UserRole.Customer,
                CreatedDate = DateTime.Now,
                IsVerified = false
            };

            _users.Add(newUser);
            return newUser;
        }
    }
} 