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

namespace Johari.Pages.Clients
{
	public class IndexModel : PageModel
	{
		private readonly IUnitofWork _unitofWork;

		[BindProperty]
		public Adjective AdjectiveObj { get; set; }

		public IndexModel(IUnitofWork unitofWork)
		{
			_unitofWork = unitofWork;
		}

		[BindProperty]
		public IList<SelectListItem> Adjectives { get; set; }


		public void OnGet()
		{

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
					//the adjectiveObj doesn't contain any data right now
					//I think we need to set the name, desc, and type, but i can't figure out how to access that info
					//AdjectiveObj.AdjName = Adjectives[]
					_unitofWork.Adjective.Add(AdjectiveObj);

				}
			}

			
			_unitofWork.Commit();

			return RedirectToPage("./Index");
		}

	}
}
