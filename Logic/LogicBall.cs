using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class LogicBall:LogicBallApi
    {
        private double _PosX;
        private double _PosY;



        public override event EventHandler<LogicEventArgs>? ChangedPosition;
        // To wykrywa (I suppose) wszystkie wywolania RaisePropertyChanged()
        public override double PosX
        {
            get => _PosX;
            set { _PosX = value; }
        }
        public override double PosY
        {
            get => _PosY;
            set { _PosY = value; }
        }
       

        internal LogicBall(double posX, double posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }



        public void UpdateBall(Object s, DataEventArgs e)
        {
            BallApi ball = (BallApi)s;
            PosX = ball.xCordinate;
            PosY = ball.yCordinate;
            LogicEventArgs args = new LogicEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }
       


    }


}


