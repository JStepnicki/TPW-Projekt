using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataEventArgs
        {
            public BallApi Ball;
            public DataEventArgs(BallApi ball)
            {
                Ball = ball;
            }
        }
    
}
