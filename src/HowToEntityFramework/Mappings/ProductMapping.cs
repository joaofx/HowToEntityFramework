﻿using System.Data.Entity.ModelConfiguration;
using HowToEntityFramework.Domain;
using HowToEntityFramework.Infra;

namespace HowToEntityFramework.Mappings
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasMany(m => m.Stocks);
        }
    }
}