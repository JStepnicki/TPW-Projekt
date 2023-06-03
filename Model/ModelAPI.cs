﻿using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicBoardApi _logicAPI;
        private ObservableCollection<CircleApi> Circles = new ObservableCollection<CircleApi>();
        private int _radius;

        public ModelAPI()
        {
            _logicAPI = LogicBoardApi.CreateAPI();
        }


        public override ObservableCollection<CircleApi> GetCircles()
        {
            Circles.Clear();
            foreach (LogicBallApi ball in _logicAPI.GetAllBalls())
            {
                CircleApi c = CircleApi.CreateCircle((int)ball.Position.X, (int)ball.Position.Y, _radius);
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
