using System.Runtime.CompilerServices;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateApi(AbstractDataApi abstractDataAPI = null)
        {
            return new LogicAPI(abstractDataAPI);
        }
        public abstract void InitiateBoard(int height, int width, int ballQuantity, int ballRadius);
        public abstract void CreateBalls();
        public abstract List<Ball> GetBallsList();
        public abstract bool IsEnabled();
        public abstract void Disable();
        public abstract void Enable();

        internal sealed class LogicAPI : AbstractLogicAPI
        {
            private AbstractDataApi dataApi;
            private Board board;
            private List<Task> tasks = new List<Task>();
            private SemaphoreSlim semaphore = new SemaphoreSlim(1);
            public LogicAPI(AbstractDataApi abstractDataAPI = null)
            {
                if (abstractDataAPI == null)
                {
                    dataApi = AbstractDataApi.CreateApi();
                }
                else
                {
                    dataApi = abstractDataAPI;
                }
            }

            public override void InitiateBoard(int height, int width, int ballsQuantity, int ballRadius)
            {
                board = new Board(height, width);
                board.FillBallList(ballsQuantity, ballRadius);
            }
            public override void CreateBalls()
            {
                
                ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

                Parallel.ForEach(board.Balls, ball =>
                {
                    Task task = new Task(async () =>
                    {
                        while (this.IsEnabled())
                        {

                                await semaphore.WaitAsync();
                                ball.XMovement = random.Value.Next(-10, 10);
                                ball.YMovement = random.Value.Next(-10, 10);

                                if (0 > (ball.XCord + ball.XMovement - ball.Radius) || board.Width < (ball.XCord + ball.XMovement + ball.Radius))
                                {
                                    ball.XMovement = ball.XMovement * -1; 
                                }
                                if (0 > (ball.YCord + ball.YMovement - ball.Radius) || board.Height < (ball.YCord + ball.YMovement + ball.Radius))
                                {
                                    ball.YMovement = ball.YMovement * -1;
                                }
                                ball.MakeMove();
                                Thread.Sleep(1);
                                semaphore.Release();
                            
                        }
                    });
                    tasks.Add(task);
                }) ;
            }

            public override List<Ball> GetBallsList()
            {
                return board.Balls;
            }
            public override void Disable()
            {
                board.IsRunning = false;
                bool isAllTasksCompleted = false;

                while (!isAllTasksCompleted)
                {
                    isAllTasksCompleted = true;
                    foreach (Task task in tasks)
                    {
                        if (!task.IsCompleted)
                        {
                            isAllTasksCompleted = false;
                            break;
                        }
                    }
                }

                foreach (Task task in tasks)
                {
                    task.Dispose();
                }
                tasks.Clear();
                board.Balls.Clear();
            }
            public override void Enable()
            {
                board.IsRunning = true;
                foreach (Task task in tasks)
                {
                 
                        task.Start();
               
                }
            }
            public override bool IsEnabled()
            {
                return board.IsRunning;
            }
        }
    }
}