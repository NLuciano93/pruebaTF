using System;

namespace Example.Api
{
    public class EarthForecast : Forecast
    {
        public double TemperatureF => 32 + Convert.ToDouble(TemperatureC) / 0.5556;

        public int Count { get; set; } = 1;

        public string Summary { get; set; } = default!;

        public string? OptionalSummary { get; set; }
    }

    /// <example>
    /// teste!
    /// </example>
    public abstract class Forecast
    {
        public DateTime Date { get; set; }

        /// <summary>
        /// The temperature measured in Celsius
        /// </summary>
        public decimal TemperatureC { get; set; }

        public WindDirection WindDirection { get; set; }
    }

    public class MarsForecast : Forecast
    {
        public double SandStormProbability { get; set; } = 0.8;
    }

    public enum WindDirection
    {
        North,
        South,
        East,
        West
    }
}
