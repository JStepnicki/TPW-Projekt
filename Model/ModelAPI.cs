﻿using Logic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicAbstractAPI _logicAPI;
        private ObservableCollection<CircleApi> Circles = new ObservableCollection<CircleApi>();
        private int _radius;

        public ModelAPI()
        {
            _logicAPI = LogicAbstractAPI.CreateAPIInstance();
        }


        public override ObservableCollection<CircleApi> GetCircles()
        {
            Circles.Clear();
            foreach (IBall ball in _logicAPI.GetAllBalls())
            {
                CircleApi c = CircleApi.CreateCircle((int)ball.PosX, (int)ball.PosY, _radius);
                Circles.Add(c);                                  //Ponizej dodajemy metode, ktora bedzie wywolywana za kazdym razem, gdy ball zglosi PropertyChanged
                ball.ChangedPosition += c.UpdateCircle!;         //wykrzyknik nie jest konieczny, to tylko mowi kompilatorowi ze metoda UpdateCircle nie bedzie NULLem
            }
            return Circles;
        }


        public override void ClearBalls()
        {
            _logicAPI.ClearBoard();
        }

        public override void Start(int BallsAmount, int Radius)
        {
            _radius = Radius;
            _logicAPI.AddBalls(BallsAmount, Radius);
        }


    }
}
