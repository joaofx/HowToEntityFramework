﻿using System;
using System.Linq;
using HowToEntityFramework.Concerns;
using HowToEntityFramework.Domain;
using HowToEntityFramework.Infra;
using NUnit.Framework;
using Shouldly;

namespace HowToEntityFramework.HowTo
{
    [TestFixture]
    public class EffectiveTest : IntegratedTest
    {
        [Test]
        public void Should_retrieve_entities_effective_for_a_range_of_date()
        {
            var iphone = new Product("iPhone", 499);
            var galaxy = new Product("Galaxy", 450);

            var june = new Effective(new DateTime(2016, 6, 1), new DateTime(2016, 6, 30));
            var julyToNow = new Effective(new DateTime(2016, 7, 1));

            using (var db = new DatabaseContext())
            {
                db.Products.Add(iphone);
                db.Products.Add(galaxy);

                db.Discounts.Add(new Discount(iphone, 479, june));
                db.Discounts.Add(new Discount(iphone, 489, julyToNow));

                db.SaveChanges();
            }
            
            using (var db = new DatabaseContext())
            {
                db.Discounts
                    .Where(x => x.ProductId == iphone.Id)
                    .Count(Effective.On(new DateTime(2016, 5, 1)))
                    .ShouldBe(0);

                db.Discounts
                    .Where(x => x.ProductId == iphone.Id)
                    .Count(Effective.On(new DateTime(2016, 6, 1)))
                    .ShouldBe(1);
            }
        }
    }
}
