﻿using System;
using System.Linq;
using HowToEntityFramework.Domain;
using HowToEntityFramework.Infra;
using NUnit.Framework;
using Shouldly;

namespace HowToEntityFramework.HowTo
{
    [TestFixture]
    public class AuditableTest : IntegratedTest
    {
        [Test]
        public void When_creating_or_updating_Auditable_should_fill_auditable_properties()
        {
            var createdAt = new DateTime(2016, 1, 2, 3, 4, 5);
            var updatedAt = new DateTime(2017, 6, 7, 8, 9, 10);

            // arrange & CreateAt
            App.Clock = () => createdAt;

            using (var db = new DatabaseContext())
            {
                db.Products.Add(new Product("iPhone", 599));
                db.Products.Add(new Product("Galaxyyy", 499));
                db.Products.Add(new Product("Motorola", 399));

                db.SaveChanges();
            }

            // act & UpdateAt
            App.Clock = () => updatedAt;

            using (var db = new DatabaseContext())
            {
                var galaxy = db.Products.Single(x => x.Name == "Galaxyyy");
                galaxy.Name = "Galaxy";
                db.SaveChanges();
            }

            // assert
            using (var db = new DatabaseContext())
            {
                var galaxy = db.Products.Single(x => x.Name == "Galaxy");
                galaxy.Audit.CreatedAt.ShouldBe(createdAt);
                galaxy.Audit.UpdatedAt.ShouldBe(updatedAt);
            }
        }
    }
}
