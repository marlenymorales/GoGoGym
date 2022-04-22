using GoGoGym.Data;
using GoGoGym.Models;
using GoGoGymMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoGoGym.Services
{
    public class ExerciseService
    {
        public bool CreateExercise(ExerciseCreate model)
        {
            var entity =
                new Exercise()
                {
                 
                    Name = model.Name,
                    level = model.level
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Exercises.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ExerciseListItem> GetExercises() 
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Exercises                     
                        .Select(
                            e =>
                                new ExerciseListItem
                                {
                                    Id = e.Id,
                                    Name = e.Name,
                                    level = e.level
                                }
                        );

                return query.ToArray();
            }
        }

        public ExerciseDetail GetExerciseById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Exercises
                    .Single(e => e.Id == id);
                return
                    new ExerciseDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Sets = entity.Sets,
                        Reps = entity.Reps,
                        level = entity.level
                    };
            }
        }

        public bool UpdateExercise(ExerciseEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Exercises
                   .Single(e => e.Id == model.Id );

                entity.Name = model.Name;
                entity.Sets = model.Sets;
                entity.Reps = model.Reps;
                entity.level = model.level;

                return ctx.SaveChanges() == 1;
            }
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
    }
}
