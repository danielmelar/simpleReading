﻿using simpleReading.Models;

namespace simpleReading.ViewModel
{
    public class GroupedReadsViewModel
    {
        public Dictionary<int, Dictionary<int, List<Read>>> ReadsByYearAndMonth { get; set; }
        public Dictionary<int, string> MonthNames { get; } = new Dictionary<int, string>
        {
            {1, "Janeiro"}, {2, "Fevereiro"}, {3, "Março"}, {4, "Abril"},
            {5, "Maio"}, {6, "Junho"}, {7, "Julho"}, {8, "Agosto"},
            {9, "Setembro"}, {10, "Outubro"}, {11, "Novembro"}, {12, "Dezembro"}
        };
        public string? Username { get; set; }
    }
}
