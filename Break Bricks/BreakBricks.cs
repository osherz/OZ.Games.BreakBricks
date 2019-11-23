#define ForPublish
//#define CheckLevels

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Game.Characters;
using Break_Bricks.Items;
using Break_Bricks.Levels;
using Break_Bricks.BoardGame;
using Break_Bricks.Properties;
using Break_Bricks.Levels.Prizes;
using Break_Bricks.BoardGame.Interface;

namespace Break_Bricks
{
    public partial class BreakBricks : Form
    {
        private BoardGame.Board board;
        private Ball ball;
        private Plank plank;
        private Border border;
        private Timer timer;
        private Level<Board>[] levelArry;
        private SizeF sizeOfBrick = new SizeF(74 * 0.7f, 33 * 0.7f);
        private float topLocationOfLevel = 50;
        private Prize_MoreSpeed<Board> moreSpeed;
        private Prize_MoreBalls<Board> moreBalls;
        private Prize_ResizeBall<Board, Ball> ballBigger;
        private Prize_ResizePlank<Board, Plank> plankBigger;
        private Prize_MoreScore<Board> moreScore_50, moreScore_100, moreScore_1000;
        private Prize_AddTurns<Board> extraTurns;
        private TurnsItem turnsItem;
        private float heightOfPrize = 50;
        private Padding marginOfAllBricks = new Padding(1);
        private Game.Characters.StatusBar bar;  

        public BreakBricks()
        {
            InitializeComponent();

            bar = new Game.Characters.StatusBar();
            bar.Style = Game.Characters.DrawStyle.Image;
            bar.ImageStyle = Game.Characters.ImageStyle.Stretch;
            bar.Pen = new Pen(Color.Black);
            bar.Brush = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Red, Color.Blue);
            bar.Image = Resources.Plank;
            bar.Precent = 0.5f;
            bar.Location = new PointF(0, 300);
            bar.Size = new SizeF(80, 15);

            ball = new Items.Ball();
            ball.Size = new SizeF(30, 30);
            ball.Image = Resources.Ball2;
            ball.Style = Game.Characters.DrawStyle.Image;
            ball.Speed = 5;

            plank = new Plank();
            //plank.AutoSizeImage = true;
            plank.Size = new SizeF(150, 17);
            plank.Image = Resources.Plank;
            plank.Style = Game.Characters.DrawStyle.Image;
            plank.Speed = 18;

            turnsItem = new Items.TurnsItem();
            turnsItem.Image = Resources.Heart;
            turnsItem.Style = Game.Characters.DrawStyle.Image;

            border = new Border();

            board = new BoardGame.Board();


            board.Size = new SizeF(540, 607);
            board.Location = new PointF(174, 24);
            board.Ball = ball;
            board.Plank = plank;
            board.BorderPrototype = border;
            board.WidthOfBorders = 0.1f;
            board.IntervalBallSpeed = 1;
            board.DistancePlankFromBoardBottom = 5;
            board.GameEnded += Board_OnGameEnded;
            board.GameStarted += Board_OnGameStarted;
            board.ScoreAddByBorokeBrick = 7;
            board.TurnsImage = turnsItem;            

            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += new EventHandler(Timer_OnTick);

            InitilizePrizes();
            InitilizeLevels();
        }

        private void InitilizePrizes()
        {
            float speedOfFallPrize = 3;
            int moveAutoIntervalOfAllPrize = 1;
            ///
            ///moreSpeed
            ///
            moreSpeed = new Prize_MoreSpeed<Board>(ball);
            moreSpeed.Board = board;
            moreSpeed.MultiplySpeed = 1.3f;
            moreSpeed.TimeOfEffect = 2000;
            moreSpeed.Image = Resources.SpeedX2;
            moreSpeed.Style = Game.Characters.DrawStyle.Image;
            moreSpeed.MoveAutoInterval = moveAutoIntervalOfAllPrize;
            moreSpeed.Speed = speedOfFallPrize;
            moreSpeed.AutoSizeImage = true;
            moreSpeed.Height = heightOfPrize;
            moreSpeed.MaxOfPrizeCanFall = 1;
            moreSpeed.Tag = moreSpeed.GetType();
            ///
            ///moreBalls
            ///
            moreBalls = new Prize_MoreBalls<Board>();
            moreBalls.Board = board;
            moreBalls.SumOfExtraBalls = 10;
            moreBalls.Image = Resources.BallX3;
            moreBalls.Style = Game.Characters.DrawStyle.Image;
            moreBalls.MoveAutoInterval = moveAutoIntervalOfAllPrize;
            moreBalls.Speed = speedOfFallPrize;
            moreBalls.AutoSizeImage = true;
            moreBalls.Height = heightOfPrize;
            moreBalls.Tag = moreBalls.GetType();
            moreBalls.MaxOfPrizeCanFall = 1;
            ///
            ///ballBigger
            ///
            ballBigger = new Levels.Prizes.Prize_ResizeBall<BoardGame.Board, Items.Ball>(ball);
            ballBigger.Board = board;
            ballBigger.MultiplySize = 1.7f;
            ballBigger.TimeOfEffect = 2000;
            ballBigger.Image = Resources.BallBigger;
            ballBigger.Style = Game.Characters.DrawStyle.Image;
            ballBigger.TimerInterval = moveAutoIntervalOfAllPrize;
            ballBigger.Speed = speedOfFallPrize;
            ballBigger.AutoSizeImage = true;
            ballBigger.Height = heightOfPrize;
            ballBigger.MaxOfPrizeCanFall = 1;
            ballBigger.Tag = "ballResize";
            ///
            ///ballSmaller
            ///
            ballSmaller = new Levels.Prizes.Prize_ResizeBall<BoardGame.Board, Items.Ball>(ball);
            ballSmaller.Board = board;
            ballSmaller.MultiplySize = 0.8f;
            ballSmaller.TimeOfEffect = 2000;
            ballSmaller.Image = Resources.BallSmaller;
            ballSmaller.Style = Game.Characters.DrawStyle.Image;
            ballSmaller.TimerInterval = moveAutoIntervalOfAllPrize;
            ballSmaller.Speed = speedOfFallPrize;
            ballSmaller.AutoSizeImage = true;
            ballSmaller.Height = heightOfPrize;
            ballSmaller.MaxOfPrizeCanFall = 1;
            ballSmaller.Tag = "ballResize";

            ///
            ///plankBigger
            ///
            plankBigger = new Levels.Prizes.Prize_ResizePlank<BoardGame.Board, Items.Plank>(plank);
            plankBigger.Board = board;
            plankBigger.MultiplySize = 1.7f;
            plankBigger.TimeOfEffect = 2000;
            plankBigger.Image = Resources.PlankBigger;
            plankBigger.Style = Game.Characters.DrawStyle.Image;
            plankBigger.TimerInterval = moveAutoIntervalOfAllPrize;
            plankBigger.Speed = speedOfFallPrize;
            plankBigger.AutoSizeImage = true;
            plankBigger.Height = heightOfPrize;
            plankBigger.MaxOfPrizeCanFall = 1;
            plankBigger.Tag = "plankResize";
            ///
            ///plankSmaller
            ///
            plankSmaller = new Levels.Prizes.Prize_ResizePlank<BoardGame.Board, Items.Plank>(plank);
            plankSmaller.Board = board;
            plankSmaller.MultiplySize = 0.5f;
            plankSmaller.TimeOfEffect = 2000;
            plankSmaller.Image = Resources.PlankSmaller;
            plankSmaller.Style = Game.Characters.DrawStyle.Image;
            plankSmaller.TimerInterval = moveAutoIntervalOfAllPrize;
            plankSmaller.Speed = speedOfFallPrize;
            plankSmaller.AutoSizeImage = true;
            plankSmaller.Height = heightOfPrize;
            plankSmaller.MaxOfPrizeCanFall = 1;
            plankSmaller.Tag = "plankResize";

            ///
            ///moreScore_50
            ///
            moreScore_50 = new Prize_MoreScore<Board>();
            moreScore_50.Board = board;
            moreScore_50.ExrtaScore = 50;
            moreScore_50.Image = Resources.ScoreExtra50;
            moreScore_50.Style = Game.Characters.DrawStyle.Image;
            moreScore_50.TimerInterval = moveAutoIntervalOfAllPrize;
            moreScore_50.Speed = speedOfFallPrize;
            moreScore_50.AutoSizeImage = true;
            moreScore_50.Height = heightOfPrize;
            moreScore_50.Tag = moreScore_50.GetType();
            moreScore_50.MaxOfPrizeCanFall = -1;
            ///
            ///moreScore_100
            ///
            moreScore_100 = new Prize_MoreScore<Board>();
            moreScore_100.Board = board;
            moreScore_100.ExrtaScore = 100;
            moreScore_100.Image = Resources.ScoreExtra100;
            moreScore_100.Style = Game.Characters.DrawStyle.Image;
            moreScore_100.TimerInterval =moveAutoIntervalOfAllPrize;
            moreScore_100.Speed = speedOfFallPrize;
            moreScore_100.AutoSizeImage = true;
            moreScore_100.Height = heightOfPrize;
            moreScore_100.Tag = moreScore_100.GetType();
            moreScore_100.MaxOfPrizeCanFall = -1;
            ///
            ///moreScore_1000
            ///
            moreScore_1000 = new Prize_MoreScore<Board>();
            moreScore_1000.Board = board;
            moreScore_1000.ExrtaScore = 1000;
            moreScore_1000.Image = Resources.ScoreExtra1000;
            moreScore_1000.Style = Game.Characters.DrawStyle.Image;
            moreScore_1000.TimerInterval = moveAutoIntervalOfAllPrize;
            moreScore_1000.Speed = speedOfFallPrize;
            moreScore_1000.AutoSizeImage = true;
            moreScore_1000.Height = heightOfPrize;
            moreScore_1000.Tag = moreScore_1000.GetType();
            moreScore_1000.MaxOfPrizeCanFall = -1;
            ///
            ///extraTurns
            ///
            extraTurns = new Prize_AddTurns<Board>();
            extraTurns.Board = board;
            extraTurns.ExtraTurns = 1;
            extraTurns.Image = Resources.Turn;
            extraTurns.Style = Game.Characters.DrawStyle.Image;
            extraTurns.TimerInterval = moveAutoIntervalOfAllPrize;
            extraTurns.Speed = speedOfFallPrize;
            extraTurns.AutoSizeImage = true;
            extraTurns.Height = heightOfPrize;
            extraTurns.Tag = extraTurns.GetType();
            extraTurns.MaxOfPrizeCanFall = 1;

        }

        private void InitilizeLevels()
        {
            levelArry = new Levels.Level<BoardGame.Board>[10];
            ///
            ///level 1
            ///
            levelArry[0] = new Levels.Level<BoardGame.Board>();
            levelArry[0].ParentBoard = board;
            Brick copyBrick = Bricks.Reg1;
            for (int i = 0; i < 6; i++)
            {
                levelArry[0].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    levelArry[0].InsertToRow(levelArry[0].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            levelArry[0].MarginOfAllBrick = marginOfAllBricks;
            levelArry[0].SizeOfBrick = sizeOfBrick;
            levelArry[0].AutoSize = true;
            levelArry[0].Top = topLocationOfLevel;
            levelArry[0].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[0].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[0].AddPrize(
                moreScore_50,
                moreBalls,
                moreSpeed);
            levelArry[0].PrizePrecent = 0.2;
            levelArry[0].ToMiddleFromLeft();
            ///
            ///level 2
            ///
            levelArry[1] = new Levels.Level<BoardGame.Board>();
            levelArry[1].ParentBoard = board;
            AddRowToLevel(1, 8, Bricks.Reg2);
            copyBrick = Bricks.Reg1;
            for (int i = 0; i < 6; i++)
            {
                levelArry[1].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0 || j == 7) copyBrick = Bricks.Reg2;
                    else copyBrick = Bricks.Reg1;
                    levelArry[1].InsertToRow(levelArry[1].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(1, 8, Bricks.Reg2);
            levelArry[1].MarginOfAllBrick = marginOfAllBricks;
            levelArry[1].SizeOfBrick = sizeOfBrick;
            levelArry[1].AutoSize = true;
            levelArry[1].Top = topLocationOfLevel;
            levelArry[1].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[1].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[1].AddPrize(
                moreScore_50,
                moreBalls,
                moreSpeed);
            levelArry[1].PrizePrecent = 0.2;
            levelArry[1].ToMiddleFromLeft();
            ///
            ///level 3
            ///
            levelArry[2] = new Levels.Level<BoardGame.Board>();
            levelArry[2].ParentBoard = board;
            AddRowToLevel(2, 8, Bricks.Broke2Lvls);
            copyBrick = Bricks.Reg1;
            for (int i = 0; i < 5; i++)
            {
                levelArry[2].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0 || j == 7) copyBrick = Bricks.Broke2Lvls;
                    else if (j == 1 || j == 6) copyBrick = Bricks.Reg1;
                    else copyBrick = Bricks.Reg2;
                    levelArry[2].InsertToRow(levelArry[2].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            for (int i = 0; i < 2; i++) AddRowToLevel(2, 8, Bricks.Broke2Lvls);
            levelArry[2].MarginOfAllBrick = marginOfAllBricks;
            levelArry[2].SizeOfBrick = sizeOfBrick;
            levelArry[2].AutoSize = true;
            levelArry[2].Top = topLocationOfLevel;
            levelArry[2].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[2].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[2].AddPrize(
                moreScore_50, moreScore_50, moreScore_50, moreScore_50,
                moreScore_100, moreScore_100,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                extraTurns);
            levelArry[2].PrizePrecent = 0.3;
            levelArry[2].ToMiddleFromLeft();
            ///
            ///level 4
            ///
            levelArry[3] = new Levels.Level<BoardGame.Board>();
            levelArry[3].ParentBoard = board;
            for (int i = 0; i < 2; i++) AddRowToLevel(3, 8, Bricks.Broke2Lvls);
            copyBrick = Bricks.Reg2;
            for (int i = 0; i < 5; i++)
            {
                levelArry[3].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (j <=1 || j >= 6) copyBrick = Bricks.Broke2Lvls;
                    else copyBrick = Bricks.Reg2;
                    levelArry[3].InsertToRow(levelArry[3].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            for(int i = 0; i<2;i++) AddRowToLevel(3, 8, Bricks.Broke2Lvls);
            levelArry[3].MarginOfAllBrick = marginOfAllBricks;
            levelArry[3].SizeOfBrick = sizeOfBrick;
            levelArry[3].AutoSize = true;
            levelArry[3].Top = topLocationOfLevel;
            levelArry[3].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[3].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[3].AddPrize(
                moreScore_50, moreScore_50,
                moreScore_100, moreScore_100,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                extraTurns);
            levelArry[3].PrizePrecent = 0.4;
            levelArry[3].ToMiddleFromLeft();
            ///
            ///level 5
            ///
            levelArry[4] = new Levels.Level<BoardGame.Board>();
            levelArry[4].ParentBoard = board;
            copyBrick = Bricks.Broke2Lvls;
            for (int i = 0; i < 7; i++)
            {
                levelArry[4].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    levelArry[4].InsertToRow(levelArry[4].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(4, 8, Bricks.CantBreak);
            levelArry[4].MarginOfAllBrick = marginOfAllBricks;
            levelArry[4].SizeOfBrick = sizeOfBrick;
            levelArry[4].AutoSize = true;
            levelArry[4].Top = topLocationOfLevel;
            levelArry[4].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[4].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[4].AddPrize(
                moreScore_50, moreScore_50,
                moreScore_100, moreScore_100,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                extraTurns);
            levelArry[4].PrizePrecent = 0.5;
            levelArry[4].ToMiddleFromLeft();
            ///
            ///level 6
            ///
            levelArry[5] = new Levels.Level<BoardGame.Board>();
            levelArry[5].ParentBoard = board;
            AddRowToLevel(5, 8, Bricks.CantBreak);
            copyBrick = Bricks.Broke2Lvls;
            for (int i = 0; i < 8; i++)
            {
                levelArry[5].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    levelArry[5].InsertToRow(levelArry[5].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(5, 8, Bricks.CantBreak);
            levelArry[5].MarginOfAllBrick = marginOfAllBricks;
            levelArry[5].SizeOfBrick = sizeOfBrick;
            levelArry[5].AutoSize = true;
            levelArry[5].Top = topLocationOfLevel;
            levelArry[5].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[5].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[5].AddPrize(
                moreScore_50, moreScore_50,
                moreScore_100, moreScore_100, moreScore_100, moreScore_100,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                plankBigger, plankBigger,
                plankBigger, plankBigger,
                extraTurns);
            levelArry[5].PrizePrecent = 0.57;
            levelArry[5].ToMiddleFromLeft();
            ///
            ///level 7
            ///
            levelArry[6] = new Levels.Level<BoardGame.Board>();
            levelArry[6].ParentBoard = board;
            copyBrick = Bricks.Broke2Lvls;
            AddRowToLevel(6, 8, Bricks.CantBreak);
            for (int i = 0; i < 8; i++)
            {
                levelArry[6].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0 || j == 7) copyBrick = Bricks.Broke3Lvls;
                    else copyBrick = Bricks.Broke2Lvls;
                    levelArry[6].InsertToRow(levelArry[6].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(6, 8, Bricks.CantBreak);
            levelArry[6].MarginOfAllBrick = marginOfAllBricks;
            levelArry[6].SizeOfBrick = sizeOfBrick;
            levelArry[6].AutoSize = true;
            levelArry[6].Top = topLocationOfLevel;
            levelArry[6].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[6].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[6].AddPrize(
                moreScore_100, moreScore_100, moreScore_100, moreScore_100,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                plankBigger, plankBigger,
                plankSmaller, plankSmaller,
                extraTurns);
            levelArry[6].PrizePrecent = 0.65;
            levelArry[6].ToMiddleFromLeft();
            ///
            ///level 8
            ///
            levelArry[7] = new Levels.Level<BoardGame.Board>();
            levelArry[7].ParentBoard = board;
            copyBrick = Bricks.Broke3Lvls;
            AddRowToLevel(7, 8, Bricks.CantBreak);
            for (int i = 0; i < 9; i++)
            {
                levelArry[7].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    levelArry[7].InsertToRow(levelArry[7].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(7, 8, Bricks.CantBreak);
            levelArry[7].MarginOfAllBrick = marginOfAllBricks;
            levelArry[7].SizeOfBrick = sizeOfBrick;
            levelArry[7].AutoSize = true;
            levelArry[7].Top = topLocationOfLevel;
            levelArry[7].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[7].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[7].AddPrize(
                moreScore_100, moreScore_100, moreScore_100,
                moreScore_1000,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                plankBigger, plankBigger,
                plankSmaller, plankSmaller,
                extraTurns);
            levelArry[7].PrizePrecent = 0.68;
            levelArry[7].ToMiddleFromLeft();
            ///
            ///level 9
            ///
            levelArry[8] = new Levels.Level<BoardGame.Board>();
            levelArry[8].ParentBoard = board;
            copyBrick = Bricks.Broke3Lvls;
            for (int i = 0; i < 9; i++)
            {
                levelArry[8].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (i > 0 && (j == 0 || j == 7)) copyBrick = Bricks.CantBreak;
                    else copyBrick = Bricks.Broke3Lvls;
                    levelArry[8].InsertToRow(levelArry[8].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(8, 8, Bricks.CantBreak);
            levelArry[8].MarginOfAllBrick = marginOfAllBricks;
            levelArry[8].SizeOfBrick = sizeOfBrick;
            levelArry[8].AutoSize = true;
            levelArry[8].Top = topLocationOfLevel;
            levelArry[8].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[8].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[8].AddPrize(
                moreScore_100, moreScore_100,
                moreScore_1000, moreScore_1000,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                plankBigger, plankBigger,
                plankSmaller, plankSmaller,
                extraTurns);
            levelArry[8].PrizePrecent = 0.7;
            levelArry[8].ToMiddleFromLeft();
            ///
            ///level 10
            ///
            levelArry[9] = new Levels.Level<BoardGame.Board>();
            levelArry[9].ParentBoard = board;
            copyBrick = Bricks.Broke3Lvls;
            AddRowToLevel(9, 5, Bricks.CantBreak);
            for (int i = 0; i < 9; i++)
            {
                levelArry[9].AddRow();
                for (int j = 0; j < 8; j++)
                {
                    if (i > 1 && (j == 0 || j == 7)) copyBrick = Bricks.CantBreak;
                    else copyBrick = Bricks.Broke3Lvls;
                    levelArry[9].InsertToRow(levelArry[9].SumOfRow - 1, (Brick)copyBrick.Clone());
                }
            }
            AddRowToLevel(9, 8, Bricks.CantBreak);
            levelArry[9].MarginOfAllBrick = marginOfAllBricks;
            levelArry[9].SizeOfBrick = sizeOfBrick;
            levelArry[9].AutoSize = true;
            levelArry[9].Top = topLocationOfLevel;
            levelArry[9].BrickBroke += new EventHandler<IBrick>(Lvl_BrickBroke);
            levelArry[9].PrizeGenerated += new EventHandler<IPrize<Board>>(Lvl_PrizeGenerated);
            levelArry[9].AddPrize(
                moreScore_1000, moreScore_1000, moreScore_1000, moreScore_1000,
                moreBalls, moreBalls,
                moreSpeed, moreSpeed,
                ballBigger, ballBigger,
                ballSmaller, ballSmaller,
                plankBigger, plankBigger,
                plankSmaller, plankSmaller,
                extraTurns);
            levelArry[9].PrizePrecent = 0.9;
            levelArry[9].ToMiddleFromLeft();


            board.AddLevel(levelArry);
        }

        private void AddRowToLevel(int levelNumIndex, int sumOfBricks, Brick brick)
        {
            levelArry[levelNumIndex].AddRow();
            for(int i = 0; i<sumOfBricks; i++)
            {
                levelArry[levelNumIndex].InsertToRow(levelArry[levelNumIndex].SumOfRow - 1, (Brick)brick.Clone());
            }
        }

        private void Lvl_PrizeGenerated(object sender, IPrize<Board> e)
        {

        }

        private void Board_OnGameEnded(object sender, bool isWin)
        {
            StringBuilder endText = new StringBuilder();
            if(isWin)
            {
                endText.AppendLine("יפה!");
                endText.AppendLine("סיימת/ה את כל השלבים!");
            }
            else
            {
                endText.AppendLine("אוי, נגמרו לך כל התורות");
                endText.AppendLine("בהצלחה בפעם הבאה! :)");
            }
            endText.AppendLine("הניקוד שלך הוא:");
            endText.Append(board.Score.ToString());
            endLabel.Text = endText.ToString();
            endLabel.Visible = true;
        }

        private void Board_OnGameStarted(object sender, EventArgs e)
        {
            endLabel.Visible = false;
        }

        private void Lvl_BrickBroke(object sender, BoardGame.Interface.IBrick e)
        {
        }

        private void Timer_OnTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private int level = 1;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            timer.Start();
            board.Score = 8000;
            board.Start(5,level);
            ball.LocationChanged += Ball_LocationChanged;
        }

        private void Ball_LocationChanged(object sender, PointF e)
        {
        }

        Rectangle newGameRect = new Rectangle(804, 373, 266, 40);
        Rectangle stopRect = new Rectangle(837, 441, 129, 36);
        Rectangle continueRect = new Rectangle(908, 498, 130, 38);
        Rectangle exitRect = new Rectangle(786, 556, 137, 42);
        Rectangle destroyRect = new Rectangle(50, 495, 79, 103);
        protected override void OnMouseMove(MouseEventArgs e)
        {
#if Check
            Text = e.Location.ToString();
            //ball.Location = new PointF(e.X - board.Left, e.Y - board.Top);
#endif
            Rectangle mouseRect = new Rectangle(e.Location, new Size(1, 1));
            if (newGameRect.IntersectsWith(mouseRect))
            {
                Cursor = Cursors.Hand;
                if (e.Button == MouseButtons.Left)
                {
                    board.EndGame(false);
                    board.Score = 8000;
                    board.Start(3,10);
                }
            }
            else if (stopRect.IntersectsWith(mouseRect))
            {
                Cursor = Cursors.Hand;
                if (e.Button == MouseButtons.Left) board.Pause();
            }
            else if (continueRect.IntersectsWith(mouseRect))
            {
                Cursor = Cursors.Hand;
                if (e.Button == MouseButtons.Left) board.Continue();
            }
            else if (exitRect.IntersectsWith(mouseRect))
            {
                Cursor = Cursors.Hand;
                if (e.Button == MouseButtons.Left) this.Close();
            }
            else if (destroyRect.Contains(e.Location))
            {
                Cursor = Cursors.Hand;
                if (e.Button == MouseButtons.Left) board.DestroyBalls();
            }
            else
            {
                Cursor = Cursors.Default;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Rectangle mouseRect = new Rectangle(e.Location, new Size(1, 1));
            if (e.Button == MouseButtons.Left)
            {
                if (newGameRect.IntersectsWith(mouseRect))
                {
                    board.EndGame(false);
                    board.Start(5);
                }
                else if (stopRect.IntersectsWith(mouseRect))
                {
                    board.Pause();
                }
                else if (continueRect.IntersectsWith(mouseRect))
                {
                    board.Continue();
                }
                else if (exitRect.IntersectsWith(mouseRect))
                {
                    this.Close();
                }
                else if (destroyRect.Contains(e.Location))
                {
                    if(!board.IsEndGame) board.DestroyBalls();
                }
            }
            base.OnMouseClick(e);
        }

        private RectangleF levelImageRect = new RectangleF(34, 23, 112, 54);
        private RectangleF levelRect = new RectangleF(48, 126, 100 - 48, 198 - 126);
        private Image[] levelNumsImage = 
            new Image[] { Resources.Level1, Resources.Level2, Resources.Level3, Resources.Level4, Resources.Level5,
                Resources.Level6, Resources.Level7, Resources.Level8, Resources.Level9, Resources.Level10};
        private Font scoreTextFont = 
            new Font(
                "Jokerman",
                50
                //FontStyle.Bold
                );
        private Rectangle textScoreRect = new Rectangle(747, 130, 345, 100);
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            string text = "000000";
            string score = board.Score.ToString();
            text = text.Remove(text.Length - score.Length);
            text = text.Insert(text.Length, score);
            TextRenderer.DrawText(g, text, scoreTextFont,
                textScoreRect, Color.White, TextFormatFlags.HorizontalCenter);

            Image lvlNum = levelNumsImage[board.LevelNum - 1];
            PointF levelnumLocation = new PointF(levelImageRect.Left + (levelImageRect.Width - lvlNum.Width) / 2, levelImageRect.Bottom);
            g.DrawImage(Resources.LevelBackGround, 0, levelImageRect.Bottom);
            g.DrawImage(lvlNum, levelRect.Left + (levelRect.Width - lvlNum.Width) / 2, levelRect.Top + (levelRect.Height - lvlNum.Height) / 2 - 10);

            //g.DrawImage(lvlNum, levelnumLocation);
            //TextRenderer.DrawText(e.Graphics, (board.LevelNum).ToString(), new Font("Ravie", 50, FontStyle.Bold),
            //    textLevelRect, Color.Blue, TextFormatFlags.HorizontalCenter);

            board.Draw(e.Graphics);
            //bar.Draw(g);
            //bar.Precent -= 0.001f;
            g = null;
            base.OnPaint(e);
        }

        private bool rightArrowPressed = false;
        private bool leftArrowPressed = false;
        private Prize_ResizeBall<Board, Ball> ballSmaller;
        private Prize_ResizePlank<Board, Plank> plankSmaller;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightArrowPressed = true; ;
                    if (!e.Control)
                        board.Plank_GoRight();
                    else
                        board.Ball_GoRight();
                    break;
                case Keys.Left:
                    leftArrowPressed = true;
                    if (!e.Control)
                        board.Plank_GoLeft();
                    else
                        board.Ball_GoLeft();
                    break;
#if CheckLevels
                case Keys.Down:
                    level--;
                    board.Start(5, level);
                    break;
                case Keys.Up:
                    level++;
                    board.Start(5, level);
                    break;
#endif
                case Keys.Space:
                    board.ShootBall();
                    break;
                case Keys.CapsLock:
                    if (!board.IsPause) board.Pause();
                    else board.Continue();
                    break;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightArrowPressed = false;
                    break;
                case Keys.Left:
                    leftArrowPressed = false;
                    break;
            }
            if (!rightArrowPressed && !leftArrowPressed) board.Plank_StopMove();
            else if (rightArrowPressed) board.Plank_GoRight();
            else if (leftArrowPressed) board.Plank_GoLeft();
            base.OnKeyUp(e);
        }
    }
}
