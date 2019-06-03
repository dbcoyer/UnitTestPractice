using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ServerSide.Controllers;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Repositories;
using ServerSide.Services;

namespace ServerSideTests.Repositories
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        [Test]
        public void FindAllReturnsListofPerson()
        {
            //Arrange
            var context = BuildPersonDbContext();
            BuildPeople(context);

            var repo = new PersonRepository(context);

            //Act
            var result = repo.FindAll().ToList();

            //Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("700 Elm Ave", result[1].address);

            context.Dispose();
        }

        [Test]
        public void FindAllReturnsListofPersonEvenWhenEmpty()
        {
            //Arrange
            var context = BuildPersonDbContext();

            var repo = new PersonRepository(context);

            //Act
            var result = repo.FindAll();

            //Assert
            Assert.AreEqual(0, result.Count());
            context.Dispose();
        }


        public PersonDbContext BuildPersonDbContext()
        {
            var id = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<PersonDbContext>()
                .UseInMemoryDatabase(id)
                .Options;

            return new PersonDbContext(options);
        }

        public void BuildPeople(PersonDbContext context)
        {
            context.People.Add(new Person()
            {
                Id = 1,
                address = "123 Main Street",
                firstName = "John",
                lastName = "Doe"
            });
            context.People.Add(new Person()
            {
                Id = 2,
                address = "700 Elm Ave",
                firstName = "Jane",
                lastName = "Doe"
            });

            context.SaveChanges();
        }
    }
}
