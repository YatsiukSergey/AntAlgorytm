namespace AntAlgoritm.ACS
{
    public class Parameters
    {
        /// <summary>
        /// Relative importance of distance(default=2)
        /// </summary>
        public int Beta { get; set; }

        /// <summary>
        /// Global evaporation rate of pheromone(0..1, default=0.1)
        /// </summary>
        public double GlobalEvaporationRate { get; set; }

        /// <summary>
        /// Local evaporation rate of pheromone(0..1, default=0.01)
        /// </summary>
        public double LocalEvaporationRate { get; set; }

        /// <summary>
        /// Probability of choosing best ant path instead of random roulette
        /// </summary>
        public double Q0 { get; set; }

        /// <summary>
        /// Initial pheromone level along each Edge
        /// </summary>
        public double T0 { get; set; }

        /// <summary>
        /// Define how many ants will be used
        /// </summary>
        public int AntCount { get; set; }

        /// <summary>
        /// Number of iterations to perform (default=2500)
        /// </summary>
        public int Iterations { get; set; }

        public Parameters(int beta, double globalEvaporationRate, double localEvaporationRate, double t0, double q0,
            int antCount, int iterations)
        {
            Beta = beta;
            GlobalEvaporationRate = globalEvaporationRate;
            LocalEvaporationRate = localEvaporationRate;
            T0 = t0;
            Q0 = q0;
            AntCount = antCount;
            Iterations = iterations;
        }

        /// <summary>
        /// Default parameters
        /// </summary>
        public Parameters()
        {
            Beta = 2;
            GlobalEvaporationRate = 0.1;
            LocalEvaporationRate = 0.01;
            Q0 = 0.9;
            AntCount = 20;
            Iterations = 1000;
            T0 = 0.01;
        }

        public void Show()
        {
            Console.WriteLine("Beta: " + Beta);
            Console.WriteLine("Global Evaporation Rate: " + GlobalEvaporationRate);
            Console.WriteLine("Local Evaporation Rate: " + LocalEvaporationRate);
            Console.WriteLine("Q0: " + Q0);
            Console.WriteLine("AntCount: " + AntCount);
            Console.WriteLine("Iterations: " + Iterations);
            Console.WriteLine("T0: " + T0);
            Console.WriteLine();
        }
    }
}
