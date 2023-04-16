using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Circle : INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int r;

        public Circle(Ball ball)
        {
            this.x = ball.XCord;
            this.y = ball.YCord;
            this.r = ball.Radius;
            ball.PropertyChanged += Update;
        }

        private void Update(object _object, PropertyChangedEventArgs args)
        {
            Ball ball = (Ball)_object;
            if (args.PropertyName == "X")
            {
                this.x = ball.XCord - ball.Radius;
                OnPropertyChanged(nameof(X));
            }
            if (args.PropertyName == "Y")
            {
                this.y = ball.YCord - ball.Radius;
                OnPropertyChanged(nameof(Y));
            }
            if (args.PropertyName == "R")
            {
                this.r = ball.Radius;
                OnPropertyChanged(nameof(R));
            }
        }

        public int X
        { get { return x; } set { x = value; OnPropertyChanged(); } }
        public int Y
        { get { return y; } set { y = value; OnPropertyChanged(); } }
        public int R
        { get { return r; } set { r = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}