﻿namespace LegalTrace.PDF.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public bool SuperAdmin { get; set; }
        public DateTime Created { get; set; }
        public bool Vigency { get; set; }

        public string TaxID { get; set; }
    }
}
