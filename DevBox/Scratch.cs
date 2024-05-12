using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DevBox
{

    /// <summary>
    /// this is used to test silly things
    /// 
    /// TEST
    /// </summary>
    public class Scratch
    {
            DateTime startTime;
           
            DateTime endTime;
            double answer;
            double diff2;
            double diff3;
            Vector2 pos1 = new Vector2(10, 10);
            Vector2 pos2 = new Vector2(50, 50);

            public void CalcDiff()
            {

                startTime = DateTime.Now;
                for (int i = 0; i < 10000000; i++)
                {
                    pos1.X = 100;
                    pos1.Y = 100;

                    pos2.X = 500;
                    pos2.Y = 500;

                    answer = Math.Sqrt((pos2.X - pos1.X) * (pos2.X - pos1.X) + (pos2.Y - pos1.Y) * (pos2.Y - pos1.Y));
                }

                    endTime = DateTime.Now;

                    diff2 = (endTime - startTime).TotalMilliseconds;
                    Console.WriteLine(diff2);

                    startTime = DateTime.Now;
                    for (int i = 0; i < 10000000; i++)
                    {
                        pos1.X = 100;
                        pos1.Y = 100;

                        pos2.X = 500;
                        pos2.Y = 500;

                        answer = Vector2.Distance(pos1, pos2);
                    }

                    endTime = DateTime.Now;

                    diff3 = (endTime - startTime).TotalMilliseconds;
                    Console.WriteLine(diff3);
            }

    }
}