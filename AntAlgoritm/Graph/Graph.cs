using AntColonySystem;

namespace AntAlgoritm.Graph;

public class Graph
{
    public Graph(List<Point> points, bool isSymetric)
    {
        Edges = new Dictionary<int, Edge>();
        Points = points;
        Dimensions = points.Count;
        IsSymetric = isSymetric;
        CreateEdges();
    }

    public List<Point> Points { get; set; }
    public Dictionary<int, Edge> Edges { get; set; }
    public double Dimensions { get; set; }
    public double MinimumPheromone { get; set; }
    private bool IsSymetric { get; set; }

    /// <summary>
    /// Create edges between all points. 
    /// NOTE: For every two points there is two edges between them in case of asymetric problem (1 -> 2, 2 -> 1).
    /// </summary>
    private void CreateEdges()
    {
        double[,] matrix = new double[Points.Count, Points.Count];
        for (int i = 0; i < Points.Count; i++)
        {
            for (int j = 0; j < Points.Count; j++)
            {
                if (i != j)
                {
                    Edge edge = new Edge(Points[i], Points[j]);
                    Edges.Add(Helper.HashFunction(Points[i].Id, Points[j].Id), edge);
                    matrix[i, j] = edge.Length;
                }
                if (i == j)
                {
                    matrix[i, j] = 0;
                }

            }
        }
        for (int i = 0; i < Points.Count; i++)
        {
            for (int j = 0; j < Points.Count; j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Return edge beetwen two points (their ID's) from Dictionary
    /// </summary>
    public Edge GetEdge(int firstPointId, int secondPointId)
    {
        return Edges[Helper.HashFunction(firstPointId, secondPointId)];
    }

    /// <summary>
    /// Set specific pheromone to all edges
    /// </summary>
        
    public void ResetPheromone(double pheromoneValue)
    {
        foreach (var edge in Edges)
        {
            edge.Value.Pheromone = pheromoneValue;
        }
    }

    public void EvaporatePheromone(Edge edge, double value)
    {
        edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value); // Math.Max is here to prevent Pheromon = 0

        if (IsSymetric)
        {
            var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
            secondEdge.Pheromone = Math.Max(MinimumPheromone, secondEdge.Pheromone * value);
        }
    }

    public void DepositPheromone(Edge edge, double value)
    {
        edge.Pheromone += value;

        if (IsSymetric)
        {
            var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
            secondEdge.Pheromone += value;
        }
    }
}