using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelAPI
    {
        public static AbstractModelAPI CreateAPI(AbstractLogicAPI abstractLogic = null)
        {
            return new CircleManager(abstractLogic);
        }
        public abstract void CreateBoard(int height, int width, int ballQuantity, int ballRadius);
        public abstract void CreateBalls();
        public abstract ObservableCollection<Circle> GetCirclesList();
        public abstract bool IsEnabled();
        public abstract void Disable();
        public abstract void Enable();
        

       
    }
}