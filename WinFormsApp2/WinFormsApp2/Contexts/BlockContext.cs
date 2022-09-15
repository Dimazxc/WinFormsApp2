using System;
using System.Collections.Generic;

using WinFormsApp2.Entities;

namespace WinFormsApp2.Contexts
{
    public sealed class BlockContext
    {
        private readonly List<Block> blocks = new List<Block>();
        private static readonly Lazy<BlockContext> lazy =
            new Lazy<BlockContext>(() => new BlockContext());

        private BlockContext()
        {
            FillContext();
        }

        public static BlockContext Instance => lazy.Value;

        public Block GetRandomBlock()
        {
            return blocks[new Random().Next(blocks.Count)];
        }

        public void AddBlock(Block block)
        {
            blocks.Add(block);
        }

        private void FillContext()
        {
            blocks.AddRange(new List<Block>
            {
                Block.Create(
                    2,
                    2,
                    new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 },
                    }),
               Block.Create(
                    1,
                    4,
                    new int[,]
                    {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 }
                    }),
               Block.Create(
                    3,
                    2,
                    new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                    }),
               Block.Create(
                    3,
                    2,
                    new int[,]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                    }),
               Block.Create(
                    3,
                    2,
                    new int[,]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                    }),
               Block.Create(
                    2,
                    3,
                    new int[,]
                    {
                        { 1, 0 },
                        { 1, 0 },
                        { 1, 1 },
                    })
            });
        }
    }
}
