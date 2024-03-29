﻿using Microsoft.AspNetCore.Identity;
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

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		public string DOB { get; set; }

		public string Gender { get; set; }

		public string ASPNETUserID { get; set; }

        [ForeignKey("ASPNETUserID")]
        public virtual IdentityUser IdentityUser { get; set; }

    }
}
