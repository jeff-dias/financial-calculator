namespace FinancialCalculator.StaticFiles
{
    public static class RateConstants
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        public const string GitHubRepositoryUrl = "https://github.com/jeff-dias/financial-calculator";
#pragma warning restore S1075 // URIs should not be hardcoded
        public const decimal RateBase = 0.01m;
    }

    public static class ControllerConstants
    {
        public const string GetCalculatorRoute = "calculaJuros";
        public const string GetShowMeTheCodeRoute = "showMeTheCode";
        public const string GetRateRoute = "taxaJuros";
    }
}
