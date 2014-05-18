using IOService.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Ants.Map;

namespace Ants
{
    public class Algorithm: IAlgorithm
    {
        //private int height;
        //private int width;

        private readonly double alpha;
        private readonly double beta;
        private readonly double rho;
        private readonly double Q;
        private readonly double pheromoneInit;
        private readonly int numAnts;
        private readonly int numIter;
        private int bestLength;
        private bool isFinished;

        private List<Coordinates> bestPath;
        private readonly Random random = new Random();
        private int mainIterator;

        private readonly List<List<double>> distances = new List<List<double>>();
        private readonly List<List<double>> pheromones = new List<List<double>>();
        private List<List<int>> numOfVisits = new List<List<int>>();
        private readonly int[,] numbersOfVisits = new int[1000, 1000];
        private int currentAnt;
        private readonly List<List<Coordinates>> path = new List<List<Coordinates>>();

        private readonly IOutputService _outputService;

        private readonly Map.Map _map;

        public List<List<double>> Pheromones
        {
            get
            {
                return this.pheromones;
            }
            set{}
        }
        public List<List<Coordinates>> Path
        {
            get
            {
                return this.path;
            }
            set { }
        }

        public Algorithm(IInputService input, Map.Map map)
        {
            _map = map;
            alpha = input.Alpha;
            beta = input.Beta;
            rho = input.Rho;
            Q = input.Q;
            pheromoneInit = 0.01;
            numIter = input.NumberOfIterations;
            numAnts = input.NumberOfAnts;
            currentAnt = 0;
            mainIterator = 0;
            bestLength = 99999999;
            mainIterator = 0;
            isFinished = false;
            for (int i = 0; i < numAnts; i++)
            {
                path.Add(new List<Coordinates>());
            }
            CalculateDistances();

            _outputService = new OutputService();
        }

        public IOutputService Execute()
        {
            if(mainIterator < numIter)
            {
                for (currentAnt = 0; currentAnt < numAnts; currentAnt++)
                {
                    BuildPath(_map);
                }
                updatePheromones(_map);//pheromones are updated after all the ants in one operation found path
                mainIterator++;

                _outputService.Pheromones = pheromones;
                _outputService.CurrentPaths = path;
                _outputService.BestPath = bestPath;

                return _outputService;
            }
            if(mainIterator==numIter)
            {
                isFinished = true;
            }

            _outputService.Pheromones = pheromones;
            _outputService.CurrentPaths = path;
            _outputService.BestPath = bestPath;

            return _outputService;
        }

        public bool IsFinished()
        {
            return isFinished;
        }

        private void resetNumOfVisits(Map.Map map)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    numbersOfVisits[i, j] = 0;
                }
            }
        }


        private void CalculateDistances()
        {
            int tempWidth;
            int tempHeight;
            for (int i = 0; i < _map.Height; i++)
            {
                distances.Add(new List<double>());
                pheromones.Add(new List<double>());
                for (int j = 0; j < _map.Width; j++)
                {
                    if (_map.MapDescription[i][j] != MapSymbols.SymbolObstacle)
                    {
                        tempWidth = (_map.Destination.Width - j) * (_map.Destination.Width - j);
                        tempHeight = (_map.Destination.Height - i) * (_map.Destination.Height - i);
                        distances[i].Add(Math.Sqrt(tempWidth + tempHeight));
                        pheromones[i].Add(pheromoneInit);
                    }
                    else
                    {
                        distances[i].Add(-1);
                        pheromones[i].Add(0);
                    }
                }
            }
        }

        private double[,] CalculateProbabilities(Coordinates currentPos)
        {
            //var map = Map.MapGenerator.Instance.GetMap();
            double[,] taueta = new double[3, 3];
            double sum = 0.0;
            int setValue = 0;
            int minHeight = (currentPos.Height - 1 < 0) ? 0 : currentPos.Height - 1;
            int minWidth = (currentPos.Width - 1 < 0) ? 0 : currentPos.Width - 1;
            int maxHeight = (currentPos.Height + 2 >= _map.Height) ? _map.Height : currentPos.Height + 2;
            int maxWidth = (currentPos.Width + 2 >= _map.Width) ? _map.Width : currentPos.Width + 2;
            while (sum == 0)
            {
                for (int i = minHeight; i < maxHeight; i++)
                {
                    for (int j = minWidth; j < maxWidth; j++)
                    {
                        if (_map.MapDescription[i][j] != MapSymbols.SymbolObstacle)
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
                                taueta[i - currentPos.Height + 1, j - currentPos.Width + 1] = Math.Pow(pheromones[i][j], alpha) *
                                  Math.Pow((1.0 / distances[i][j]), beta);
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

        private Coordinates FindNextStep(Coordinates currentPos)
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

        private void BuildPath(Map.Map map)
        {
            resetNumOfVisits(map);
            path[currentAnt] = new List<Coordinates>();
            path[currentAnt].Add(map.Start);
            numbersOfVisits[map.Start.Height, map.Start.Width]++;
            while (!IsDestinationFound((Coordinates)path[currentAnt][path[currentAnt].Count - 1], map))
            {
                Coordinates currentPos = (Coordinates)path[currentAnt][path[currentAnt].Count - 1];
                Coordinates next = FindNextStep(currentPos);
                next.SetOffset(currentPos);
                path[currentAnt].Add(next);

                numbersOfVisits[next.Height, next.Width]++;
            }
            //path[currentAnt].Add(map.Destination);
            if (path[currentAnt].Count < bestLength)
            {
                bestLength = path[currentAnt].Count;
                bestPath = path[currentAnt];
            }
        }

        private bool IsDestinationFound(Coordinates pos, Map.Map map)
        {
            return Math.Abs(pos.Height - map.Destination.Height) <= 1 && Math.Abs(pos.Width - map.Destination.Width) <= 1;
            //stop conditions, last found must be next to dest.
        }

        private void updatePheromones(Map.Map map)
        {
            double length = 0;
            double decrease = 0;
            double increase = 0;
            for (int k = 0; k < numAnts; k++)
            {
                length = path[k].Count;
                for (int i = 0; i < map.Height; i++)
                {
                    for (int j = 0; j < map.Width; j++)
                    {
                        decrease = (1.0 - rho) * pheromones[i][j];
                        increase = 0.0;
                        if (path[k].Contains(new Coordinates(i, j)))
                        {
                            increase = (Q / length);
                        }
                        pheromones[i][j] = decrease + increase;
                    }
                }
            }
        }

    }
}
