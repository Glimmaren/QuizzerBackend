using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentPoints { get; set; }
        public bool IsWinner { get; set; }
        public bool isHost = true;

        public Player(string id, string name, int currentPoints)
        {
            Id = id;
            Name = name;
            CurrentPoints = currentPoints;
        }
    }
}
