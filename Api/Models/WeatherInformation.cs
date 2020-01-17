using System;

namespace Api.Models
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public string Country { get; set; }
    }
}
