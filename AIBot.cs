using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TronLC.Framework
{
    public enum PointState
    {
        Clear,
        You,
        YourWall,
        Opponent,
        OpponentWall
    }

    public class AIBot
    {
        public string GameStatePath { get; private set; }
        public PointState[,] BoardState { get; private set; }
        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }
        public int OpponentX { get; private set; }
        public int OpponentY { get; private set; }

        public AIBot(string gameStatePath)
        {
            this.GameStatePath = gameStatePath;
            this.BoardState = new PointState[30, 30];
        }

        public void Locate(PointState what, out int px, out int py)
        {
            px = py = -1;

            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 30; y++)
                {
                    if (BoardState[x, y] == what)
                    {
                        px = x;
                        py = y;
                    }
                }
            }
        }

        public void ReadGameState()
        {
            int x, y;
            PointState state;

            string[] lines = File.ReadAllLines(this.GameStatePath, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] tokens = line.Split(' ');
                if (tokens.Length != 3)
                {
                    continue;
                }

                if (int.TryParse(tokens[0], out x) == false)
                {
                    continue;
                }
                if (int.TryParse(tokens[1], out y) == false)
                {
                    continue;
                }
                if (Enum.TryParse<PointState>(tokens[2], out state) == false)
                {
                    continue;
                }

                this.BoardState[x, y] = state;
                if (state == PointState.You)
                {
                    this.PlayerX = x;
                    this.PlayerY = y;
                }
                if (state == PointState.Opponent)
                {
                    this.OpponentX = x;
                    this.OpponentY = y;
                }
            }
        }
        public void WriteGameState()
        {
            using (TextWriter writer = File.CreateText(this.GameStatePath))
            {
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 30; y++)
                    {
                        writer.WriteLine("{0} {1} {2}", x, y, this.BoardState[x, y].ToString());
                    }
                }
                writer.Close();
            }
        }
    }
}
