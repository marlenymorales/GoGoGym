using GoGoGym.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Models
{
    public class RatingDetail
    {
        public int Id { get; set; }

        [ForeignKey("Workouts")]
        public virtual Workout Workout { get; set; }
        public double ExertionScore { get; set; }
        public double EnjoymentScore { get; set; }
        public double HeartrateScore { get; set; }
    }
}
