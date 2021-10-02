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
using System.Security.Claims;

namespace Johari.Pages.Clients
{
    public class FriendResponseModel : PageModel
    {
		private readonly IUnitofWork _unitofWork;

		[BindProperty]
		public Adjective AdjectiveObj { get; set; }

		[BindProperty]
		public Client clientObj { get; set; }

		public int clientID;

		public FriendResponseModel(IUnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
			
		}

		[BindProperty]
		public IList<SelectListItem> Adjectives { get; set; }


		public void OnGet(int id)
		{
			clientObj = new Client();
			clientObj.ClientID = id;
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
				Friend f = _unitofWork.Friend.List().Last();


				if (Adjective.Selected)
				{
					_unitofWork.FriendResponses.Add(new FriendResponses { AdjectiveID = Int32.Parse(Adjective.Value), FriendID = f.FriendID,ClientID = clientObj.ClientID });
				}
			}

			_unitofWork.Commit();

			return RedirectToPage("../Index");
		}

	}
}
    