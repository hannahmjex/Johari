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

        [BindProperty]
        public Friend FriendObj { get; set; }

        [BindProperty]
        public Client ClientObj { get; set; }

        [BindProperty]
        public bool ClientExists { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofwork = unitofWork;
            ClientObj = new Client();
            FriendObj = new Friend();
            ClientExists = true;
        }
        public IActionResult OnPost()
        {
            //if client id is verified from clientDB redirect to /FriendResponse
            List<Client> clients = new List<Client>();
            var clientsList = _unitofwork.Client.List();
            clients = (List<Client>)clientsList;

            foreach (Client c in clients)
            {
                if(c.ClientID==ClientObj.ClientID)
                {
                    _unitofwork.Friend.Add(new Friend {HowLong = FriendObj.HowLong, Relationship = FriendObj.Relationship });
                    _unitofwork.Commit();
                    
                    ClientExists = true;
                    //return RedirectToPage("./FriendResponse");
                    Response.Redirect(string.Format("./Friends/FriendResponse?id={0}", ClientObj.ClientID));
                }
            }

            ClientExists = false;

          
            return Page();

        }
    }
}
