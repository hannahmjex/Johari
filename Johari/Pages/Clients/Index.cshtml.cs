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
	public class IndexModel : PageModel
	{
		private readonly IUnitofWork _unitofWork;

		[BindProperty]
		public Adjective AdjectiveObj { get; set; }

		public Client clientObj { get; set; }

		public IndexModel(IUnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
			//harded right now to make the page load
			clientObj = new Client();
			clientObj.ClientID = 2;
		}

		[BindProperty]
		public IList<SelectListItem> Adjectives { get; set; }


		public void OnGet()
		{				
			var clientsList = _unitofWork.Client.List();
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
					_unitofWork.ClientResponses.Add(new ClientResponses { AdjectiveID = Int32.Parse(Adjective.Value), ClientID = clientObj.ClientID });
					
				}
			}

			_unitofWork.Commit();

			return RedirectToPage("./Index");
		}

	}
}
