using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Johari
{
    public class ClientResponsesVM
    {
        [BindProperty]
        public ClientResponses ClientResponses { get; set; }

        public IEnumerable<SelectListItem> ClientList { get; set; }

        public IEnumerable<SelectListItem> AdjectiveList { get; set; }



    }
}
