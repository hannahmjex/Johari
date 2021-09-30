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
using Microsoft.AspNetCore.Identity;

namespace Johari.Pages.Clients
{
    public class FriendResponseModel : PageModel
    {
		private readonly IUnitofWork _unitofWork;

		[BindProperty]
		public Adjective AdjectiveObj { get; set; }

		public Client clientObj { get; set; }

		public FriendResponseModel(IUnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
			//harded right now to make the page load
			clientObj = new Client();

			//needs to get this from the onpost of the friend/index page
			clientObj.ClientID = 2;
		}

		[BindProperty]
		public IList<SelectListItem> Adjectives { get; set; }


		public void OnGet()
		{
			var clientsList = _unitofWork.Friend.List();
			var adjectivesList = _unitofWork.Adjective.List();

			List<Adjective> AdjectiveList = new List<Adjective>();
			AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List();
			Adjectives = AdjectiveList.ToList<Adjective>()
				.Select(c => new SelectListItem { Text = c.AdjName + "+" + c.AdjDefinition + "+" + c.AdjType, Value = c.AdjectiveID.ToString() })
				.ToList<SelectListItem>();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			//if boxes are checked add them to table
			foreach (SelectListItem Adjective in Adjectives)
			{
				if (Adjective.Selected)
				{
					//Friend ID is just hardcoded to zero rn. Dont know what its really for.
					_unitofWork.FriendResponses.Add(new FriendResponses { AdjectiveID = Int32.Parse(Adjective.Value), ClientID = clientObj.ClientID, FriendID=3 });

				}
			}

			_unitofWork.Commit();

			return RedirectToPage("./Index");
		}

	}
}
    