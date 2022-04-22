using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Data
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkoutLength { get; set; }
        public Level level { get; set; }
        public string Exercise { get; set; }
        public double Ratings { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
