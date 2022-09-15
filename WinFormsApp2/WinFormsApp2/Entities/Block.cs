using System;

namespace WinFormsApp2.Entities
{
    public class Block
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private int[,] cells;

        private int[,] memoryCells;

        private Block() { }

        public static Block Create(
            int width,
            int height,
            int[,] initCells)
        {
            //if (height != initCells.GetLength(0)
            //    || width != initCells.GetLength(1))
            //{
            //    throw new Exception("Not valid block");
            //}

            var block = new Block
            {
                Width = width,
                Height = height,
                cells = initCells
            };

            return block;
        }

        public void Rotate()
        {
            memoryCells = cells;
            cells = new int[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cells[i, j] = memoryCells[Height - 1 - j, i];
                }
            }

            (Width, Height) = (Height, Width);
        }

        public void ForEach(Action<int, int, int> func)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    func(cells[j, i], i, j);
                }
            }
        }

        public void RollbackRotation()
        {
            cells = memoryCells;
            (Width, Height) = (Height, Width);
        }
    }
}
