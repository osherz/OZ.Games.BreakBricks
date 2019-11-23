using Break_Bricks.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Prizes
{
    static class Bricks
    {
        private static SizeF sizeOfAll;

        static public Brick Reg1
        {
            get
            {
                Brick brick = new Brick();
                brick.LevelList.Add(Resources.Brick1);
                brick.Style = Game.Characters.DrawStyle.Image;
                brick.IsCanToBreak = true;
                brick.Size = SizeOfAll;
                return brick;
            }
        }

        static public Brick Reg2
        {
            get
            {
                Brick brick = new Brick();
                brick.LevelList.Add(Resources.Brick2);
                brick.Style = Game.Characters.DrawStyle.Image;
                brick.IsCanToBreak = true;
                brick.Size = SizeOfAll;
                return brick;
            }
        }

        static public Brick Broke2Lvls
        {
            get
            {
                Brick brick = new Brick();
                brick.LevelList.AddRange(
                    new Image[] {
                        Resources.Brick4,
                        Resources.Brick4Broken1 });
                brick.Style = Game.Characters.DrawStyle.Image;
                brick.IsCanToBreak = true;
                brick.Size = SizeOfAll;
                return brick;
            }
        }

        static public Brick Broke3Lvls
        {
            get
            {
                Brick brick = new Brick();
                brick.LevelList.AddRange(
                    new Image[] {
                        Resources.Brick5,
                        Resources.Brick5Broken1,
                        Resources.Brick5Broken2});
                brick.Style = Game.Characters.DrawStyle.Image;
                brick.IsCanToBreak = true;
                brick.Size = SizeOfAll;
                return brick;
            }
        }

        static public Brick CantBreak
        {
            get
            {
                Brick brick = new Brick();
                brick.LevelList.Add(Resources.Brick6);
                brick.Style = Game.Characters.DrawStyle.Image;
                brick.IsCanToBreak = false;
                brick.Size = SizeOfAll;
                return brick;
            }
        }

        static public SizeF SizeOfAll
        {
            get
            {
                return sizeOfAll;
            }

            set
            {
                sizeOfAll = value;
            }
        }
    }
}
