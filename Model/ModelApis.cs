using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelAPI
    {
        public static AbstractModelAPI CreateAPI(AbstractLogicAPI abstractLogic = null)
        {
            return new ModelAPI(abstractLogic);
        }
        public abstract void CreateBoard(int height, int width, int ballQuantity, int ballRadius);
        public abstract void CreateBalls();
        public abstract ObservableCollection<Circle> GetCirclesList();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();

        internal sealed class ModelAPI : AbstractModelAPI
        {
            private AbstractLogicAPI logicAPI;
            private ObservableCollection<Circle> circles = new ObservableCollection<Circle>();

            public ModelAPI(AbstractLogicAPI abstractLogicAPI)
            {
                if (abstractLogicAPI == null)
                {
                    this.logicAPI = AbstractLogicAPI.CreateApi();
                }
                else
                {
                    this.logicAPI = abstractLogicAPI;
                }
            }

            public ObservableCollection<Circle> Circles
            { get { return this.circles; } set { this.circles = value; } }

            public override void CreateBoard(int height, int width, int ballQuantity, int ballRadius)
            {
                logicAPI.CreateBoard(height, width, ballQuantity, ballRadius);
            }
            public override void CreateBalls()
            {
                logicAPI.CreateBalls();
            }
            public override ObservableCollection<Circle> GetCirclesList()
            {
                circles.Clear();
                foreach (Ball ball in logicAPI.GetBallsList())
                {
                    circles.Add(new Circle(ball));
                }
                return circles;
            }

            public override bool IsRunning()
            {
                return logicAPI.IsRunning();
            }
            public override void TurnOff()
            {
                logicAPI.TurnOff();
            }
            public override void TurnOn()
            {
                logicAPI.TurnOn();
            }
        }
    }
}