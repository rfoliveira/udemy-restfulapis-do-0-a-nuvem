using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.Converter;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repo;
        private readonly PersonConverter _converter;

        public PersonBusiness()
        {
        }

        public PersonBusiness(IPersonRepository repo)
        {
            _repo = repo;
            _converter = new PersonConverter();
        }
        
        public PersonVO Create(PersonVO person)
        {
            try
            {
                // Só pra fins de teste apenas, 
                // em um mundo real a validação consideraria os atributos do objeto
                if (string.IsNullOrEmpty(person.Firstname))
                    throw new System.Exception("Firstname is required");
                else if (string.IsNullOrEmpty(person.Lastname))
                    throw new System.Exception("Lastname is required");
                else if (string.IsNullOrEmpty(person.Address))
                    throw new System.Exception("Address is required");

                //if (_repo.FindByFirstname(person.Firstname) != null)
                //    throw new System.Exception("Firstname already exists");

                var personEntity = _converter.Parse(person);
                personEntity = _repo.Create(personEntity);

                return _converter.Parse(personEntity);
            }
            catch (Exception)
            {
                 throw;
            }
        }

        public void Delete(long id) => _repo.Delete(id);

        public PersonVO Disable(long id) => _converter.Parse(_repo.Disable(id));

        public bool Exists(long id) => _repo.Exists(id);
        
        public IEnumerable<PersonVO> FindAll() => _converter.ParseList(_repo.FindAll());
        
        public PersonVO FindById(long id) => _converter.Parse(_repo.FindById(id));

        public IEnumerable<PersonVO> FindByName(string name) => _converter.ParseList(_repo.FindByName(name));

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pagesize, int page)
        {
            var offset = page > 0 ? page - 1 : 0;
            var sort = (string.IsNullOrEmpty(sortDirection) || sortDirection != "desc") ? "asc" : sortDirection;
            var size = (pagesize < 1) ? 1 : pagesize;
            
            // Camada de business não deve conter SQL, mas foi assim que o instrutor usou.
            // TODO: formatar para apenas passar os valores para o repositório
            var query = "select id, firstname, lastname, address, genre, enabled " +
                        "from persons " +
                        $"where firstname like '%{name}%' " + 
                        $"order by firstname {sort} " + 
                        $"limit {size} offset {offset}";

            var persons = _repo.FindWithPagedSearch(query);

            var countQuery = "select count(*) from persons";
            int totalResults = _repo.GetCount(countQuery);

            return new PagedSearchVO<PersonVO> 
            {
                CurrentPage = offset,
                List = _converter.ParseList(persons),
                PageSize = size,
                SortDirection = sortDirection,
                TotalResults = totalResults
            };
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repo.Update(personEntity);

            return _converter.Parse(personEntity);
        }
    }
}