using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new Board(800, 600);
        }

        public abstract void CreateTasks(int quantity, int radius);

        public abstract void EnableMovement();
        public abstract void ClearBoard();

        public abstract List<List<int>> GetBallsPosition();
    }
}
