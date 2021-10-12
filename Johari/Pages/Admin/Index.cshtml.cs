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

        [BindProperty]
        public AdjectiveVM adjObj { get; set; }

        public string hidden { get; set; }
        public string unknown { get; set; }
        public string blind { get; set; }
        public string open { get; set; }


        public IEnumerable<ClientResponses> clientResponses { get; set; }
        public IEnumerable<FriendResponses> friendResponses { get; set; }
        public IEnumerable<Adjective> adjectives { get; set; }

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

                clientResponses = _unitofWork.ClientResponses.List();
                friendResponses = _unitofWork.FriendResponses.List();
                adjectives = _unitofWork.Adjective.List();

                adjObj = new AdjectiveVM
                {
                    BlindList = new List<string>(),
                    HiddenList = new List<string>(),
                    OpenList = new List<string>(),
                    UnknownList = new List<string>()
                };

                //open query
                var openQuery = from adjective in adjectives
                                join fr in friendResponses on adjective.AdjectiveID equals fr.Adjective.AdjectiveID into frAdjTbl
                                from frAdj in frAdjTbl.DefaultIfEmpty()

                                join cr in clientResponses on adjective.AdjectiveID equals cr.Adjective.AdjectiveID into crAdjTbl
                                from crAdj in crAdjTbl.DefaultIfEmpty()

                                where frAdj != null && crAdj!=null

                                   select new
                                   {
                                       AdjectiveName = adjective.AdjName,
                                       AdjectiveType = adjective.AdjType,
                                       AdjectiveDefinition = adjective.AdjDefinition
                                   };

                foreach (var o in openQuery)
                {
                    adjObj.OpenList.Add(o.AdjectiveName + " ");
                }

                open = listToString(adjObj.OpenList);

                //hidden query
                var unknownQuery = from adjective in adjectives
                                       join fr in friendResponses on adjective.AdjectiveID equals fr.Adjective.AdjectiveID into frAdjTbl
                                       from frAdj in frAdjTbl.DefaultIfEmpty()

                                       join cr in clientResponses on adjective.AdjectiveID equals cr.Adjective.AdjectiveID into crAdjTbl
                                       from crAdj in crAdjTbl.DefaultIfEmpty()

                                       where frAdj == null && crAdj == null

                                   select new
                                       {
                                           AdjectiveName = adjective.AdjName,
                                           AdjectiveType = adjective.AdjType,
                                           AdjectiveDefinition = adjective.AdjDefinition
                                       };

                foreach (var u in unknownQuery)
                {
                    adjObj.UnknownList.Add(u.AdjectiveName + " ");
                }

                unknown = listToString(adjObj.UnknownList);

                //hidden query
                var hiddenQuery = from cr in clientResponses
                                      join fr in friendResponses on cr.Adjective.AdjectiveID equals fr.Adjective.AdjectiveID into crFrTbl
                                      from crFr in crFrTbl.DefaultIfEmpty()

                                      join adj in adjectives on cr.Adjective.AdjectiveID equals adj.AdjectiveID into crFrAdjTbl
                                      from crFrAdj in crFrAdjTbl.DefaultIfEmpty()

                                      where crFr == null
                                      select new
                                      {
                                          AdjectiveName = crFrAdj.AdjName,
                                          AdjectiveType = crFrAdj.AdjType,
                                          AdjectiveDefinition = crFrAdj.AdjDefinition
                                      };
                foreach (var h in hiddenQuery)
                {
                    adjObj.HiddenList.Add(h.AdjectiveName + " ");
                }

                hidden = listToString(adjObj.HiddenList);

                //blind query
                var blindQuery = from fr in friendResponses
                                  join cr in clientResponses on fr.Adjective.AdjectiveID equals cr.Adjective.AdjectiveID into crFrTbl
                                  from crFr in crFrTbl.DefaultIfEmpty()

                                  join adj in adjectives on fr.Adjective.AdjectiveID equals adj.AdjectiveID into crFrAdjTbl
                                  from crFrAdj in crFrAdjTbl.DefaultIfEmpty()

                                  where crFr == null
                                  select new
                                  {
                                      AdjectiveName = crFrAdj.AdjName,
                                      AdjectiveType = crFrAdj.AdjType,
                                      AdjectiveDefinition = crFrAdj.AdjDefinition
                                  };

                foreach (var b in blindQuery)
                {
                    adjObj.BlindList.Add(b.AdjectiveName + " ");
                }

                blind = listToString(adjObj.BlindList);

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

        public string listToString(List<string> sList)
        { 
            string ret = "";
            foreach(string s in sList)
            {
                ret += s + " ";
            }
            return ret;
        }
    }
}
