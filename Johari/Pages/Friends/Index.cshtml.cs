using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Friends
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofwork;

        public Client Client { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofwork = unitofWork;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            /*
            Client = new Client()
            {
                ClientID = Int32.Parse(claim.Value)
            };
            */

        }
    }
}
