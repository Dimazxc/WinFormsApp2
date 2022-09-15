using WinFormsApp2.Constants;

namespace WinFormsApp2.Entities
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public static Position GetPositionByBlockSize(Block block)
        {
            return new Position
            {
                X = (GameBoardOptions.BoardWidth / 2) - (block.Width / 2),
                Y = -block.Height
            };
        }
    }
}
