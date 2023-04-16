using Logic;
using Model;
using System.Collections.ObjectModel;

internal sealed class CircleManager : AbstractModelAPI
{
    private AbstractLogicAPI logicAPI;
    private ObservableCollection<Circle> circles = new ObservableCollection<Circle>();

    public CircleManager(AbstractLogicAPI abstractLogicAPI)
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
        logicAPI.InitiateBoard(height, width, ballQuantity, ballRadius);
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

    public override bool IsEnabled()
    {
        return logicAPI.IsEnabled();
    }
    public override void Disable()
    {
        logicAPI.Disable();
    }
    public override void Enable()
    {
        logicAPI.Enable();
    }
}
