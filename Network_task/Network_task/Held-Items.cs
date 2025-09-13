using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Network_task
{
    public class Held_Items
    {
        
        [JsonPropertyName("item")]
        public Item Item { get; set; }
    }
}
