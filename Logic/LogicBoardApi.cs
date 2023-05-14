using System.Runtime.CompilerServices;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new LogicApi(400, 580);
        }


        public abstract void AddBalls(int number, int radius);

        public abstract void ClearBoard();

        public abstract List<LogicBallApi> GetAllBalls();

    }
}
