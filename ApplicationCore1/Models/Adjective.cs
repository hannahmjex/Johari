using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
	public class Adjective
	{
		[Key]
		public int AdjectiveID { get; set; }

		public string AdjName { get; set; }

		public string AdjDefinition { get; set; }

		//this is supposed to be a bit, but that isn't a type so idk
		//0=negative 1=positive
		public bool AdjType { get; set; }

	}
}
