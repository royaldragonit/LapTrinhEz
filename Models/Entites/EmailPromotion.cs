﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace LapTrinhEZ.Models.Entites
{
    public partial class EmailPromotion
    {
        public int EmailPromotionId { get; set; }
        public string Email { get; set; }
        public DateTime CreateOn { get; set; }
        public bool? IsActive { get; set; }
    }
}