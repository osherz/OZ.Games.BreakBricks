using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Characters;
using Break_Bricks.Items;
using Break_Bricks.BoardGame.Interface;
using System.Drawing;
using Game.Controls;
using Game.Other;

namespace Break_Bricks.Levels
{
    public class Brick : Square, IBrick
    {
        public Brick()
        {
            levelList = new ChooseCollection<Image>();
            levelList.ItemAdded += LevelList_ItemAdded;
            levelList.ItemRemoved += LevelList_ItemRemoved;
        }

        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private bool isBrokeCompletly;

        private bool isCanToBreak;

        private int levelNum = 1;
        /// <summary>
        /// List of lvls of brick
        /// </summary>
        private ChooseCollection<Image> levelList;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when the brick broke completly
        /// </summary>
        public event EventHandler BrokedCompletly;
        /// <summary>
        /// Occurs when brick broke lvl
        /// </summary>
        public event EventHandler BrokeLvl;
        /// <summary>
        /// Occurs when IsCanBreak changed
        /// </summary>
        public event EventHandler<bool> IsCanBreakChanged;
        /// <summary>
        /// Occurs when sum of lvls changed
        /// </summary>
        public event EventHandler<int> SumOfLvlsChanged;
        /// <summary>
        /// Occurs when number of levl of brick changed
        /// </summary>
        public event EventHandler<int> LevelNumChanged;
        /// <summary>
        /// Occurs when level added
        /// </summary>
        public event EventHandler<CollectionEventArgs<Image>> LevelAdded
        {
            add
            {
                levelList.ItemAdded += value;
            }

            remove
            {
                levelList.ItemRemoved -= value;
            }
        }
        /// <summary>
        /// Occurs when level removed
        /// </summary>
        public event EventHandler<CollectionEventArgs<Image>> LevelRemoved
        {
            add
            {
                levelList.ItemAdded += value;
            }

            remove
            {
                levelList.ItemRemoved -= value;
            }
        }
        #endregion Events

        #region Raise event methods
        /// <summary>
        /// Raise the BrokeCompletly event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBrokedCompletly(EventArgs e)
        {

            BrokedCompletly?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the BrokeLvl event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBrokeLvl(EventArgs e)
        {

            BrokeLvl?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the IsCanBreakChanged event
        /// </summary>
        /// <param name="isCanToBreak"></param>
        protected virtual void OnIsCanToBreakChanged(bool isCanToBreak)
        {

            IsCanBreakChanged?.Invoke(this, isCanToBreak);
        }

        /// <summary>
        /// Raise the IsCanBreakChanged event
        /// </summary>
        /// <param name="sumOfLvls"></param>
        protected virtual void OnSumOfLvlsChanged(int sumOfLvls)
        {

            SumOfLvlsChanged?.Invoke(this, sumOfLvls);
        }

        protected virtual void OnLevelNumChanged(int levelNum)
        {

            LevelNumChanged?.Invoke(this, levelNum);
        }
        #endregion Raise event methods

        #region Events methods

        protected virtual void LevelList_ItemRemoved(object sender, CollectionEventArgs<Image> e)
        {
        }

        protected virtual void LevelList_ItemAdded(object sender, CollectionEventArgs<Image> e)
        {
        }

        #endregion Events methods

        #region Properties
        public bool IsBrokeCompletly
        {
            get
            {
                return isBrokeCompletly;
            }

            set
            {
                isBrokeCompletly = value;
                if (IsBrokeCompletly)
                {
                    levelNum = -1;
                    OnBrokedCompletly(new EventArgs());
                }
                
            }
        }

        public bool IsCanToBreak
        {
            get
            {
                return isCanToBreak;
            }

            set
            {
                isCanToBreak = value;
                OnIsCanToBreakChanged(IsCanToBreak);
            }
        }

        public int SumOfLvls
        {
            get
            {
                return levelList.Count;
            }
        }

        public int SumOfLvlsLeft
        {
            get
            {
                if (levelNum < 1) return 0;
                else return levelList.Count - levelNum;
            }
        }

        public int LevelNum
        {
            get
            {
                return levelNum;
            }

            set
            {
                if(value >= -1)
                {
                    levelNum = value;
                    if(LevelNum > SumOfLvls && !IsBrokeCompletly)
                    {
                        IsBrokeCompletly = true;
                        levelNum = -1;
                    }
                    OnLevelNumChanged(LevelNum);
                }
            }
        }

        public ChooseCollection<Image> LevelList
        {
            get
            {
                return levelList;
            }
        }

        public override Image Image
        {
            get
            {
                if (LevelNum < 0 || LevelNum > levelList.Count || levelList.Count < 1) return null;
                return levelList[LevelNum - 1];
            }

            set
            {
                //base.Image = value;
            }
        }
        #endregion Properties

        public void BreakCompletly()
        {
            IsBrokeCompletly = true;
        }

        public void BreakLvl()
        {
            if(SumOfLvls > 0 && IsCanToBreak) LevelNum++;
        }

        protected override Game.Characters.Item Copy()
        {
            Brick brick = new Brick();
            base.CopyTo(brick);
            CopyTo(brick);
            return brick;
        }

        protected Brick CopyTo(Brick item)
        {
            item.IsCanToBreak = IsCanToBreak;
            item.levelList.AddRange(levelList.CopyList());
            item.levelNum = LevelNum;
            item.IsBrokeCompletly = IsBrokeCompletly;
            return item;
        }

        public void Reset()
        {
            IsBrokeCompletly = false;
            LevelNum = 1;
        }
    }
}
