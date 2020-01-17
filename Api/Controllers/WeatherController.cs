using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Countries = new[]
        {
            "France", "Japan", "Germany", "Italy", "Spain"
        };

        [HttpGet("infos")]
        public ICollection<Weather> GetWeathers()
        {
            return Enumerable.Range(1, 5).Select(index => new Weather
            {
                Date = GetRandomDate(),
                Country = GetRandomCountry(),
                Temperature = GetRandomTemperature(),
            }).ToArray();
        }

        private static DateTime GetRandomDate()
        {
            var index = RandomNumberGenerator.GetInt32(-100, 100);
            return DateTime.Now.AddDays(index);
        }

        private static string GetRandomCountry()
        {
            var index = RandomNumberGenerator.GetInt32(0, Countries.Length);
            return Countries[index];
        }

        private static int GetRandomTemperature()
        {
            return RandomNumberGenerator.GetInt32(-30, 30);
        }
    }
}
