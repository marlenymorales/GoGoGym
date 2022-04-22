using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGoGym.Data
{
    public class Rating
    {
        public int Id { get; set; }
        public double HeartrateScore { get; set; }
        public double EnjoymentScore { get; set; } 
        public double ExertionScore { get; set; }
        public double AverageScore
        {
            get
            {
                return (HeartrateScore + EnjoymentScore + ExertionScore * 2) / 4;
            }
        }

    }
}
