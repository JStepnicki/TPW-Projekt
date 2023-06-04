using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class LogicEventArgs   {
        public LogicBallApi ball;
        public LogicEventArgs(LogicBallApi ball)
        {
            this.ball = ball;
        }
    }
}

