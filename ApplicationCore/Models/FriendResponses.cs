using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
	public class FriendResponses
	{
		[Key]
		public int ResponseID { get; set; }

		public int AdjectiveID { get; set; }

		public int ClientID { get; set; }

		public int FriendID { get; set; }

		[ForeignKey("AdjectiveID")]
		public virtual Adjective Adjective { get; set; }

		[ForeignKey("ClientID")]
		public virtual Client Client { get; set; }

		[ForeignKey("FriendID")]
		public virtual Friend Friend { get; set; }
	}
}
