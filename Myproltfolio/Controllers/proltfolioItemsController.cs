using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Web.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfasces;

namespace Web.Controllers
{
    public class proltfolioItemsController : Controller
    {
        private readonly IUnitOfWork<proltfolioItem> unitOfWork;
        private readonly IHostingEnvironment _hosting;

        public proltfolioItemsController(IUnitOfWork<proltfolioItem> unitOfWork,IHostingEnvironment hosting)
        {
            this.unitOfWork = unitOfWork;
            this._hosting = hosting;
        }

        // GET: proltfolioItems
        public IActionResult Index()
        {
            return View(unitOfWork.Entity.GetAll());
        }

        // GET: proltfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proltfolioItem = unitOfWork.Entity.GetById(id);
                
            if (proltfolioItem == null)
            {
                return NotFound();
            }

            return View(proltfolioItem);
        }

        // GET: proltfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: proltfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ViewModelProtfolio model)
        {
            if (ModelState.IsValid)
            {
                if(model.File!=null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath,@"img\portfolio");
                    string fullpath = Path.Combine(uploads,model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                proltfolioItem proltfolioItem = new proltfolioItem() 
                {
                  Description=model.Description,
                  ProjectName=model.ProjectName,
                  ImageURL=model.File.FileName
                };
                unitOfWork.Entity.Insert(proltfolioItem);
                unitOfWork.save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: proltfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proltfolioItem =unitOfWork.Entity.GetById(id) ;
            if (proltfolioItem == null)
            {
                return NotFound();
            }
            ViewModelProtfolio viewModelProtfolio = new ViewModelProtfolio() 
            { 
                 ID=proltfolioItem.ID,
                  Description=proltfolioItem.Description,
                  ImageURL=proltfolioItem.ImageURL,
                  ProjectName=proltfolioItem.ProjectName
            };
            return View(viewModelProtfolio);
        }

        // POST: proltfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ViewModelProtfolio model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string upload = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(upload, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                 


                    proltfolioItem proltfolioItem = new proltfolioItem()
                    {
                        ID = model.ID,
                        ProjectName=model.ProjectName,
                        Description=model.Description,
                         ImageURL=model.File.FileName
                    
                    };
                    unitOfWork.Entity.Update(proltfolioItem);
                    unitOfWork.save() ;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!proltfolioItemExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: proltfolioItems/Delete/5
        public  IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proltfolioItem = unitOfWork.Entity.GetById(id);
                
            if (proltfolioItem == null)
            {
                return NotFound();
            }

            return View(proltfolioItem);
        }

        // POST: proltfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
             unitOfWork.Entity.GetById(id);
            unitOfWork.save();
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool proltfolioItemExists(Guid id)
        {
            return unitOfWork.Entity.GetAll().Any(e => e.ID == id);
        }
    }
}
