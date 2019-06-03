using System;
using System.Collections.Generic;
using System.Linq;
using ServerSide.Models;

namespace ServerSide.Repositories
{
    public interface IPersonRepository
    {
        
        IEnumerable<Person> FindAll();
        Person GetById(int id);
    }

    public class PersonRepository : IPersonRepository
    {
        private PersonDbContext context;

        public PersonRepository(PersonDbContext context)
        {
            if (context.People.Count() == 0)
            {
                context.Initialize();
                context.SaveChanges();
            }

            this.context = context;
        }
        public IEnumerable<Person> FindAll()
        {
            return context.People.AsEnumerable();
        }

        public Person GetById(int id)
        {
            return context.People.FirstOrDefault(x => x.Id == id);
        }

    }
}
