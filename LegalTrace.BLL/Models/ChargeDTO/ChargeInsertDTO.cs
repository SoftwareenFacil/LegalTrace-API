﻿using LegalTrace.DAL.Models;
using System.Runtime.CompilerServices;

namespace LegalTrace.BLL.Models.ChargeDTO
{
    public class ChargeInsertDTO
    {
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public ChargeType chargeType { get; set; }
        public string fileName { get; set; }

        public string fileType { get; set; }
        public string fileString { get; set; }



    }
}
