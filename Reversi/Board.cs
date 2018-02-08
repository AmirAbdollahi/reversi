using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Reversi
{
    enum SquareStat
    {
        EMPTY,
        SELECTABLE,
        BLACK,
        WHITE
    }

    enum Player
    {
        BLACK,
        WHITE
    }

    enum GameMode
    {
        HUMAN_VS_COMPUTER,
        HUMAN_VS_HUMAN
    }

    class Board
    {
        Random random = new Random();
        public int[,] reversibleSquaresScores = new int[8, 8]; // store the number of disks that will reverse by clicking on selectable square

        private SquareStat[,] squareStats = new SquareStat[8, 8];
        public SquareStat[,] SquareStats
        {
            get
            {
                return squareStats;
            }
        }

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }

            private set
            {
                currentPlayer = value;
            }
        }


        GameMode gameMode;
        public GameMode TheGameMode
        {
            get
            {
                return gameMode;
            }
        }

        public Board(GameMode mode)
        {
            SetAllToEmpty();
            SetToStartPosition();
            //CurrentPlayer = GetRandomStarterPlayer();
            CurrentPlayer = Player.WHITE;
            ResetSelectableSquares();
            gameMode = mode;
        }

        private Player GetRandomStarterPlayer() // used only at start of game
        {
            int randomNumber = random.Next(0, 2);

            if (randomNumber == 0)
            {
                return Player.BLACK;
            }
            else
            {
                return Player.WHITE;
            }
        }

        public void SwitchPlayer()
        {
            if (CurrentPlayer == Player.BLACK)
            {
                CurrentPlayer = Player.WHITE;
            }
            else if (CurrentPlayer == Player.WHITE)
            {
                CurrentPlayer = Player.BLACK;
            }
        }

        public void SetAllToEmpty()
        {
            for (int y = 0; y < SquareStats.GetLength(0); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(1); x++)
                {
                    SquareStats[x, y] = SquareStat.EMPTY;
                }
            }
        }

        public void SetToStartPosition()
        {
            SquareStats[3, 3] = SquareStat.WHITE;
            SquareStats[4, 4] = SquareStat.WHITE;
            SquareStats[3, 4] = SquareStat.BLACK;
            SquareStats[4, 3] = SquareStat.BLACK;
        }

        public void ResetSelectableSquares()
        {
            // clear all selectable squares and set them to empty:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.SELECTABLE)
                    {
                        SquareStats[x, y] = SquareStat.EMPTY;
                    }
                }
            }

            // Verify Horizontal Rows:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.EMPTY)
                    {
                        if (CurrentPlayer == Player.BLACK)
                        {
                            // verify squares at right side:
                            if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.WHITE)
                            {
                                for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i + 1, y] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i + 1] != SquareStat.BLACK && SquareStats[x, i + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // verify squares at left side:
                            if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.WHITE)
                            {
                                for (int i = x - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i - 1, y] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i - 1] != SquareStat.BLACK && SquareStats[x, i - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (CurrentPlayer == Player.WHITE)
                        {
                            // verify squares at right side:
                            if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.BLACK)
                            {
                                for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i + 1, y] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i + 1] != SquareStat.BLACK && SquareStats[x, i + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // verify squares at left side:
                            if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.BLACK)
                            {
                                for (int i = x - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i - 1, y] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i - 1] != SquareStat.BLACK && SquareStats[x, i - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Verify Vertical Rows:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.EMPTY)
                    {
                        if (CurrentPlayer == Player.BLACK)
                        {
                            // verify squares at bottom side:
                            if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[x, i + 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i + 1] != SquareStat.BLACK && SquareStats[x, i + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // verify squares at up side:
                            if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = y - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[x, i - 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i - 1] != SquareStat.BLACK && SquareStats[x, i - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (CurrentPlayer == Player.WHITE)
                        {
                            // verify squares at bottom side:
                            if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[x, i + 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }                                        
                                        else if (SquareStats[x, i + 1] != SquareStat.BLACK && SquareStats[x, i + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // verify squares at up side:
                            if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = y - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[x, i - 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[x, i - 1] != SquareStat.BLACK && SquareStats[x, i - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Verify NWSE Diagonal:
            for (int y = 0; y < SquareStats.GetLength(0); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(1); x++)
                {
                    if (SquareStats[x, y] == SquareStat.EMPTY)
                    {
                        if (CurrentPlayer == Player.BLACK)
                        {
                            // verify squares at bottom right side:
                            if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i + 1, j + 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i + 1, j + 1] != SquareStat.BLACK && SquareStats[i + 1, j + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            // verify squares at top left side:
                            if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                                {
                                    if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i - 1, j - 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i - 1, j - 1] != SquareStat.BLACK && SquareStats[i - 1, j - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (CurrentPlayer == Player.WHITE)
                        {
                            // verify squares at bottom right side:
                            if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i + 1, j + 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i + 1, j + 1] != SquareStat.BLACK && SquareStats[i + 1, j + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            // verify squares at top left side:
                            if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                                {
                                    if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i - 1, j - 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i - 1, j - 1] != SquareStat.BLACK && SquareStats[i - 1, j - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Verify NESW Diagonal:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.EMPTY)
                    {
                        if (CurrentPlayer == Player.BLACK)
                        {
                            // verify squares at top right side:
                            if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i + 1, j - 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i + 1, j - 1] != SquareStat.BLACK && SquareStats[i + 1, j - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            // verify squares at bottom left side:
                            if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                                {
                                    if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE)
                                    {
                                        if (SquareStats[i - 1, j + 1] == SquareStat.BLACK)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i - 1, j + 1] != SquareStat.BLACK && SquareStats[i - 1, j + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (CurrentPlayer == Player.WHITE)
                        {
                            // verify squares at top right side:
                            if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i + 1, j - 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i + 1, j - 1] != SquareStat.BLACK && SquareStats[i + 1, j - 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            // verify squares at bottom left side:
                            if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                                {
                                    if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK)
                                    {
                                        if (SquareStats[i - 1, j + 1] == SquareStat.WHITE)
                                        {
                                            SquareStats[x, y] = SquareStat.SELECTABLE;
                                            break;
                                        }
                                        else if (SquareStats[i - 1, j + 1] != SquareStat.BLACK && SquareStats[i - 1, j + 1] != SquareStat.WHITE)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetScores(out int blackScore, out int whiteScore)
        {
            blackScore = 0;
            whiteScore = 0;

            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.BLACK)
                    {
                        blackScore++;
                    }
                    else if (SquareStats[x, y] == SquareStat.WHITE)
                    {
                        whiteScore++;
                    }
                }
            }
        }

        public void ReverseLine(int x, int y)
        {
            if (SquareStats[x, y] == SquareStat.SELECTABLE)
            {
                if (CurrentPlayer == Player.BLACK)
                {
                    // verify right side line:
                    if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.WHITE)
                    {
                        for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.WHITE && SquareStats[i + 1, y] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, y] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, y);
                                    i--;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify left side line:
                    if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.WHITE)
                    {
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.WHITE && SquareStats[i - 1, y] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, y] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, y);
                                    i++;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify up side line:
                    if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.WHITE)
                    {
                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.WHITE && SquareStats[x, i - 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[x, i] == SquareStat.WHITE)
                                {
                                    ChangeColor(x, i);
                                    i++;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify down side line:
                    if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.WHITE)
                    {
                        for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                        {
                            if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.WHITE && SquareStats[x, i + 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[x, i] == SquareStat.WHITE)
                                {
                                    ChangeColor(x, i);
                                    i--;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify SE side line:
                    if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.WHITE)
                    {
                        for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i + 1, j + 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, j] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, j);
                                    i--;
                                    j--;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify NW side line:
                    if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.WHITE)
                    {
                        for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                        {
                            if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i - 1, j - 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, j] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, j);
                                    i++;
                                    j++;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify NE side line:
                    if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.WHITE)
                    {
                        for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i + 1, j - 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, j] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, j);
                                    i--;
                                    j++;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }

                    // verify SW side line:
                    if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.WHITE)
                    {
                        for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                        {
                            if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i - 1, j + 1] == SquareStat.BLACK)
                            {
                                while (SquareStats[i, j] == SquareStat.WHITE)
                                {
                                    ChangeColor(i, j);
                                    i++;
                                    j--;
                                }

                                SquareStats[x, y] = SquareStat.BLACK;
                                break;
                            }
                        }
                    }
                }
                else if (CurrentPlayer == Player.WHITE)
                {
                    // verify right side line:
                    if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.BLACK)
                    {
                        for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.BLACK && SquareStats[i + 1, y] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, y] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, y);
                                    i--;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify left side line:
                    if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.BLACK)
                    {
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.BLACK && SquareStats[i - 1, y] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, y] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, y);
                                    i++;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify up side line:
                    if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.BLACK)
                    {
                        for (int i = y - 1; i >= 0; i--)
                        {
                            if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.BLACK && SquareStats[x, i - 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[x, i] == SquareStat.BLACK)
                                {
                                    ChangeColor(x, i);
                                    i++;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify down side line:
                    if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.BLACK)
                    {
                        for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                        {
                            if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.BLACK && SquareStats[x, i + 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[x, i] == SquareStat.BLACK)
                                {
                                    ChangeColor(x, i);
                                    i--;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify SE side line:
                    if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.BLACK)
                    {
                        for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i + 1, j + 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, j] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, j);
                                    i--;
                                    j--;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify NW side line:
                    if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.BLACK)
                    {
                        for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                        {
                            if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i - 1, j - 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, j] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, j);
                                    i++;
                                    j++;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify NE side line:
                    if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.BLACK)
                    {
                        for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                        {
                            if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i + 1, j - 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, j] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, j);
                                    i--;
                                    j++;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }

                    // verify SW side line:
                    if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.BLACK)
                    {
                        for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                        {
                            if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i - 1, j + 1] == SquareStat.WHITE)
                            {
                                while (SquareStats[i, j] == SquareStat.BLACK)
                                {
                                    ChangeColor(i, j);
                                    i++;
                                    j--;
                                }

                                SquareStats[x, y] = SquareStat.WHITE;
                                break;
                            }
                        }
                    }
                }

                SwitchPlayer();
            }
        }

        private void ChangeColor(int x, int y)
        {
            if (SquareStats[x, y] == SquareStat.BLACK)
            {
                SquareStats[x, y] = SquareStat.WHITE;
            }
            else if (SquareStats[x, y] == SquareStat.WHITE)
            {
                SquareStats[x, y] = SquareStat.BLACK;
            }
        }

        public void PlayComputer()
        {
            SetReversibleSquaresScores();
            Point pntSquareWithMostScore = GetSquareWithMostScore();
            ReverseLine(pntSquareWithMostScore.X, pntSquareWithMostScore.Y);
        }

        public void SetReversibleSquaresScores()
        {
            // set all square to 0:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    reversibleSquaresScores[x, y] = 0;
                }
            }

            // set selectable squares scores:
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.SELECTABLE)
                    {
                        if (CurrentPlayer == Player.BLACK)
                        {
                            // verify right side line:
                            if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.WHITE)
                            {
                                for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.WHITE && SquareStats[i + 1, y] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, y] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify left side line:
                            if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.WHITE)
                            {
                                for (int i = x - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.WHITE && SquareStats[i - 1, y] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, y] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify up side line:
                            if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = y - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.WHITE && SquareStats[x, i - 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[x, i] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify down side line:
                            if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.WHITE && SquareStats[x, i + 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[x, i] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify SE side line:
                            if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i + 1, j + 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, j] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                            j--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify NW side line:
                            if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                                {
                                    if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i - 1, j - 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, j] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                            j++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify NE side line:
                            if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.WHITE)
                            {
                                for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i + 1, j - 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, j] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                            j++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify SW side line:
                            if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.WHITE)
                            {
                                for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                                {
                                    if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.WHITE && SquareStats[i - 1, j + 1] == SquareStat.BLACK)
                                    {
                                        while (SquareStats[i, j] == SquareStat.WHITE)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                            j--;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        else if (CurrentPlayer == Player.WHITE)
                        {
                            // verify right side line:
                            if (x + 1 < SquareStats.GetLength(0) && SquareStats[x + 1, y] == SquareStat.BLACK)
                            {
                                for (int i = x + 1; i < SquareStats.GetLength(0); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && SquareStats[i, y] == SquareStat.BLACK && SquareStats[i + 1, y] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, y] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify left side line:
                            if (x - 1 >= 0 && SquareStats[x - 1, y] == SquareStat.BLACK)
                            {
                                for (int i = x - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[i, y] == SquareStat.BLACK && SquareStats[i - 1, y] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, y] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify up side line:
                            if (y - 1 >= 0 && SquareStats[x, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = y - 1; i >= 0; i--)
                                {
                                    if (i - 1 >= 0 && SquareStats[x, i] == SquareStat.BLACK && SquareStats[x, i - 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[x, i] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify down side line:
                            if (y + 1 < SquareStats.GetLength(1) && SquareStats[x, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = y + 1; i < SquareStats.GetLength(1); i++)
                                {
                                    if (i + 1 < SquareStats.GetLength(1) && SquareStats[x, i] == SquareStat.BLACK && SquareStats[x, i + 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[x, i] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify SE side line:
                            if (x + 1 < SquareStats.GetLength(0) && y + 1 < SquareStats.GetLength(1) && SquareStats[x + 1, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = x + 1, j = y + 1; i < SquareStats.GetLength(0) && j < SquareStats.GetLength(1); i++, j++)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i + 1, j + 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, j] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                            j--;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify NW side line:
                            if (x - 1 >= 0 && y - 1 >= 0 && SquareStats[x - 1, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                                {
                                    if (i - 1 >= 0 && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i - 1, j - 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, j] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                            j++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify NE side line:
                            if (x + 1 < SquareStats.GetLength(0) && y - 1 >= 0 && SquareStats[x + 1, y - 1] == SquareStat.BLACK)
                            {
                                for (int i = x + 1, j = y - 1; i < SquareStats.GetLength(0) && j >= 0; i++, j--)
                                {
                                    if (i + 1 < SquareStats.GetLength(0) && j - 1 >= 0 && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i + 1, j - 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, j] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i--;
                                            j++;
                                        }
                                        break;
                                    }
                                }
                            }

                            // verify SW side line:
                            if (x - 1 >= 0 && y + 1 < SquareStats.GetLength(1) && SquareStats[x - 1, y + 1] == SquareStat.BLACK)
                            {
                                for (int i = x - 1, j = y + 1; i >= 0 && j < SquareStats.GetLength(1); i--, j++)
                                {
                                    if (i - 1 >= 0 && j + 1 < SquareStats.GetLength(1) && SquareStats[i, j] == SquareStat.BLACK && SquareStats[i - 1, j + 1] == SquareStat.WHITE)
                                    {
                                        while (SquareStats[i, j] == SquareStat.BLACK)
                                        {
                                            reversibleSquaresScores[x, y]++;
                                            i++;
                                            j--;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private Point GetSquareWithMostScore()
        {
            Point pntSquare = Point.Empty;

            if (SquareStats[0, 0] == SquareStat.SELECTABLE)
            {
                return new Point(0, 0);
            }
            else if (SquareStats[SquareStats.GetLength(0) - 1, 0] == SquareStat.SELECTABLE)
            {
                return new Point(SquareStats.GetLength(0) - 1, 0);
            }
            else if (SquareStats[0, SquareStats.GetLength(1) - 1] == SquareStat.SELECTABLE)
            {
                return new Point(0, SquareStats.GetLength(1) - 1);
            }
            else if (SquareStats[SquareStats.GetLength(0) - 1, SquareStats.GetLength(1) - 1] == SquareStat.SELECTABLE)
            {
                return new Point(SquareStats.GetLength(0) - 1, SquareStats.GetLength(1) - 1);
            }
            else
            {
                for (int y = 0; y < reversibleSquaresScores.GetLength(1); y++)
                {
                    for (int x = 0; x < reversibleSquaresScores.GetLength(0); x++)
                    {
                        if (reversibleSquaresScores[x, y] > reversibleSquaresScores[pntSquare.X, pntSquare.Y])
                        {
                            pntSquare = new Point(x, y);
                        }
                    }
                }

                return pntSquare;
            }
        }

        private Point GetSquareWithLeastScore()
        {
            Point pntSquare = Point.Empty;

            // set pntSquare to first reversible square:
            for (int y = 0; y < reversibleSquaresScores.GetLength(1); y++)
            {
                for (int x = 0; x < reversibleSquaresScores.GetLength(0); x++)
                {
                    if (reversibleSquaresScores[x, y] > 0)
                    {
                        pntSquare = new Point(x, y);
                        break;
                    }
                }
            }

            // find the least reversible square score:
            for (int y = 0; y < reversibleSquaresScores.GetLength(1); y++)
            {
                for (int x = 0; x < reversibleSquaresScores.GetLength(0); x++)
                {
                    if (reversibleSquaresScores[x, y] > 0 && reversibleSquaresScores[x, y] < reversibleSquaresScores[pntSquare.X, pntSquare.Y])
                    {
                        pntSquare = new Point(x, y);
                    }
                }
            }

            return pntSquare;
        }

        public bool IsThereAnySelectableSquare()
        {
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.SELECTABLE)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsAllSquaresFilled()
        {
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.EMPTY || SquareStats[x, y] == SquareStat.SELECTABLE)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsThereAnyBlackDiskOnBoard()
        {
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.BLACK)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsThereAnyWhiteDiskOnBoard()
        {
            for (int y = 0; y < SquareStats.GetLength(1); y++)
            {
                for (int x = 0; x < SquareStats.GetLength(0); x++)
                {
                    if (SquareStats[x, y] == SquareStat.WHITE)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
