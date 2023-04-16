using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Logic
{
    public class Board : LogicAbstractAPI
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Ball> Balls { get; set; }

        public Board(int sizeX, int sizeY)
        {
            this.BoardWidth = sizeX;
            this.BoardHeight = sizeY;
            Tasks = new List<Task>();
        }


        public override void CreateTasks(int quantity, int radius)
        {
            for (int i = 0; i < quantity; i++)
            {
                Tasks.Add(new Task(() =>
                {
                    Random random = new Random();
                    int x = random.Next(radius, BoardWidth - radius);
                    int y = random.Next(radius, BoardHeight - radius);
                    Ball ball = new Ball(x, y, radius);
                    Balls.Add(ball);
                    
                    while (true)
                    {
                        while (true)
                        {
                            ball.RandSpeed(-5, 5);
                            if (ball.CheckColission(this.BoardWidth, this.BoardHeight))
                            {
                                ball.MoveBall();
                                break;
                            }
                            else
                            {
                                ball.RandSpeed(-5, 5);
                            }
                        }
                        Thread.Sleep(400);
                    }
                }));

            }
        }


        public override void EnableMovement()
        {
            foreach (Task task in Tasks)
            {
                task.Start();
            }
        }
        public override void ClearBoard()
        {
            foreach (Task task in Tasks)
            {
                task.Dispose();
            }
        }

        public override List<List<int>> GetBallsPosition()
        {
            {
                List<List<int>> positions = new List<List<int>>();
                foreach (Ball b in Balls)
                {
                    List<int> BallPosition = new List<int>
                {
                    b.Xpos,
                    b.Ypos
                };
                    positions.Add(BallPosition);
                }
                return positions;
            }

        }
    }

}
