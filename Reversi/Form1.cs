using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {
        Bitmap bmpMain;
        Graphics grpMain;
        Board board;

        Rectangle[,] recSquares = new Rectangle[8, 8];
        Size sizSquare = new Size(75, 75);
        Pen penSquare = new Pen(Color.LightGreen, 5);

        SolidBrush solBlack = new SolidBrush(Color.Black);
        SolidBrush solWhite = new SolidBrush(Color.White);
        SolidBrush solCurrentPlayer;

        int scoreBlack;
        int scoreWhite;
        bool isBoardDrawn = false;

        // start menu:
        LinkLabel linHumanVsComputer;
        LinkLabel linHumanVsHuman;
        LinkLabel linExit;
        Font fntStartMenu = new Font("Arial", 15);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            bmpMain = new Bitmap(picMain.Width, picMain.Height);
            grpMain = Graphics.FromImage(bmpMain);

            ShowStartMenu();
        }

        private void ShowStartMenu()
        {
            linHumanVsComputer = new LinkLabel();
            linHumanVsComputer.Text = "Human vs. Computer";
            linHumanVsComputer.Font = fntStartMenu;
            linHumanVsComputer.Location = new Point(200, 250);
            linHumanVsComputer.Size = new Size(200, 30);
            this.linHumanVsComputer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linHumanVsComputer_LinkClicked);
            picMain.Controls.Add(linHumanVsComputer);

            linHumanVsHuman = new LinkLabel();
            linHumanVsHuman.Text = "Human vs. Human";
            linHumanVsHuman.Font = fntStartMenu;
            linHumanVsHuman.Location = new Point(linHumanVsComputer.Location.X, linHumanVsComputer.Location.Y + 40);
            linHumanVsHuman.Size = new Size(200, 30);
            this.linHumanVsHuman.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linHumanVsHuman_LinkClicked);
            picMain.Controls.Add(linHumanVsHuman);

            linExit = new LinkLabel();
            linExit.Text = "Exit";
            linExit.Font = fntStartMenu;
            linExit.Location = new Point(linHumanVsComputer.Location.X, linHumanVsHuman.Location.Y + 40);
            linExit.Size = new Size(200, 30);
            this.linExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linExit_LinkClicked);
            picMain.Controls.Add(linExit);
        }

        private void ShowBoard(GameMode mode)
        {
            board = new Board(mode);
            DrawSquares();
            board.SetToStartPosition();
            board.SetReversibleSquaresScores();
            ShowDisksOnBoard();
            SetPlayersLabelEnable();
            ShowScores();
            isBoardDrawn = true;

            picMain.Refresh();
        }

        private void DrawSquares()
        {
            for (int y = 0; y < recSquares.GetLength(0); y++)
            {
                for (int x = 0; x < recSquares.GetLength(1); x++)
                {
                    recSquares[x, y] = new Rectangle(new Point(x * 75, y * 75), sizSquare);
                    grpMain.DrawRectangle(penSquare, recSquares[x, y]);
                }
            }
        }

        private void ShowDisksOnBoard()
        {
            for (int y = 0; y < board.SquareStats.GetLength(0); y++)
            {
                for (int x = 0; x < board.SquareStats.GetLength(1); x++)
                {
                    if (board.SquareStats[x, y] == SquareStat.BLACK)
                    {
                        grpMain.FillEllipse(solBlack, recSquares[x, y]);
                    }
                    else if (board.SquareStats[x, y] == SquareStat.WHITE)
                    {
                        grpMain.FillEllipse(solWhite, recSquares[x, y]);
                    }
                    else if (board.SquareStats[x, y] == SquareStat.SELECTABLE)
                    {
                        if (board.CurrentPlayer == Player.BLACK)
                        {
                            solCurrentPlayer = solBlack;
                        }
                        else if (board.CurrentPlayer == Player.WHITE)
                        {
                            solCurrentPlayer = solWhite;
                        }

                        Rectangle recSelectableSquare = new Rectangle(recSquares[x, y].X + recSquares[x, y].Width * 3 / 8, recSquares[x, y].Y + recSquares[x, y].Height * 3 / 8, recSquares[x, y].Width / 4, recSquares[x, y].Height / 4);
                        grpMain.FillEllipse(solCurrentPlayer, recSelectableSquare);
                        grpMain.DrawString(board.reversibleSquaresScores[x, y].ToString(), fntStartMenu, solCurrentPlayer, recSquares[x, y]);
                    }
                }
            }
        }

        private void SetPlayersLabelEnable()
        {
            if (board.CurrentPlayer == Player.BLACK)
            {
                lblPlayerBlack.Enabled = true;
                lblPlayerWhite.Enabled = false;
            }
            else if (board.CurrentPlayer == Player.WHITE)
            {
                lblPlayerBlack.Enabled = false;
                lblPlayerWhite.Enabled = true;
            }
        }

        private void ShowScores()
        {
            board.GetScores(out scoreBlack, out scoreWhite);

            lblScoreBlack.Text = scoreBlack.ToString();
            lblScoreWhite.Text = scoreWhite.ToString();
        }

        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpMain, 0, 0);
        }

        private Point GetSquareCoordinates(Point mousePoint)
        {
            for (int y = 0; y < recSquares.GetLength(0); y++)
            {
                for (int x = 0; x < recSquares.GetLength(1); x++)
                {
                    if (recSquares[x, y].Contains(mousePoint))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return new Point(-1, -1);
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            Point pntSquareCoordinates = GetSquareCoordinates(e.Location);

            if (isBoardDrawn)
            {
                if (board.SquareStats[pntSquareCoordinates.X, pntSquareCoordinates.Y] == SquareStat.SELECTABLE)
                {
                    // play human:
                    board.ReverseLine(pntSquareCoordinates.X, pntSquareCoordinates.Y);
                    board.ResetSelectableSquares();
                    board.SetReversibleSquaresScores();
                    grpMain.Clear(picMain.BackColor);
                    DrawSquares();
                    ShowDisksOnBoard();
                    SetPlayersLabelEnable();
                    ShowScores();
                    picMain.Refresh();
                    if (!board.IsAllSquaresFilled() && !board.IsThereAnySelectableSquare())
                    {
                        if (!board.IsThereAnyBlackDiskOnBoard())
                        {
                            MessageBox.Show("Player White Wins!");
                        }
                        else if (!board.IsThereAnyWhiteDiskOnBoard())
                        {
                            MessageBox.Show("Player Black Wins!");
                        }
                        else
                        {
                            MessageBox.Show("There is no available room!");

                            board.SwitchPlayer();
                            board.ResetSelectableSquares();
                            board.SetReversibleSquaresScores();
                            grpMain.Clear(picMain.BackColor);
                            DrawSquares();
                            ShowDisksOnBoard();
                            SetPlayersLabelEnable();
                            ShowScores();
                            picMain.Refresh();
                        }
                    }
                    else if (board.IsAllSquaresFilled())
                    {
                        ShowWinnerPlayer();
                    }

                    if (board.TheGameMode == GameMode.HUMAN_VS_COMPUTER)
                    {
                        PlayPC();
                    }
                }
            }
        }
        private void PlayPC()
        {
            // play computer:
            picMain.Cursor = Cursors.WaitCursor;
            board.PlayComputer();
            System.Threading.Thread.Sleep(1000);
            board.ResetSelectableSquares();
            board.SetReversibleSquaresScores();
            grpMain.Clear(picMain.BackColor);
            DrawSquares();
            ShowDisksOnBoard();
            SetPlayersLabelEnable();
            ShowScores();
            picMain.Refresh();
            picMain.Cursor = Cursors.Default;

            if (!board.IsAllSquaresFilled() && !board.IsThereAnySelectableSquare())
            {
                if (!board.IsThereAnyBlackDiskOnBoard())
                {
                    MessageBox.Show("Player White Wins!");
                }
                else if (!board.IsThereAnyWhiteDiskOnBoard())
                {
                    MessageBox.Show("Player Black Wins!");
                }
                else
                {
                    MessageBox.Show("There is no available room!");
                    board.SwitchPlayer();
                }
            }
            else if (board.IsAllSquaresFilled())
            {
                ShowWinnerPlayer();
            }
        }

        private void ShowWinnerPlayer()
        {
            if (scoreBlack > scoreWhite)
            {
                if (MessageBox.Show("Player Black Wins!") == DialogResult.OK)
                {
                    grpMain.Clear(picMain.BackColor);
                    ShowStartMenu();
                }
            }
            else if (scoreWhite > scoreBlack)
            {
                if (MessageBox.Show("Player White Wins!") == DialogResult.OK)
                {
                    grpMain.Clear(picMain.BackColor);
                    ShowStartMenu();
                }
            }
            else if (scoreBlack == scoreWhite) // tie
            {
                if (MessageBox.Show("It's a Tie!") == DialogResult.OK)
                {
                    grpMain.Clear(picMain.BackColor);
                    ShowStartMenu();
                }
            }
        }
        private void linHumanVsComputer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            picMain.Controls.Remove(linHumanVsComputer);
            picMain.Controls.Remove(linHumanVsHuman);
            picMain.Controls.Remove(linExit);

            ShowBoard(GameMode.HUMAN_VS_COMPUTER);
        }

        private void linHumanVsHuman_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            picMain.Controls.Remove(linHumanVsComputer);
            picMain.Controls.Remove(linHumanVsHuman);
            picMain.Controls.Remove(linExit);

            ShowBoard(GameMode.HUMAN_VS_HUMAN);
        }

        private void linExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}
