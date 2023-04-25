namespace AntAlgoritm.Helpers
{
    public static class Distance
    {
        public static double Euclidean(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }
    }
}
