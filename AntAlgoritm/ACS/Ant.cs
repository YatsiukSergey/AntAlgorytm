using AntAlgoritm.Graph;
using AntAlgoritm.Helpers;
using AntColonySystem;

namespace AntAlgoritm.ACS
{
    public class Ant
    {
        #region Properties

        public Graph.Graph Graph { get; set; }
        public int Beta { get; set; }
        public double Q0 { get; set; }
        public int StartNodeId { get; set; }
        public double Distance { get; set; }
        public List<Point> VisitedNodes { get; set; }
        public List<Point> UnvisitedNodes { get; set; }
        public List<Edge> Path { get; set; }
        public int LimitedDistance { get; set; }

        public Point CurrentNode
        {
            get
            {
                return VisitedNodes[^1];
            }
        }
        #endregion

        public Ant(Graph.Graph graph, int beta, double q0, int startNodeId,int limitedDistance)
        {
            Graph = graph;
            Beta = beta;
            Q0 = q0;
            StartNodeId = startNodeId;
            VisitedNodes = new List<Point>();
            UnvisitedNodes = new List<Point>();
            Path = new List<Edge>();
            Distance = 0;
            LimitedDistance = limitedDistance;

            var startNode = Graph.Points.FirstOrDefault(x => x.Id == StartNodeId);
            VisitedNodes.Add(startNode);
            UnvisitedNodes = Graph.Points.Where(x => x.Id != StartNodeId).ToList();
            Path.Clear();
        }

        // public void Init(int startNodeId)
        // {
        //     StartNodeId = 1;
        //     Distance = 0;
        //     VisitedNodes.Add(Graph.Points.Where(x => x.Id == startNodeId).First());
        //     UnvisitedNodes = Graph.Points.Where(x => x.Id != startNodeId && x.Id != 7).ToList();
        //    // UnvisitedNodes.Add(Graph.Points.Where(x => x.Id == 7).Last());
        //
        //     Path.Clear();
        // }

        // public Point CurrentNode()
        // {
        //     //return VisitedNodes[VisitedNodes.Count - 1];
        //     return VisitedNodes[^1];
        // }

        public bool CanMove()
        {
            return VisitedNodes.Count != Path.Count;
        }

        public Edge Move()
        {
            Point nextPoint;
            var currentPoint = CurrentNode;
            Point destinationPoint = Graph.Points.FirstOrDefault(x => x.Id == 2);



            if (UnvisitedNodes.Count == 1 /*|| Graph.GetEdge(currentPoint.Id, destinationPoint.Id).Length < 20*/)
            {
                VisitedNodes.Add(destinationPoint);
                nextPoint = destinationPoint;
                var edge = Graph.GetEdge(currentPoint.Id, nextPoint.Id);
                Path.Add(edge);
                Distance += edge.Length;
                return edge;
            }
            else
            {
                UnvisitedNodes.RemoveAt(UnvisitedNodes.FindIndex(x => x.Id == 2));
                nextPoint = ChooseNextPoint();
                if (Distance + Graph.GetEdge(currentPoint.Id, nextPoint.Id).Length +
                    Graph.GetEdge(nextPoint.Id, destinationPoint.Id).Length < LimitedDistance)
                {
                    VisitedNodes.Add(nextPoint);
                    UnvisitedNodes.RemoveAt(UnvisitedNodes.FindIndex(x => x.Id == nextPoint.Id));
                    var edge2 = Graph.GetEdge(currentPoint.Id, nextPoint.Id);
                    Path.Add(edge2);
                    Distance += edge2.Length;
                    UnvisitedNodes.Add(Graph.Points.FirstOrDefault(x => x.Id == 2));
                    return edge2;
                }

               
            }
            VisitedNodes.Add(destinationPoint);
            nextPoint = destinationPoint;
            var edge3 = Graph.GetEdge(currentPoint.Id, nextPoint.Id);
            Path.Add(edge3);
            Distance += edge3.Length;
            UnvisitedNodes.Clear();
            return edge3;

        }


        private Point ChooseNextPoint()
        {
            List<Edge> edgesWithWeight = new List<Edge>();
            Edge bestEdge = new Edge();
            int currentNodeId = CurrentNode.Id;

            foreach (var node in UnvisitedNodes)
            {
                var edge = Graph.GetEdge(currentNodeId, node.Id);
                edge.Weight = Weight(edge);

                if (edge.Weight > bestEdge.Weight)
                {
                    bestEdge = edge;
                }

                edgesWithWeight.Add(edge);
            }

            var random = RandomGenerator.Instance.Random.NextDouble();
            if (random < Q0)
            {
                return Exploitation(bestEdge);
            }

            return Exploration(edgesWithWeight);
        }

        private double Weight(Edge edge)
        {
            double heuristic = 1 / edge.Length;
            return edge.Pheromone * Helper.Pow(heuristic, Beta);
        }

        private Point Exploitation(Edge bestEdge)
        {
            return bestEdge.End;
        }

        private Point Exploration(List<Edge> edgesWithWeight)
        {
            double totalSum = edgesWithWeight.Sum(x => x.Weight);
            var edgeProbabilities = edgesWithWeight.Select(w =>
            {
                w.Weight = (w.Weight / totalSum);
                return w;
            }).ToList();
            var cumSum = Helper.EdgeCumulativeSum(edgeProbabilities);
            Point choosenPoint = Helper.GetRandomEdge(cumSum);

            return choosenPoint;
        }
    }
}