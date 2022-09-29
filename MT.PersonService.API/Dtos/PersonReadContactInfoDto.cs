using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.PersonService.API.Dtos
{
    public class PersonReadContactInfoDto : PersonDto
    {
        public IEnumerable<ContactInfoDto> ContactInfos { get; set; }
    }
}
