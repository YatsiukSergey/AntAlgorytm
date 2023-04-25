namespace AntAlgoritm.Helpers
{
    public sealed class RandomGenerator
    {
        public static RandomGenerator Instance { get; } = new RandomGenerator();
        public Random Random { get; set; }

        private RandomGenerator() => Random = new Random();

        public static double GetDoubleRangeRandomNumber(double minimum, double maximum)
        {
            return Instance.Random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Generate List of unique numbers
        /// </summary>
        public static List<int> GenerateRandom(int count, int min, int max)
        {
            List<int> range = Enumerable.Range(min, max).OrderBy(x => Instance.Random.Next()).Take(count).ToList();
            return range;
        }
    }
}
