using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServerSide.Controllers;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Repositories;
using ServerSide.Services;

namespace Tests
{
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _personRepository = new Mock<IPersonRepository>();

        [Test]
        public void FindAllReturnsListofPerson()
        {
            //Arrange
            IEnumerable<Person> personList = new List<Person> {
                new Person { Id = 2 }
            };
            _personRepository.Setup(p => p.FindAll()).Returns(personList);
            var personService = new PersonService(_personRepository.Object);

            //Act
            var result = personService.FindAll().ToList();

            //Assert
            Assert.IsTrue(personList.Count() == 1);
            Assert.AreEqual(2, result[0].Id);
        }

        [Test]
        public void FindAllReturnsListofPersonEvenWhenEmpty()
        {
            //Arrange
            IEnumerable<Person> personList = new List<Person> {

            };
            _personRepository.Setup(p => p.FindAll()).Returns(personList);
            var personService = new PersonService(_personRepository.Object);

            //Act
            var result = personService.FindAll();

            //Assert
            Assert.AreEqual(0, personList.Count());
        }


        [Test]
        public void GetByIdReturnsPerson()
        {
            //Arrange
            int personId = 1;

            Person person = new Person()
            {
                Id = 1,
                address = "123 Main Street",
                firstName = "John",
                lastName = "Doe"
            };
            _personRepository.Setup(p => p.GetById(personId)).Returns(person);
            var personService = new PersonService(_personRepository.Object);

            //Act
            var result = personService.GetById(personId);

            //Assert
            Assert.AreEqual(personId, result.Id);
            Assert.AreEqual(person.address, result.address);
            Assert.AreEqual(person.lastName, result.lastName);
        }

        [Test]
        public void GetByIdThrowsWhenPersonNotFound()
        {
            //Arrange
            int personId = 2;
            _personRepository.Setup(p => p.GetById(personId)).Throws<NotFoundException>();
            var personService = new PersonService(_personRepository.Object);

            //Act/Assert
            Assert.Throws<NotFoundException>(() => personService.GetById(personId));

        }
    }
}