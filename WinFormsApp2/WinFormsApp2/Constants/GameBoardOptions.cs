using System;

namespace WinFormsApp2.Constants
{
    public static class GameBoardOptions
    {
        public static int BoardWidth => width;
        public static int BoardHeight => height;
        public const int CellSize = 20;

        public const int BoardWidthMin = 15;
        public const int BoardHeightMin = 20;

        private static int height = BoardWidthMin;
        private static int width = BoardHeightMin;

        public static void SetOptions(int newHeight, int newWidth)
        {
            height = newHeight;
            width = newWidth;
        }
    }
}
