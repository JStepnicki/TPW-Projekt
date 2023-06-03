﻿using Data;
using System.Collections.Generic;

namespace Logic
{
    public abstract class LogicBoardApi
    {
        public static LogicBoardApi CreateAPI(BoardApi dataApi = null)
        {
            return new LogicBoard(dataApi == null ? BoardApi.CreateApi(600,400) : dataApi);
        }

        public abstract void AddBalls(int number, int radius);

        public abstract void ClearBoard();

        public abstract List<LogicBallApi> GetAllBalls();

    }
}
