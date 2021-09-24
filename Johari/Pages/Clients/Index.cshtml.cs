using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Johari.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [BindProperty]
        public IList<SelectListItem> Adjectives { get; set; }


        public void OnGet()
        {
            List<Adjective> AdjectiveList = new List<Adjective>();
            AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List();
            Adjectives = AdjectiveList.ToList<Adjective>()
                .Select(c => new SelectListItem { Text = c.AdjName + " " +c.AdjDefinition, Value = c.AdjectiveID.ToString() })
                .ToList<SelectListItem>();
        }
            
    }
}
