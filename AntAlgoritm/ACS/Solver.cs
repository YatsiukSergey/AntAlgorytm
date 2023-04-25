using System.Diagnostics;
using AntAlgoritm.Graph;

namespace AntAlgoritm.ACS
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        public int LimitedDistance { get; set; }
        private Ant GlobalBestAnt { get; set; }
        private Graph.Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }
        public List<Ant> GlobalBestAntColony = new List<Ant>();
        public Solver(Parameters parameters, Graph.Graph graph, int limitedDistance)
        {
            Parameters = parameters;
            graph.MinimumPheromone = parameters.T0;
            Graph = graph;
            Stopwatch = new Stopwatch();
            LimitedDistance = limitedDistance;

        }

        /// <summary>
        /// Main loop of ACS algorithm
        /// </summary>
        public List<Ant> RunAcs()
        {
            Stopwatch.Start();
            Graph.ResetPheromone(Parameters.T0);
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                List<Ant> antColony = CreateAnts();
                GlobalBestAnt = antColony[0];

                Ant localBestAnt = BuildTours(antColony);
                if (Math.Round(localBestAnt.Distance, 2) < Math.Round(GlobalBestAnt.Distance, 2))
                {
                    GlobalBestAnt = localBestAnt;
                    GlobalBestAntColony.Add(localBestAnt);
                   /* for (int j = 0; j < GlobalBestAnt.VisitedNodes.Count; j++)
                    {
                        Console.Write(GlobalBestAnt.VisitedNodes[j].Id + " ");
                    }

                    Console.WriteLine(
                        "Current Global Best: " + GlobalBestAnt.Distance + " found in " + i + " iteration");
               */
                    }
               
 
            }
            GlobalBestAntColony.Sort((a1, a2) =>
            {
                int countComparison = a2.VisitedNodes.Count.CompareTo(a1.VisitedNodes.Count);
                if (countComparison != 0)
                {
                    return countComparison;
                }
                else
                {
                    return a2.Distance.CompareTo(a1.Distance) * -1;
                }
            });
            Stopwatch.Stop();
            return GlobalBestAntColony;
        }

        /// <summary>
        /// Create ants and place every ant in random point on graph (warning AntCount < Dimensions)
        /// </summary>
        public List<Ant> CreateAnts()
        {
            List<Ant> antColony = new List<Ant>();
            for (int i = 0; i < Graph.Points.Count; i++)
            {
                Ant ant = new Ant(Graph, Parameters.Beta, Parameters.Q0, 1,LimitedDistance);
                antColony.Add(ant);
            }

            return antColony;
        }

        /// <summary>
        /// This method builds solution for every ant in AntColony and return the best ant (with shortest distance tour)
        /// </summary>
        public Ant BuildTours(List<Ant> antColony)
        {
            for (int i = 0; i < Graph.Dimensions; i++)
            {
                foreach (Ant ant in antColony)
                {
                    if( ant.VisitedNodes.Count ==Graph.Points.Count||ant.VisitedNodes.Contains(Graph.Points.FirstOrDefault(x => x.Id == 2)))//Тут!
                    {
                        break;
                    }
                    Edge edge = ant.Move();
                    
                    LocalUpdate(edge);
                }
            }
            antColony.RemoveAll(ant => ant.VisitedNodes.All(visitednodes=>visitednodes.Id !=2));
            GlobalUpdate();
            return antColony.OrderBy(x => x.Distance).FirstOrDefault(); // find shortest ant tour (path)
        }

        /// <summary>
        /// Update pheromone level on edge passed in parameter
        /// </summary>
        public void LocalUpdate(Edge edge)
        {
            double evaporate = (1 - Parameters.LocalEvaporationRate);
            Graph.EvaporatePheromone(edge, evaporate);

            double deposit = Parameters.LocalEvaporationRate * Parameters.T0;
            Graph.DepositPheromone(edge, deposit);
        }

        /// <summary>
        /// Update pheromone level on path for best ant
        /// </summary>
        public void GlobalUpdate()
        {
            double deltaR = 1 / GlobalBestAnt.Distance;
            foreach (Edge edge in GlobalBestAnt.Path)
            {
                double evaporate = (1 - Parameters.GlobalEvaporationRate);
                Graph.EvaporatePheromone(edge, evaporate);

                double deposit = Parameters.GlobalEvaporationRate * deltaR;
                Graph.DepositPheromone(edge, deposit);
            }
        }

        public TimeSpan GetExecutionTime()
        {
            return Stopwatch.Elapsed;
        }
    }
}