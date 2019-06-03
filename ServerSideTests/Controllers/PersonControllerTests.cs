using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServerSide.Controllers;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Services;

namespace Tests
{
    public class PersonControllerTests
    {
        private Mock<IPersonService> _personService = new Mock<IPersonService>();
        private PersonController _personController;

        [Test]
        public void FindAllReturnsListofPerson()
        {
            //Arrange
            _personController = new PersonController(_personService.Object);
            List<Person> personList = new List<Person> { new Person { Id = 1 } };
            _personService.Setup(p => p.FindAll()).Returns(personList);

            //Act
            var controllerReturn = _personController.FindAll().Result as ObjectResult;
            var returnedList = controllerReturn.Value as List<Person>;

            //Assert
            Assert.AreEqual(200, controllerReturn.StatusCode.Value);
            Assert.AreEqual(1, returnedList.Count);
        }


        [Test]
        public void GetByIdReturnsPerson()
        {
            //Arrange
            int personId = 1;

            _personController = new PersonController(_personService.Object);
            Person person = new Person()
            {
                Id = 1,
                address = "123 Main Street",
                firstName = "John",
                lastName = "Doe"
            };
            _personService.Setup(p => p.GetById(personId)).Returns(person);

            //Act
            var controllerReturn = _personController.Get(personId).Result as ObjectResult;
            var result = controllerReturn.Value as Person;

            //Assert
            Assert.AreEqual(200, controllerReturn.StatusCode.Value);
            Assert.AreEqual(personId, result.Id);
        }

        [Test]
        public void GetByIdThrowsWhenPersonNotFound()
        {
            //Arrange
            int personId = 2;

            _personController = new PersonController(_personService.Object);

            _personService.Setup(p => p.GetById(personId)).Throws<NotFoundException>();

            //Act
            var controllerReturn = _personController.Get(personId).Result as ObjectResult;
            var result = controllerReturn.Value as Person;

            //Assert
            Assert.AreEqual(404, controllerReturn.StatusCode.Value);
        }
    }
}