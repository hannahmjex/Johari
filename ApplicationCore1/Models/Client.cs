using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
	public class Client
	{
		[Key]
		public int ClientID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DOB { get; set; }

		public string Gender { get; set; }

		public int ASPNETUserID { get; set; }

		//[ForeignKey("ASPNETUserID")]
		//public virtual IdentityUser IdentityUser { get; set; }

	}
}
