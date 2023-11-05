using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfasces;
using Core.Entities;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<proltfolioItem> _proltfoliom;
        public HomeController(IUnitOfWork<Owner>owner,
            IUnitOfWork<proltfolioItem>proltfoliom)
        {
            this._owner = owner;
            this._proltfoliom = proltfoliom;
        }
        public IActionResult Index()
        {
            var homeviewmodel = new HomeViewModel() { Owner =_owner.Entity.GetAll().First(),proltfolioItems=_proltfoliom.Entity.GetAll().ToList()};
            return View(homeviewmodel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}

