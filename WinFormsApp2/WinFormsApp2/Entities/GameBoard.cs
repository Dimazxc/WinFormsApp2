using System;
using WinFormsApp2.Constants;

namespace WinFormsApp2.Entities
{
    public class GameBoard
    {
        private readonly int[,] board;

        public event Action OnGameOver;
        public event Action<int[,], int> OnRowFilled;

        public GameBoard()
        {
            board = new int[GameBoardOptions.BoardWidth, GameBoardOptions.BoardHeight];
        }

        public void AddBlock(Block block, Position position)
        {
            block.ForEach((value, i, j) =>
            {
                if (value == 1)
                {
                    if (isGameOver(position))
                    {
                        OnGameOver?.Invoke();

                        return;
                    }

                    board[position.X + i, position.Y + j] = 1;
                }
            });
        }

        public bool IsBlockTouchAnother(Block block, Position position)
        {
            var isBlockStucked = false;

            block.ForEach((value, i, j) =>
            {
                if (position.Y + j > 0
                    && value == 1
                    && board[position.X + i, position.Y + j] == 1)
                {
                    isBlockStucked = true;
                }
            });

            return isBlockStucked;
        }

        private bool isGameOver(Position position)
        {
            return position.Y < 0;
        }

        public void CheckFilledRow()
        {
            var isRowFinded = false;
            var rowCount = 0;

            for (int i = 0; i < GameBoardOptions.BoardHeight; i++)
            {
                int j;
                for (j = GameBoardOptions.BoardWidth - 1; j >= 0; j--)
                {
                    if (board[j, i] == 0)
                        break;
                }

                if (j == -1)
                {
                    isRowFinded = true;
                    rowCount++;

                    for (j = 0; j < GameBoardOptions.BoardWidth; j++)
                    {
                        for (int k = i; k > 0; k--)
                        {
                            board[j, k] = board[j, k - 1];
                        }

                        board[j, 0] = 0;
                    }
                }
            }

            if (isRowFinded)
                OnRowFilled?.Invoke(board, rowCount);
        }
    }
}
