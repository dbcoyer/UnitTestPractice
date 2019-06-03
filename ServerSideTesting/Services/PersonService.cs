using System;
using System.Collections.Generic;
using ServerSide.Exceptions;
using ServerSide.Models;
using ServerSide.Repositories;

namespace ServerSide.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> FindAll();
        Person GetById(int id);
    }

    public class PersonService : IPersonService
    {

        private IPersonRepository _repository;

        public PersonService(IPersonRepository _repository)
        {
            this._repository = _repository;
        }


        public IEnumerable<Person> FindAll()
        {
            return this._repository.FindAll();
        }

        public Person GetById(int id)
        {
            var entity = _repository.GetById(id);

            if (entity == null)
                throw new NotFoundException($"The person ({id}) was not found.");

            return entity;
        }
    }
}
