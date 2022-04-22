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
  
        [Authorize]
        public class ExerciseController : Controller
        {
            // GET: Exercise
            public ActionResult Index()
            {
                var service = new ExerciseService();
                var model = service.GetExercises();

                return View(model);
            }


            //GET
            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public ActionResult Create(ExerciseCreate model)
            {
                if (!ModelState.IsValid) return View(model);

                var service = CreateExerciseService();

                if (service.CreateExercise(model))
                {
                    TempData["SaveResult"] = "Your Exercise was Created.";
                    return RedirectToAction("Index");
                };

                ModelState.AddModelError("", "Exercise could not be created.");

                return View(model);
            }

            public ActionResult Details(int id)
            {
                var svc = CreateExerciseService();
                var model = svc.GetExerciseById(id);

                return View(model);
            }

            private ExerciseService CreateExerciseService()
            {
                var service = new ExerciseService();
                return service;
            }

            public ActionResult Edit(int id)
            {
                var service = CreateExerciseService();
                var detail = service.GetExerciseById(id);
                var model =
                    new ExerciseEdit
                    {
                        Id = detail.Id,
                        Name = detail.Name,
                        Reps = detail.Reps,
                        Sets = detail.Sets
                    };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, ExerciseEdit model)
            {
                if (!ModelState.IsValid) return View(model);

                if (model.Id != id)
                {
                    ModelState.AddModelError("", "Id Mismatch");
                    return View(model);
                }

                var service = CreateExerciseService();

                if (service.UpdateExercise(model))
                {
                    TempData["SaveResult"] = "Your exercise was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your exercise could not be updated.");

                return View(model);
            }

            public bool DeleteExercise(int Id) 
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Exercises
                            .Single(e => e.Id == Id);

                    ctx.Exercises.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }

            [ActionName("Delete")]
            public ActionResult Delete(int id)
            {
                var svc = CreateExerciseService();
                var model = svc.GetExerciseById(id);

                return View(model);
            }

            [HttpPost]
            [ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeletePost(int id)
            {
                var service = CreateExerciseService();

                service.DeleteExercise(id);

                TempData["SaveResult"] = "Your exercise was deleted";

                return RedirectToAction("Index");
            }
        }
}