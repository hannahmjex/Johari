using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Johari.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;

        public Client clientObj { get; set; }

        [BindProperty]
        public IList<SelectListItem> Clients { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public void OnGet()
        {
            List<Client> ClientList = new List<Client>();
            ClientList = (List<Client>)_unitofWork.Client.List();
            Clients = ClientList.ToList<Client>()
                .Select(c => new SelectListItem { Text = c.FirstName + " " + c.LastName, Value = c.ClientID.ToString()})
                .ToList<SelectListItem>(); 
        }

        public void OnPost()
        {
            foreach (SelectListItem Client in Clients)
            {
                if (Client.Selected)
                {
                    Response.Redirect(string.Format("./Admin/JohariWindow?id={0}", Int32.Parse(Client.Value)));
                }
            }
        }
    }
}
