using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int r;
        private int xMovement;
        private int yMovement;

        public Ball(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.xMovement = 0;
            this.yMovement = 0;
        }

        public void MakeMove()
        {
            X += xMovement;
            Y += yMovement;
        }

        public int X
        { get { return x; } set { x = value; OnPropertyChanged(); } }
        public int Y
        { get { return y; } set { y = value; OnPropertyChanged(); } }
        public int R
        { get { return r; } set { r = value; OnPropertyChanged(); } }
        public int XMovement
        { get { return xMovement; } set { xMovement = value; } }
        public int YMovement
        { get { return yMovement; } set { yMovement = value; } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}