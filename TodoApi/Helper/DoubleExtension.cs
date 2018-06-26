namespace TodoApi.Helper
{
    public static class DoubleExtension
    {
        public static double Round(this double input, int digit)
        {
            double outI;
            outI = System.Math.Round(input, digit);
            return outI;
        }
    }
}