﻿using UserManagement.Enums;

namespace UserManagement.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
        public UserStatus Status { get; set; }
    }
}
