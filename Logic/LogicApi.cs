using System.Runtime.CompilerServices;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Logic
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateApi(AbstractDataApi abstractDataAPI = null)
        {
            return new LogicAPI(abstractDataAPI);
        }
        public abstract void CreateBoard(int height, int width, int ballQuantity, int ballRadius);
        public abstract void CreateBalls();
        public abstract List<Ball> GetBallsList();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();

        internal sealed class LogicAPI : AbstractLogicAPI
        {
            private AbstractDataApi dataApi;
            private Board board;
            private List<Task> tasks = new List<Task>();
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

            public override void CreateBoard(int height, int width, int ballsQuantity, int ballRadius)
            {
                board = new Board(height, width);
                board.CreateBallsList(ballsQuantity, ballRadius);
            }
            public override void CreateBalls()
            {
                ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

                foreach (Ball ball in board.Balls)
                {
                    Task task = new Task(() =>
                    {
                        while (this.IsRunning())
                        {
                            lock (ball)
                            {
                                ball.XMovement = random.Value.Next(-10000, 10000) % 5;
                                ball.YMovement = random.Value.Next(-10000, 10000) % 5;

                                if (0 > (ball.X + ball.XMovement - ball.R) ||
                                    board.Width < (ball.X + ball.XMovement + ball.R))
                                {
                                    ball.XMovement = -ball.XMovement;
                                }
                                if (0 > (ball.Y + ball.YMovement - ball.R) ||
                                    board.Height < (ball.Y + ball.YMovement + ball.R))
                                {
                                    ball.YMovement = -ball.YMovement;
                                }

                                ball.MakeMove();
                                Thread.Sleep(10);
                            }
                        }
                    });
                    tasks.Add(task);
                }
            }

            public override List<Ball> GetBallsList()
            {
                return board.Balls;
            }
            public override void TurnOff()
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
            public override void TurnOn()
            {
                board.IsRunning = true;
                foreach (Task task in tasks)
                {
                    try
                    {
                        task.Start();
                    }
                    catch
                    {

                    }
                }
            }
            public override bool IsRunning()
            {
                return board.IsRunning;
            }
        }
    }
}