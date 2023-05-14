using System;
using System.Collections.Generic;

namespace Data

{
    public abstract class AbstractDataAPI
    {
        public static AbstractDataAPI CreateApi()
        {
            return new DataApi();
        }

        public abstract void InitiateBoard(int height, int width, int ballQuantity, int ballRadius, int ballMass);
        public abstract void CreateBalls();
        public abstract List<Ball> GetBallsList();
        public abstract bool IsEnabled();
        public abstract void Disable();
        public abstract void Enable();
        public abstract bool CheckCoordinates(int x, int y, int r);

        internal class DataApi : AbstractDataAPI
        {

        }
    }
}
