﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MT.PersonService.API.Dtos
{
    public class PersonDto
    {
        
        [Required] //FluentValidation Kullanılabilir.
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string Company { get; set; }

    }
}
