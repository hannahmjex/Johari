using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Johari.ViewModels
{
    public class AdjectiveVM
    {
        public List<String> HiddenList { get; set; }
        public List<String> OpenList { get; set; }
        public List<String> UnknownList { get; set; }
        public List<String> BlindList { get; set; }
    }
}
