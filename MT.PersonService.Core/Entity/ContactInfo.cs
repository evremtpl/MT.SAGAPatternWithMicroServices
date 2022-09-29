﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.PersonService.Core.Entity
{
    public class ContactInfo
    {

        public int id { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Location { get; set; }
        public int UUID { get; set; }
        public virtual Person Person { get; set; }
    }
}
