using DecisionTechProductBasket.Models;
using DecisionTechProductBasket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DecisionTechProductBasket.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            this._basketService = basketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            return View(this._basketService.GetCurrentBasket());
        }

        // GET: Basket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Basket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Basket/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Basket basket)
        {
            return View(basket);
        }

        // GET: Basket/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Edit");
        }

        // POST: Basket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Basket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Basket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
