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

        [BindProperty]
        public bool MaxFriends { get; set; }

        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofwork = unitofWork;
            ClientObj = new Client();
            FriendObj = new Friend();
            ClientExists = true;
            MaxFriends = false;
        }

        public void OnGet(int? id)
        {
            if(id>0)
            {
                ClientObj = _unitofwork.Client.Get(u => u.ClientID == id);
            }
        }

        public IActionResult OnPost()
        {
            //if client id is verified from clientDB redirect to /FriendResponse
            List<Client> clients = new List<Client>();
            var clientsList = _unitofwork.Client.List();
            clients = (List<Client>)clientsList;

            
            int frlist = _unitofwork.FriendResponses.List(u => u.ClientID == ClientObj.ClientID).Count();
            int numFr = 19; //the number of responses each friend is required to enter

            if ((frlist/numFr)< 4)
            {

                foreach (Client c in clients)
                {
                    if (c.ClientID == ClientObj.ClientID)
                    {

                        _unitofwork.Friend.Add(new Friend { HowLong = FriendObj.HowLong, Relationship = FriendObj.Relationship });
                        _unitofwork.Commit();

                        ClientExists = true;
                        Response.Redirect(string.Format("./Friends/FriendResponse?id={0}", ClientObj.ClientID));
                    }
                }

                ClientExists = false;

            }
            else
            {
                MaxFriends = true;
            }
            return Page();

        }
    }
}
