using System;

using WinFormsApp2.Contexts;
using WinFormsApp2.Entities;
using WinFormsApp2.Extensions;

namespace WinFormsApp2
{
    public class TetrisGame
    {
        private readonly GameBoard board;
        private readonly BlockContext blockContext = BlockContext.Instance;

        private Block currentBlock;
        private Position position;

        public event Action<Block, Position> OnBlockStateChanged;
        public event Action OnGameBoardRefresh;

        public TetrisGame()
        {
            RefreshState();

            board = new GameBoard();
        }

        public void SubscribeOnGameOverEvent(Action action)
        {
            board.OnGameOver += action;
        }

        public void SubscribeOnRowFilledEvent(Action<int[,], int> action)
        {
            board.OnRowFilled += action;
        }

        public void MoveBlock(int sideDelta, int downDelta)
        {
            var movedPosition = new Position
            {
                X = position.X + sideDelta,
                Y = position.Y + downDelta,
            };

            if (currentBlock.IsTouchSideBorder(movedPosition))
            {
                return;
            }

            if (currentBlock.IsTouchBottomBorder(movedPosition)
                || board.IsBlockTouchAnother(currentBlock, movedPosition))
            {
                board.AddBlock(currentBlock, position);
                board.CheckFilledRow();

                RefreshGame();
                OnGameBoardRefresh?.Invoke();

                return;
            }

            position = movedPosition;
            OnBlockStateChanged?.Invoke(currentBlock, position);
        }

        public void RotateBlock()
        {
            currentBlock.Rotate();

            var isTouchBorder = currentBlock.IsTouchSideBorder(position)
                || board.IsBlockTouchAnother(currentBlock, position);
            if (isTouchBorder)
            {
                currentBlock.RollbackRotation();
            }

            OnBlockStateChanged?.Invoke(currentBlock, position);
        }

        private void RefreshGame()
        {
            RefreshState();
            OnGameBoardRefresh?.Invoke();
        }

        private void RefreshState()
        {
            currentBlock = blockContext.GetRandomBlock();
            position = Position.GetPositionByBlockSize(currentBlock);
        }
    }
}
