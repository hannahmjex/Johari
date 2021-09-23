using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
	public class Friend
	{
		[Key]
		public int FriendID { get; set; }

		public string Relationship { get; set; }

		public string HowLong { get; set; }
	

		//this is a forgien key but idk how to access it
		public int ASPNETUserID { get; set; }

		//[ForeignKey("ASPNETUserID")]
		//public virtual IdentityUser IdentityUser { get; set; }
	}
}
