using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Network_task
{
    public class Stats
    {
        [JsonPropertyName("base_stat")]
        public int Base_Stats {  get; set; }
        [JsonPropertyName("effort")]
        public int Effort {  get; set; }
        [JsonPropertyName("stat")]
        public Stat Stat_Stats { get; set; }
    }
}
