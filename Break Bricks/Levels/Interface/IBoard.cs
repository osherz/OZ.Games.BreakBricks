using Break_Bricks.BoardGame.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Interface
{
    public interface IBoard : IItemF
    {
        IPlank Plank { get; set; }
        IBall Ball { get; set; }
        int Score { get; set; }
        int Turns { get; set; }

        event EventHandler BallFailed;

        float BallSpeed { get; set; }

        void AddBall(int sum, bool kefelBySumOfBallsExict = false);
    }
}
