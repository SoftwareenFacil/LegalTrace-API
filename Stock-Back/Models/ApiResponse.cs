﻿namespace Stock_Back.Models
{
    public class ApiResponse
    {
        public string? Message { get; set; }
        public object? ResponseData { get; set; }

    }

    public enum ResponseType
    {
        Success,
        NotFound,
        Failure
    }
}
