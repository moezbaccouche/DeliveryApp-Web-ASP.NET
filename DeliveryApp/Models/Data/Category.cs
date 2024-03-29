﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.Data
{
    public class Category : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public byte[] ImageBase64 { get; set; }
    }
}
