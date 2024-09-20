﻿using CarRent.Domain.Entities;

namespace CarRent.Application.Dtos
{
    public class PaymentReceiptDto
    {
        public int Id { get; set; }
        public virtual required Rental Rental { get; set; }
        public decimal RentValue { get; set; }
        public required string Observation { get; set; }
        public DateTime Emission { get; set; }
    }
}