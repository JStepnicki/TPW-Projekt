using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model
{

    internal class Circle : CircleApi, INotifyPropertyChanged
    {
        public override int x { get => _x; set { _x = value; RaisePropertyChanged(); } }
        public override int y { get => _y; set { _y = value; RaisePropertyChanged(); } }

        public override int radius { get => _radius; set { _radius = value; RaisePropertyChanged(); } }

        private int _x { get; set; }
        private int _y { get; set; }
        private int _radius { get; set; }

        public Circle(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

      
        public override void UpdateCircle(Object s, LogicEventArgs e)
        {
            LogicBallApi ball = (LogicBallApi)s;
            x = (int)ball.Position.X;
            y = (int)ball.Position.Y;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }



}
