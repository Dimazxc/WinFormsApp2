using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp2.Contexts;
using WinFormsApp2.Entities;
using WinFormsApp2.Helpers;

namespace WinFormsApp2
{
    public partial class Form3 : Form
    {
        private readonly List<CellControl> cells = new List<CellControl>();
        private readonly BlockContext blockContext = BlockContext.Instance;

        public Form3()
        {
            InitializeComponent();

            var cellControls = Controls.OfType<CellControl>().ToList();
            cells.AddRange(cellControls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isBlockValid())
            {
                MessageBox.Show("Block is invalid");

                return;
            }

            var array = GetMatrixFromSelectedCells();
            var resultArray = GetBlockCellsArray(array);

            var block = Block.Create(
                resultArray.GetLength(1),
                resultArray.GetLength(0),
                resultArray);

            blockContext.AddBlock(block);

            MessageBox.Show("Block has successfully added to the game");
            ClearCells();
        }

        private void ClearCells()
        {
            cells.ForEach(cell => cell.Reset());
        }

        private bool isBlockValid()
        {
            return isCountLessMaxCellValue() && IsEverySelectedHasNeighbor();
        }

        private bool isCountLessMaxCellValue() => cells.Count(c => c.IsSelected) <= 8;

        private bool IsEverySelectedHasNeighbor()
        {
            var array = GetMatrixFromSelectedCells();
 
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i,j] == 1)
                    {
                        var result = IsCellHaveAtLeastOneNeighbor(array, i, j);

                        if (!result)
                            return false;
                    }
                }
            }

            return true;
        }

        private bool IsCellHaveAtLeastOneNeighbor(int[,] array, int i, int j)
        {
            var indexNeighors = new Point[]
            {
                new Point(-1, 0),
                new Point(0, -1),
                new Point(0, 1),
                new Point(1, 0)
            };

            var neigbhorCount = 0;
            foreach(var item in indexNeighors)
            {
                var rowIndex = (i + item.X) % array.GetLength(0);
                var columnIndex = (j + item.Y) % array.GetLength(1);

                if (rowIndex >= 0 && columnIndex >= 0 && (rowIndex != i || columnIndex != j))
                {
                    if (array[rowIndex, columnIndex] == 1)
                        neigbhorCount++;
                }
            }

            return neigbhorCount > 0;
        }

        private int[,] GetMatrixFromSelectedCells()
        {
            var array = new int[4, 4];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    var cell = cells.FirstOrDefault(cell => cell.X == i && cell.Y == j);

                    if (cell != null && cell.IsSelected)
                        array[i, j] = 1;
                }
            }

            return array;
        }

        private int[,] GetBlockCellsArray(int[,] array)
        {
            var listNotEmptyRow = new List<int>();
            var listNotEmptyCol = new List<int>();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 1)
                    {
                        listNotEmptyRow.Add(i);
                        break;
                    }

                }
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[j, i] == 1)
                    {
                        listNotEmptyCol.Add(i);
                        break;
                    }

                }
            }

            var resultArr = new int[listNotEmptyRow.Count, listNotEmptyCol.Count];
            var indexRow = 0;
            foreach (var x in listNotEmptyRow)
            {
                var indexCol = 0;
                foreach (var y in listNotEmptyCol)
                {
                    resultArr[indexRow, indexCol] = array[x, y];
                    indexCol++;
                }

                indexRow++;
            }

            return resultArr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormSwitcher.SwitchForm(this, new Form2());
        }
    }
}
