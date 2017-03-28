using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private HomePageViewModel _viewModel = new HomePageViewModel();

        public ActionResult Index(string text)
        {
            HomePageViewModel vm = new HomePageViewModel();
            if (!String.IsNullOrEmpty(text))
            {
                vm.SearchText = text;
                vm.CharacterCounts = CountCharacters(text);
            }
            return View(_viewModel);
        }

        //public ActionResult SubmitText(string text)
        //{
        //    if (!String.IsNullOrEmpty(text))
        //    {
        //        _viewModel.SearchText = text;
        //        _viewModel.CharacterCounts = CountCharacters(text);
        //    }
        //    return Redirect("/home/index");
        //}

        private Dictionary<char, int> CountCharacters(string text)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();
            foreach (char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                result.Add(c, 0);
            }

            foreach (char c in text.ToUpper())
            {
                if (result.ContainsKey(c))
                {
                    result[c]++;
                }
            }

            return result;
        }
    }
}