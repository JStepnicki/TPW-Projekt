using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI CreateAPIInstance()
        {
            return new ModelAPI();
        }

        public abstract void Start(int BallsAmount, int Radius);

        public abstract void ClearBalls();

        public abstract ObservableCollection<CircleApi> GetCircles();








    }
}
