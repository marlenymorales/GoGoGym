using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Data
{
    public enum Level
    {
        Easy = 1,
        Intermediate = 2,
        Hard = 3
    }
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public Level level { get; set; }

        [Required]
        [ForeignKey("Workouts")]
        public int WorkoutId { get; set; }
        public virtual Workout Workouts { get; set; }
    }
}
