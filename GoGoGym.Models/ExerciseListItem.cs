using GoGoGym.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Models
{
    public class ExerciseListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Level level { get; set; }
    }
}
