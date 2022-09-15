using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using WinFormsApp2.Constants;
using WinFormsApp2.Entities;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private TetrisGame game;

        private Bitmap backgroudBitmap;
        private Bitmap currentBitmap;

        private Brush currentBrush;
        private Pen pen;

        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            LoadGraphics();

            game = new TetrisGame();
            game.OnBlockStateChanged += RedrawBlock;
            game.OnGameBoardRefresh += RefreshBackgroundBitmap;

            game.SubscribeOnGameOverEvent(ShowGameOverScreen);
            game.SubscribeOnRowFilledEvent(RedrawBoardAfterClearRows);

            timer1.Start();
        }

        private void LoadGraphics()
        {
            pictureBox1.Size = new Size(
                GameBoardOptions.BoardWidth * GameBoardOptions.CellSize,
                GameBoardOptions.BoardHeight * GameBoardOptions.CellSize);

            backgroudBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            currentBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            currentBrush = new SolidBrush(Color.Red);

            pen = new Pen(Color.White);
            pen.Alignment = PenAlignment.Inset;
        }

        private void RefreshBackgroundBitmap()
        {
            backgroudBitmap = new Bitmap(currentBitmap);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.MoveBlock(0, 1);
        }

        private void RedrawBlock(Block block, Position position)
        {
            currentBitmap = new Bitmap(backgroudBitmap);

            using (var graphic = Graphics.FromImage(currentBitmap))
            {
                block.ForEach((value, i, j) =>
                {
                    if (value == 1)
                    {
                        var rectangle = new Rectangle(
                            (position.X + i) * GameBoardOptions.CellSize,
                            (position.Y + j) * GameBoardOptions.CellSize,
                            GameBoardOptions.CellSize,
                            GameBoardOptions.CellSize);

                        graphic.FillRectangle(currentBrush, rectangle);
                        graphic.DrawRectangle(pen, rectangle);
                    }
                });
            }

            pictureBox1.Image = currentBitmap;
        }

        private void RedrawBoardAfterClearRows(int[,] board, int rowCount)
        {
            score += rowCount;
            label2.Text = score.ToString();

            currentBitmap = new Bitmap(backgroudBitmap);
            using (var graphic = Graphics.FromImage(currentBitmap))
            {
                for(int i = 0; i < GameBoardOptions.BoardWidth; i++)
                {
                    for(int j = 0; j < GameBoardOptions.BoardHeight; j++)
                    {
                        var rectangle = new Rectangle(
                            i * GameBoardOptions.CellSize,
                            j * GameBoardOptions.CellSize,
                            GameBoardOptions.CellSize,
                            GameBoardOptions.CellSize);

                        var brush = board[i, j] == 1
                            ? currentBrush
                            : Brushes.Black;

                        graphic.FillRectangle(brush, rectangle);

                        if (brush != Brushes.Black)
                            graphic.DrawRectangle(pen, rectangle);
                    }
                }
            }

            pictureBox1.Image = currentBitmap;
        }

        private void ShowGameOverScreen()
        {
            Application.Exit();
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                {
                    game.MoveBlock(-1, 0);
                    break;
                }
                case Keys.Right:
                {
                    game.MoveBlock(1, 0);
                    break;
                }
                case Keys.Up:
                {
                    game.RotateBlock();
                    break;
                }
                case Keys.Down:
                {
                    game.MoveBlock(0, 1);
                    break;
                }
            }
        }
    }
}
