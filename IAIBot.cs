using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronLC.Framework
{
    public interface IAIBot
    {
        void ExecuteMove(string gameStateFile);
    }
}
