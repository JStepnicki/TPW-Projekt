using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        private int mass;
        private int xCordinate;
        private int yCordinate;
        private int r;
        private int xMove;
        private int yMove;

        public Ball(int x, int y, int r, int m)
        {
            this.xCordinate = x;
            this.yCordinate = y;
            this.r = r;
            this.mass = m;
            this.xMove = 0;
            this.yMove = 0;
        }

        public void MakeMove()
        {
            XCord += xMove;
            YCord += yMove;
        }

        public int XCord
        { 
            get { return xCordinate; } 
            set { xCordinate = value; OnPropertyChanged("X"); } // "X" to nazwa Eventu
        }
        public int YCord
        { 
            get { return yCordinate; } 
            set { yCordinate = value; OnPropertyChanged("Y"); } 
        }
        public int Radius
        { 
            get { return r; } 
            set { r = value; OnPropertyChanged("R"); } 
        }
        public int Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        public int XMovement
        { 
            get { return xMove; } 
            set { xMove = value; } 
        }
        public int YMovement
        { 
            get { return yMove; } 
            set { yMove = value; } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}