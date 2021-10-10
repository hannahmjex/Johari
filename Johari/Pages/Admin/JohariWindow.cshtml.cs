using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Johari.Pages.Admin
{
    public class JohariWindowModel : PageModel
    {
        public IList<SelectListItem> OpenAdj { get; set; }
        public IList<SelectListItem> HiddenAdj { get; set; }
        public IList<SelectListItem> BlindAdj { get; set; }
        public IList<SelectListItem> UnknownAdj { get; set; }

        public void OnGet()
        {


        }
    }
}
