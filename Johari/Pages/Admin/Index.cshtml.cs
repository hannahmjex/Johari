using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Johari.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Johari.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public ClientVM clientObj { get; set; }

        public ClientVM SelectedClient { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public void OnGet(int? ClientID)
        {
            var clients = _unitofWork.Client.List();

            //on submit new client id
            if (ClientID != null)
            {
                clientObj = new ClientVM
                {
                    Clients = _unitofWork.Client.Get(u => u.ClientID == ClientID),
                    ClientList = clients.Select(c => new SelectListItem { Value = c.ClientID.ToString(), Text = c.FirstName + " " + c.LastName })
                };
            }

            //first time page load
            else
            {
                clientObj = new ClientVM
                {
                    Clients = new Client(),
                    ClientList = clients.Select(c => new SelectListItem { Value = c.ClientID.ToString(), Text = c.FirstName + " " + c.LastName })
                };
            }

        }

        public IActionResult OnPost()
        {
            return RedirectToPage("./Index", new { clientObj.Clients.ClientID });
        }
    }
}
