﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WinFormsApp2
{
    class Game
    {
        Diagram now;
        Board gboard = Board.GameBoard;
        internal Point NowPosition
        {
            get
            {
                if(now == null)
                {
                    return new Point(0, 0);
                }
                return new Point(now.X, now.Y);
            }
        }

        internal int BlockNum
        {
            get
            {
                return now.BlockNum;
            }
        }
        internal int Turn
        {
            get
            {
                return now.Turn;
            }
        }
        
        internal static Game Singleton
        {
            get;
            private set;
        }
        internal int this[int x, int y]
        {
            get 
            {
                return gboard[x, y];
            }
        }
        static Game()
        {
            Singleton = new Game();
        }
        Game()
        {
            now = new Diagram();
        }
        internal bool MoveLeft()
        {
            for(int xx=0; xx<4; xx++)
            {
                for(int yy=0; yy<4; yy++)
                {
                   if(BlockValue.bvals[now.BlockNum,Turn,xx,yy]!=0)
                    {
                        if(now.X + xx<=0)
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum,Turn, now.X-1, now.Y))
            {

                now.MoveLeft();
                return true;
            }
                return false;
        }

        internal bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if (now.X + xx + 1 >= GameRule.BX)
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, Turn, now.X + 1, now.Y))
            {

                now.MoveRight();
                return true;
            }
            return false;
        }

        internal bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if ((now.Y + yy >= GameRule.BY))
                        {
                            if (gboard.MoveEnable(now.BlockNum, Turn, now.X, now.Y + 1))
                                return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, Turn, now.X, now.Y + 1))
            {

                now.MoveDown();
                return true;
            }
            gboard.Store(now.BlockNum, Turn, now.X, now.Y);
            return false;

        }
        internal bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[now.BlockNum, (Turn + 1) % 4, xx, yy] != 0)
                    {
                        if (((now.X + xx) < 0) || ((now.X + xx) >= GameRule.BX) || ((now.Y + yy) >= GameRule.BY))
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, (Turn+1)%4, now.X, now.Y))
            {

                now.MoveTurn();
                return true;
            }
            return false;
           
        }

        internal void Next()
        {
            now.Reset();
        }
    }
}
