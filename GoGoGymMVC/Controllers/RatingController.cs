using GoGoGym.Models;
using GoGoGym.Services;
using GoGoGymMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoGoGymMVC.Controllers
{
    public class RatingController : Controller
    {
        // GET: Workouts
        public ActionResult Index()
        {
           
            var service = new RatingService();
            var model = service.GetRating();

            return View(model);
        }

        // GET: Ratings
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(RatingCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRatingsService();

            if (service.CreateRating(model))
            {
                TempData["SaveResult"] = "Your Rating was Created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Rating could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRatingsService();
            var model = svc.GetRatingById(id);

            return View(model);
        }

        private RatingService CreateRatingsService()
        {
            var service = new RatingService();
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRatingsService();
            var detail = service.GetRatingById(id);
            var model =
                new RatingEdit
                {
                    Id = detail.Id,
                    ExertionScore = detail.ExertionScore,
                    EnjoymentScore = detail.EnjoymentScore,
                    HeartrateScore = detail.HeartrateScore
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RatingEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRatingsService();

            if (service.UpdateRating(model))
            {
                TempData["SaveResult"] = "Your rating was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your rating could not be updated.");

            return View(model);
        }

        public bool DeleteRating(int Id)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.Id == Id );

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRatingsService();
            var model = svc.GetRatingById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRatingsService();

            service.DeleteRating(id);

            TempData["SaveResult"] = "Your rating was deleted";

            return RedirectToAction("Index");
        }
    }
}