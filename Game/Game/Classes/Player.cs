using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Player
    {
        ///
        ///event
        ///
        public event EventHandler<string> ChangeName;
        public event EventHandler<int> ChangeScore;
        public event EventHandler<string> ErrorScore;
        /// <summary>
        /// שם השחקן
        /// </summary>
        private string name;
        /// <summary>
        /// ניקוד
        /// </summary>
        private int score;

        public Player() { }

        public Player(string name)
        {
            Name = name;
        }

        public Player(string name, int score) : this(name)
        {
            Score = score;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                ChangeName?.Invoke(this, Name);
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                if (value >= 0)
                {
                    score = value;
                    ChangeScore?.Invoke(this, Score);
                }
                else
                {
                    ErrorScore?.Invoke(this, string.Format("Score: {0}.The score of Player {1} must be 0 or bigger", Score, Name));
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Player name is: {0}, score: {1}", Name, Score);
        }
    }
}
