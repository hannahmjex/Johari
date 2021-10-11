using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Johari.ViewModels
{
    public class ClientVM
    {
        [BindProperty]
        public Client Clients { get; set; }

        public IEnumerable<SelectListItem> ClientList { get; set; }
    }
}
