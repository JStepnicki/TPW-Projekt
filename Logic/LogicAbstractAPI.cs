using System.Runtime.CompilerServices;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Logic
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateApi(AbstractDataAPI dataApi)
        {
            if (dataApi == null)
            {
                return new BoardManager(AbstractDataAPI.CreateApi());
            }
            else
            {
                return new BoardManager(dataApi);
            }
        }
        public abstract void InitiateBoard(int height, int width, int ballQuantity, int ballRadius, int ballMass);
        public abstract void CreateBalls();
        public abstract List<Ball> GetBallsList();
        public abstract bool IsEnabled();
        public abstract void Disable();
        public abstract void Enable();

        
    }
}