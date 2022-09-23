using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTry.Models;
using System.Data.SqlClient;

namespace FirstTry.Controllers
{
    public class ProductController : Controller
    {

        //GET: Product/
        public IActionResult Index()
        {
            DBHandler dbhandler = new DBHandler();
            ModelState.Clear();
            return View(dbhandler.GetAll());
        }

        //GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Product/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBHandler dbhandler = new DBHandler();
                    if (dbhandler.Insert(product))
                    {
                        ViewBag.Message = "Product Berhasil Ditambahkan";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/
        public ActionResult Edit(int id)
        {
            DBHandler dbhandler = new DBHandler();
            return View(dbhandler.GetAll().Find(item => item.Id == id));
        }

        // POST: Product/Edit/
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                DBHandler dbhandler = new DBHandler();
                dbhandler.Update(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/
        public ActionResult Delete(int id)
        {
            try
            {
                DBHandler dbhandler = new DBHandler();
                if (dbhandler.Delete(id))
                {
                    ViewBag.AlertMsg = "Berhasil Dihapus";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
