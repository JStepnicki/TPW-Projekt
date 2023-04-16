using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    internal sealed class BoardManager : AbstractLogicAPI
    {
        private Board board;
        private List<Task> tasks = new List<Task>();
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);

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
                        ball.XMovement = random.Value.Next(-7, 7);
                        ball.YMovement = random.Value.Next(-7, 7);
                        if (0 > (ball.XCord + ball.XMovement - ball.Radius) || board.Width < (ball.XCord + ball.XMovement + ball.Radius))
                        {
                            ball.XMovement *= -1;
                        }
                        if (0 > (ball.YCord + ball.YMovement - ball.Radius) || board.Height < (ball.YCord + ball.YMovement + ball.Radius))
                        {
                            ball.YMovement *= -1;
                        }
                        ball.MakeMove();
                        Thread.Sleep(3);
                        semaphore.Release();
                    }
                });
                tasks.Add(task);
            });
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
        public override List<Ball> GetBallsList()
        {
            return board.Balls;
        }
    }
}
