using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class StartFirstToPointMessage
    {
        public string Type { get; set; }
        public int PointsToWin { get; set; }
        public ContentModel Data { get; set; }
        public List<Player> Players { get; set; }
    }
}
