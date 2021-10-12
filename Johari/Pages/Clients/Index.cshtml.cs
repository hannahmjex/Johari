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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace Johari.Pages.Clients
{
	public class IndexModel : PageModel
	{
		private readonly IUnitofWork _unitofWork;
		private readonly IEmailSender _emailSender;

	

		public Client clientObj { get; set; }
		public ClientResponses clientResponseObj { get; set; }

		public IndexModel(IUnitofWork unitofWork, IEmailSender emailSender)
		{
			_unitofWork = unitofWork;
			_emailSender = emailSender;

		}

		[Required]
		[EmailAddress]
		[BindProperty]
		[Display(Name = "Email")]
		public string Email1{ get; set; }

		[Required]
		[EmailAddress]
		[BindProperty]
		[Display(Name = "Email")]
		public string Email2 { get; set; }

		[Required]
		[EmailAddress]
		[BindProperty]
		[Display(Name = "Email")]
		public string Email3 { get; set; }

		[Required]
		[EmailAddress]
		[BindProperty]
		[Display(Name = "Email")]
		public string Email4 { get; set; }

		[BindProperty]
		public IList<SelectListItem> Adjectives { get; set; }


		public IActionResult OnGet(int id)
		{		
			var adjectivesList = _unitofWork.Adjective.List();

			List<Adjective> AdjectiveList = new List<Adjective>();
			AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List();
			Adjectives = AdjectiveList.ToList<Adjective>()
				.Select(c => new SelectListItem { Text = c.AdjName + "+" + c.AdjDefinition + "+" + c.AdjType, Value = c.AdjectiveID.ToString() })
				.ToList<SelectListItem>();

			var claimsIdentity = (ClaimsIdentity)this.User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string claimValue = claim.Value;
			clientObj = _unitofWork.Client.Get(m => m.ASPNETUserID == claimValue, false, null);

			clientResponseObj = _unitofWork.ClientResponses.Get(m => m.ClientID == clientObj.ClientID, false, null);
            if (clientResponseObj != null)
            {
				return RedirectToPage("./ResponseExists");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			string claimValue = claim.Value;

			clientObj = _unitofWork.Client.Get(m => m.ASPNETUserID == claimValue, false, null);

			//if boxes are checked add them to table
			foreach (SelectListItem Adjective in Adjectives)
			{
				if (Adjective.Selected)
				{
					_unitofWork.ClientResponses.Add(new ClientResponses { AdjectiveID = Int32.Parse(Adjective.Value), ClientID = clientObj.ClientID });
					
				}
			}

			_unitofWork.Commit();

			string subject = "Johari for a Friend!";
			string message = $"Please fill out the Johari questionairre for " + clientObj.FirstName + " " + clientObj.LastName + " <a href='https://joharihannahalex.azurewebsites.net/Friends?id="+ clientObj.ClientID + "'>clicking here</a> and using the ID: " + clientObj.ClientID;

			await _emailSender.SendEmailAsync(Email1, subject, message);
			await _emailSender.SendEmailAsync(Email2, subject, message);
			await _emailSender.SendEmailAsync(Email3, subject, message);
			await _emailSender.SendEmailAsync(Email4, subject, message);

			return RedirectToPage("./ResponseExists");
		}

	}
}
