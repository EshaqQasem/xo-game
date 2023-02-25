using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TicTacGame
{
    public enum PlayerType{
        X=1,
        O=2,
        None=0
    }

    public enum GameOrder{
        _3=3,
        _6=6,
        _11=11
    }

    public enum whoWin
    {
        X=1,
        O=2,
        None=0,
        equal=3
    }

    class TicTacGame
    {

        public TicTacGame( GameOrder go)
        {
            this.gameOrder = go;
            playersPositions = new PlayerType[(int)gameOrder, (int)gameOrder];//{{PlayerType.None}};
            for (int i=0;i<(int)gameOrder*(int)gameOrder;i++)
            {
               playersPositions[i/(int)gameOrder , i%(int)gameOrder] = PlayerType.None;
            }


            this.emptyCount = (int)gameOrder * (int)gameOrder;

        }
        public PlayerType[,] playersPositions;

        protected int emptyCount;
        public whoWin playOn(int row, int col,PlayerType player)
        {
            playersPositions[row, col] = player;

            if (isPalyerWin(player))
            {
                return player==PlayerType.O? whoWin.O: whoWin.X;
            }
            if (--this.emptyCount == 0)
                return whoWin.equal;
            return whoWin.None;
        }

      /*  public whoWin OplayOn(int row, int col)
        {
            playersPositions[row, col] = PlayerType.O;
            if(

            if (--this.emptyCount == 0)
                return whoWin.equal;

            return whoWin.None;
        }
        */
        public List<Point> rowsAndColsWin;
        protected bool isPalyerWin(PlayerType player)
        {
           rowsAndColsWin = new List<Point>();
            
            bool   winR1 = true,winR2=true;
            bool winVertical,winH;
            int i;
            for ( i = 0; i < (int)gameOrder; i++)
            {
                winVertical = true;
                winH = true;
                int j;
                for (j = 0; j < (int)gameOrder; j++)
                {
                     if (playersPositions[i, j] != player)
                        winH = false;
                     if(playersPositions[j,i] != player)
                       winVertical = false;



                     if (winH == false && winVertical == false) /*playersPositions[i, j] != player && playersPositions[j, i] != player)*/
                     {
                         break;
                     }
                   
                   
                }

                if (playersPositions[i, i] != player)
                {
                    winR1 = false;
                }
                if (playersPositions[ i, (int)gameOrder-1 - i] != player)
                {
                    winR2 = false;
                }

               
                    if (winH)
                    {
                        rowsAndColsWin.Add(new Point(-1, i));
                    }
                    if (winVertical)
                    {
                        rowsAndColsWin.Add(new Point(i, -1));
                    }
                   // return true;
                
            }
            if (winR1)
            {
                rowsAndColsWin.Add(new Point(i, -1));
            }
            if (winR2)
            {
                rowsAndColsWin.Add(new Point(-1, i));
            }

    if (rowsAndColsWin.Count > 0)
        return true;

    return false;
        }
        private GameOrder gameorder;
        public GameOrder gameOrder
        {
            set
            {
                gameorder = value;
            }
            get
            {
                return gameorder;
            }
        }

        
        

    }
}
