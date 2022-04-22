using GoGoGym.Data;
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
    public class WorkoutController : Controller 
    {
        // GET: Workouts
        public ActionResult Index()
        {
           
            var service = new WorkoutService();
            var model = service.GetWorkouts();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(WorkoutCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateWorkoutsService();

            if (service.CreateWorkout(model))
            {
                TempData["SaveResult"] = "Your Workout was Created. Are you ready to design it?";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Workout could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateWorkoutsService();
            var model = svc.GetWorkoutById(id);

            return View(model);
        }

        private WorkoutService CreateWorkoutsService()
        {
            
            var service = new WorkoutService();
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateWorkoutsService();
            var detail = service.GetWorkoutById(id);
            var model =
                new WorkoutEdit
                {
                    Id = detail.Id,
                    Name = detail.Name,
                    //Rating = detail.Rating
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkoutEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateWorkoutsService();

            if (service.UpdateWorkout(model))
            {
                TempData["SaveResult"] = "Your workout was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your workout could not be updated.");

            return View(model);
        }

        public bool DeleteWorkout(int Id)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Workouts
                        .Single(e => e.Id == Id);

                ctx.Workouts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateWorkoutsService();
            var model = svc.GetWorkoutById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateWorkoutsService();

            service.DeleteWorkout(id);

            TempData["SaveResult"] = "Your workout was deleted";

            return RedirectToAction("Index");
        }
    }
}