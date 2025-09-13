using BLL.interfaces;
using BLL.Models;
using DLL.interfaces;
using DLL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PeopleService : IsServices<People>
    {
        private IsRepository<People_Info> _peopleRepositopry;
        private List<People> _people;
        public PeopleService(IsRepository<People_Info> repository)
        {
            this._peopleRepositopry = repository;
            this._people = new List<People>();
        }
        private People_Info PeopleToPeopleInfo(People people)
        {
            return new People_Info()
            {
                Id = people.Id,
                Name = people.Name,
                LastName = people.LastName,
                Birthday = people.Birthday,
                Phone = people.Phone,
            };
        }
        public void Add(People value)
        {
            _peopleRepositopry.Add(PeopleToPeopleInfo(value));
        }
    }
}
