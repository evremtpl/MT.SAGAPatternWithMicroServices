using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using MT.PersonService.API.Dtos;
using MT.PersonService.Core.Entity;
using MT.PersonService.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace MT.PersonService.API.Controllers
{
    [Route("api/[controller]/[action]")] //best practise açısından method conf a göre gitmek daha uygun olurdu.
    [ApiController]
    public class ContactInfoController : ControllerBase
    {

        private readonly IGenericService<ContactInfo> _contactInfoService;
        private readonly IMapper _mapper;

        public ContactInfoController(IGenericService<ContactInfo> contactInfoService, IMapper mapper)
        {
            _contactInfoService = contactInfoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddContactInfotoPerson(ContactInfoDto contactInfoDto)
        {
            var newContactInfo = await _contactInfoService.AddAsync(_mapper.Map<ContactInfo>(contactInfoDto));
            return Created(string.Empty, _mapper.Map<ContactInfoDto>(newContactInfo));
            

        }

        [HttpDelete]
        public IActionResult RemoveContactInfoFromPerson(int id)
        {
            var contactInfo = _contactInfoService.GetByIdAsync(id).Result;
            if (contactInfo != null)
            {
                _contactInfoService.Delete(contactInfo);

                return NoContent();
            }
            return BadRequest("Silinecek kayıt yok");
        }
    }
}
