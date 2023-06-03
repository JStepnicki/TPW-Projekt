using Logic;
using System;
using System.ComponentModel;

namespace Model
{
    public abstract class CircleApi
    {
        public static CircleApi CreateCircle(int x, int y, int radius)
        {
            return new Circle(x, y, radius);
        }


        public abstract int x { get; set; }
        public abstract int y { get; set; }
        public abstract int radius { get; set; }

        public abstract void UpdateCircle(Object s, LogicEventArgs e);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
        //public abstract void RaisePropertyChanged([CallerMemberName] string? propertyName = null);
    }
}
