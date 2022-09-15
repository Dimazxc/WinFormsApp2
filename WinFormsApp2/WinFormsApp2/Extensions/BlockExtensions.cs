using WinFormsApp2.Constants;
using WinFormsApp2.Entities;

namespace WinFormsApp2.Extensions
{
    public static class BlockExtensions
    {
        public static bool IsTouchSideBorder(
            this Block block,
            Position position)
        {
            return position.X < 0
                || position.X + block.Width > GameBoardOptions.BoardWidth;
        }

        public static bool IsTouchBottomBorder(
            this Block block,
            Position position)
        {
            return position.Y + block.Height > GameBoardOptions.BoardHeight;
        }
    }
}
