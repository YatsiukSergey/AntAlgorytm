namespace AntAlgoritm.Graph
{
    public class Edge
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public double Length { get; set; }
        public double Pheromone { get; set; }
        public double Weight { get; set; }

        public Edge() { }

        public Edge(Point start, Point end)
        {
           // Math.Round(Start.DistanceTo(End));
            Start = start;
            End = end;
            Length = Start.DistanceTo(End);
        }
    }
}
