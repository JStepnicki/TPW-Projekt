using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static Data.BoardApi;

namespace Data
{
    public abstract class BallApi : INotifyPropertyChanged
    {
        public abstract void MakeMove();
        public abstract int xCordinate { get; set; }
        public abstract int yCordinate { get; set; }
        public abstract int radius { get; set; }
        public abstract int mass { get; set; }
        public abstract int xMove { get; set; }
        public abstract int yMove { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public BallApi CreateBall(int x, int y, int r, int m)
        {
            return new Ball(x, y, r, m);
        }

        internal class Ball : BallApi
        {
            private int _mass;
            private int _xCordinate;
            private int _yCordinate;
            private int _radius;
            private int _xMove;
            private int _yMove;
            public Ball(int x, int y,int r,int m)
            {  
                _mass = m;
                _xCordinate = x;
                _yCordinate = y;
                _radius = r;
            }
            public override void MakeMove()
            {
                xCordinate += xMove;
                xCordinate += yMove;
            }

            public override int xCordinate
            {
                get { return _xCordinate; }
                set { _xCordinate = value; OnPropertyChanged("X"); } // "X" to nazwa Eventu
            }
            public override int yCordinate
            {
                get { return _yCordinate; }
                set { _yCordinate = value; OnPropertyChanged("Y"); }
            }
            public override int radius
            {
                get { return _radius; }
                set { _radius = value; }
            }
            public override int mass
            {
                get { return _mass; }
                set { _mass = value; }
            }
            public override int xMove
            {
                get { return _xMove; }
                set { _xMove = value; }
            }
            public override int yMove
            {
                get { return _yMove; }
                set { _yMove = value; }
            }

            public override event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private async void CreateTask()
            {
                await Task.Run(async () =>
                {
                    Random random = new Random();

                    while (true)
                    {
                        await Task.Delay(3);

                        if (0 > (_xCordinate + _xMove - _radius) || Board.width < (_xCordinate + _xMove + _radius))
                        {
                            _xMove *= -1;
                        }
                        if (0 > (_yCordinate + _yMove - _radius) || board.height < (_yCordinate + _yMove + _radius))
                        {
                            _yMove *= -1;
                        }
                        MakeMove();
                    }
                });
            }
        }
    }
}