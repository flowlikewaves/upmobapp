using System;

namespace Mobappg4v2.Model
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsVerified { get; set; }
    }

    public enum UserRole
    {
        Customer,
        Seller,
        Admin
    }
} 