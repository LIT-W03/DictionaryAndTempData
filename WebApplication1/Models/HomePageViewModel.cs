using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomePageViewModel
    {
        public string SearchText { get; set; }
        public Dictionary<char, int> CharacterCounts { get; set; }
    }
}