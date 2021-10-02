using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Johari.Pages.Clients
{
    public class ResponseExistsModel : PageModel
    {
        public Client clientObj { get; set; }
        public IUnitofWork _unitofWork { get; set; }

        public ResponseExistsModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string claimValue = claim.Value;
            clientObj = _unitofWork.Client.Get(m => m.ASPNETUserID == claimValue, false, null);
        }
    }
}
