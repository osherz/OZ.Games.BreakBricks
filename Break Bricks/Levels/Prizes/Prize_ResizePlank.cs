using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Prizes
{
    public class Prize_ResizePlank<TBoard, TObjectToResize> : Resize<TBoard, float, SizeF, PointF, TObjectToResize>, IPrize<TBoard>
        where TBoard : IBoard
        where TObjectToResize : ILocation<float, PointF>, ISize<float, SizeF>
    {
        public Prize_ResizePlank(TObjectToResize objectToResize) : base(objectToResize)
        {
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override void ResizeObject(TObjectToResize objectToResize, float multiplySize)
        {
            objectToResize.Left -= (objectToResize.Width * multiplySize - objectToResize.Width) / 2;
            objectToResize.Width *= multiplySize;
        }

        protected override void BackToFirstSizeObject(TObjectToResize objectToResize, float multiplySize)
        {
            objectToResize.Left += (objectToResize.Width - objectToResize.Width / multiplySize) / 2;
            objectToResize.Width /= multiplySize;
        }

        #region Copy methods
        protected override Game.Characters.Item Copy()
        {
            Prize_ResizePlank<TBoard, TObjectToResize> prize = new Prizes.Prize_ResizePlank<TBoard, TObjectToResize>(ObjectToResize);
            CopyTo(prize);
            return prize;
        }

        protected void CopyTo(Prize_ResizePlank<TBoard, TObjectToResize> prize)
        {

        }
        #endregion Copy methods
    }
}
