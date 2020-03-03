using System.Collections.Generic;

namespace CustomLogic.Core.Interfaces.Models
{
    public class NgTable<T> 
    {
        public bool Success { get; set; }

        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}
