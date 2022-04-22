using GoGoGym.Data;
using GoGoGym.Models;
using GoGoGymMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Services
{
    public class RatingService
    {
        public bool CreateRating(RatingCreate model)
        {
            var entity =
                new Rating()
                {                   
                    ExertionScore = model.ExertionScore,
                    EnjoymentScore = model.EnjoymentScore,
                    HeartrateScore = model.HeartrateScore
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ratings.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RatingListItem> GetRating()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Ratings
                        .Select(
                            e =>
                                new RatingListItem
                                {
                                    Id = e.Id,
                                    AverageScore = e.AverageScore
                                }
                        );

                return query.ToArray();
            }
        }

        public RatingDetail GetRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Ratings
                    .Single(e => e.Id == id);
                return
                    new RatingDetail
                    {
                        Id = entity.Id,
                        ExertionScore = entity.ExertionScore,
                        EnjoymentScore = entity.EnjoymentScore,
                        HeartrateScore = entity.HeartrateScore
                    };
            }
        }

        public bool UpdateRating(RatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Ratings
                    .Single(e => e.Id == model.Id );

                entity.Id = model.Id;
                entity.ExertionScore = model.ExertionScore;
                entity.EnjoymentScore = model.EnjoymentScore;
                entity.HeartrateScore = model.HeartrateScore;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.Id == Id);

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
