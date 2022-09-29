using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MT.PersonService.API.Dtos;
using MT.PersonService.Core.Entity;
using MT.PersonService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace MT.PersonService.API.Controllers
{
    [Route("api/[controller]/[action]")] // best practise açısından method ismi verilmemeli, ancak bu proje için uygulanmıştır.
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IGenericService<Person> _personService;
      
        private readonly IMapper _mapper;
        
        public PersonController(IGenericService<Person> personService,  IMapper mapper)
            
        {
            _personService = personService;
          
            _mapper = mapper;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPerson()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id) //db de Id var mı yok mu diye bir kontrol yapılmadı! Bir filter yazılabilir kod tekrarı olmaması için
        {
            var person = await _personService.GetByIdAsync(id);
            return Ok(_mapper.Map<PersonDto>(person));

        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(PersonDto personDto)
        {
            var newPerson = await _personService.AddAsync(_mapper.Map<Person>(personDto));
            return Created(string.Empty,_mapper.Map<PersonDto>(newPerson));

        }

        [HttpDelete]
        public  IActionResult RemovePerson(int id) // exception mekanizması ele alınmadı.
        {
            var person = _personService.GetByIdAsync(id).Result;
            _personService.Delete(person);
            return NoContent();

        }

      
    }
}
