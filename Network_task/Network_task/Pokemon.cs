using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Network_task
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("species")]
        public Species Species { get; set; }
        [JsonPropertyName("stats")]
        public List<Stats> StatsPokemon { get; set; }
        [JsonPropertyName("held_items")]
        public List<Held_Items> HeldItems { get; set; }
    }
}
