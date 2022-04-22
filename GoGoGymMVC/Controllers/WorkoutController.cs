using GoGoGym.Data;
using GoGoGym.Models;
using GoGoGymMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoGoGymMVC.Controllers
{
    public class WorkoutController
    {
        public bool CreateWorkout(WorkoutCreate model)
        {
            var entity =
                new Workout()
                {
                    Name = model.Name,
                     Ratings = model.Rating,
                    CreatedUtc = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Workouts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<WorkoutListItem> GetWorkouts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Workouts
                       // .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                            //{ /*var ratings = e.Ratings;*/



                            new WorkoutListItem
                            {
                                Id = e.Id,
                                Name = e.Name,
                                CreatedUtc = e.CreatedUtc
                            }

                        );

                return query.ToArray();
            }
        }

        public WorkoutDetail GetWorkoutById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Workouts
                    .Single(e => e.Id == id);
                return
                    new WorkoutDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                       // Ratings = entity.Rating,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdateWorkout(WorkoutEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Workouts
                    .Single(e => e.Id == model.Id);

                entity.Name = model.Name;
                entity.Ratings = model.Rating;
                entity.CreatedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
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
    }
}