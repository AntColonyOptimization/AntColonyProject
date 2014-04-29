using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;



namespace Ants
{
    class Algorithm
    {
        private int height;
        private int width;
        private static char symbolStart = 'S';
        private static char symbolDestination = 'F';
        private static char symbolObstacle = 'x';
        private static char symbolFreeToGo = '0';

        private double alpha;
        private double beta;
        private double rho;
        private double Q;
        private double pheromoneInit;

        private int numAnts;
        private int numIter;
        private int bestLength;
        List<Coordinates> bestPath;
        private Random random = new Random();
        private int mainIterator;

        private Coordinates start;
        private Coordinates destination;

        private char[,] map = new char[1000, 1000];
        private double[,] distances = new double[1000, 1000];
        private double[,] pheromones = new double[1000, 1000];
        private int[,] numbersOfVisits = new int[1000, 1000];
        private int currentAnt;

        List<List<Coordinates>> path = new List<List<Coordinates>>();



        public Algorithm()
        {
            alpha = 3;
            beta = 10;
            rho = 0.01;
            Q = 50.0;
            pheromoneInit = 0.01;
            numIter = 100;
            numAnts = 3;
            currentAnt = 0;
            bestLength = 99999999;
            mainIterator = 0;
            for (mainIterator = 0; mainIterator < numAnts; mainIterator++)
            {
                path.Add(new List<Coordinates>());
            }
        }

        public void Execute()
        {
            ReadMapFromFile();
            CalculateDistances();
            for (int i = 0; i < numIter; i++)
            {
                for (currentAnt = 0; currentAnt < numAnts; currentAnt++)
                {
                    BuildPath();
                }
                updatePheromones();//pheromones are updated after all the ants in one operation found path
            }
            int a = 10;
        }

        public void resetNumOfVisits()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    numbersOfVisits[i, j] = 0;
                }
            }
        }

        public void ReadMapFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"../../map.txt");

            System.Console.WriteLine("Contents of WriteLines2.txt = ");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                height = lines.Length;
                width = line.Length;
                for (int j = 0; j < line.Length; j++)
                {
                    map[i, j] = char.Parse(line[j]);
                    if (map[i, j] == symbolDestination)
                    {
                        destination = new Coordinates(j, i);
                    }
                    if (map[i, j] == symbolStart)
                    {
                        start = new Coordinates(j, i);
                    }
                }
            }
        }


        public void CalculateDistances()
        {
            int tempWidth;
            int tempHeight;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] != symbolObstacle)
                    {
                        tempWidth = (destination.Width - j) * (destination.Width - j);
                        tempHeight = (destination.Height - i) * (destination.Height - i);
                        distances[i, j] = Math.Sqrt(tempWidth + tempHeight);
                        pheromones[i, j] = pheromoneInit;
                    }
                    else
                    {
                        distances[i, j] = -1;
                    }
                }
            }
        }

        public double[,] CalculateProbabilities(Coordinates currentPos)
        {
            double[,] taueta = new double[3, 3];
            double sum = 0.0;
            int setValue = 0;
            int minHeight = (currentPos.Height - 1 < 0) ? 0 : currentPos.Height - 1;
            int minWidth = (currentPos.Width - 1 < 0) ? 0 : currentPos.Width - 1;
            int maxHeight = (currentPos.Height + 2 >= height) ? height : currentPos.Height + 2;
            int maxWidth = (currentPos.Width + 2 >= width) ? width : currentPos.Width + 2;
            while (sum == 0)
            {
                for (int i = minHeight; i < maxHeight; i++)
                {
                    for (int j = minWidth; j < maxWidth; j++)
                    {
                        if (map[i, j] != symbolObstacle)
                        {
                            if (i == currentPos.Height && j == currentPos.Width)
                            {
                                taueta[i - currentPos.Height + 1, j - currentPos.Width + 1] = 0.0; // Prob of moving to self is zero
                            }
                            else if (numbersOfVisits[i, j] > setValue)
                            {
                                taueta[i - currentPos.Height + 1, j - currentPos.Width + 1] = 0.0;
                            }
                            else
                            {
                                taueta[i - currentPos.Height + 1, j - currentPos.Width + 1] = Math.Pow(pheromones[i, j], alpha) *
                                  Math.Pow((1.0 / distances[i, j]), beta);
                            }
                            sum += taueta[i - currentPos.Height + 1, j - currentPos.Width + 1];
                        }
                    }
                }
                if (sum == 0)
                {
                    setValue++;
                }
            }
            if (sum == 0)
            {
                int a = 10;
                throw new Exception("sum=0, wszystkie pola w kolo zostaly odwiedzone");
                //go random
            }


            double[,] probabilities = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    probabilities[i, j] = taueta[i, j] / sum;
                }
            }
            return probabilities;
        }

        public Coordinates FindNextStep(Coordinates currentPos)
        {
            double[,] probabilities = CalculateProbabilities(currentPos);

            double[] cumulativeProbs = new double[10];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cumulativeProbs[i * 3 + j + 1] = cumulativeProbs[i * 3 + j]
                                                   + probabilities[i, j];
                }
            }

            double p = random.NextDouble();

            for (int i = 0; i < cumulativeProbs.Length - 1; ++i)
            {
                if (p >= cumulativeProbs[i] && p < cumulativeProbs[i + 1])
                {
                    return new Coordinates(i / 3 - 1, i % 3 - 1);
                }
            }
            Console.WriteLine("Im here");
            return new Coordinates(1, 1);
            //throw new Exception("Failure to return valid city in NextCity");
        }

        public void BuildPath()
        {
            resetNumOfVisits();
            path[currentAnt] = new List<Coordinates>();
            path[currentAnt].Add(start);
            numbersOfVisits[start.Height, start.Width]++;
            while (!IsDestinationFound((Coordinates)path[currentAnt][path[currentAnt].Count - 1]))
            {
                Coordinates currentPos = (Coordinates)path[currentAnt][path[currentAnt].Count - 1];
                Coordinates next = FindNextStep(currentPos);
                next.SetOffset(currentPos);
                path[currentAnt].Add(next);

                numbersOfVisits[next.Height, next.Width]++;
            }
            if (path[currentAnt].Count < bestLength)
            {
                bestLength = path[currentAnt].Count;
                bestPath = path[currentAnt];
            }
        }

        public bool IsDestinationFound(Coordinates pos)
        {
            return Math.Abs(pos.Height - destination.Height) <= 1 && Math.Abs(pos.Width - destination.Width) <= 1;
            //stop conditions, last found must be next to dest.
        }

        public void updatePheromones()
        {
            double length = 0;
            double decrease = 0;
            double increase = 0;
            for (int k = 0; k < numAnts; k++)
            {
                length = path[k].Count;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        decrease = (1.0 - rho) * pheromones[i, j];
                        increase = 0.0;
                        if (path[k].Contains(new Coordinates(i, j)))
                        {
                            increase = (Q / length);
                        }
                        pheromones[i, j] = decrease + increase;
                    }
                }
            }
        }

    }
}
