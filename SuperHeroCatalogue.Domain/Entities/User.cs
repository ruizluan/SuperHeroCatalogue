﻿namespace SuperHeroCatalogue.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int IdRole { get; set; }
    }
}