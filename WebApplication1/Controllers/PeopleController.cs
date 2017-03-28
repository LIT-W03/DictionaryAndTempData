using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public static class PersonDb
    {
        private static List<Person> _ppl = new  List<Person>();
        public static IEnumerable<Person> GetPeople()
        {
            return _ppl;
        }

        public static void Add(Person p)
        {
            _ppl.Add(p);
        }
    }

    public class PeopleVM
    {
        public IEnumerable<Person> People { get; set; }
        public string Message { get; set; }
    }

    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            PeopleVM vm = new PeopleVM();
            vm.People = PersonDb.GetPeople();
            if (TempData.ContainsKey("Person"))
            {
                Person p = (Person) TempData["Person"];
                vm.Message = p.FirstName + " " + p.LastName + " added!";
            }

            return View(vm);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Add(Person p)
        {
            PersonDb.Add(p);
            TempData["Person"] = p;
            return Redirect("/people/index");
        }
    }
}