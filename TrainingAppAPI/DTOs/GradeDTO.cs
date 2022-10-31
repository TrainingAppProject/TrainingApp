using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingAppAPI.DTOs
{
    public class GradeDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public Guid GradeID { get; set; }

        public IEnumerable<GradeElementDTO> Elements { get; set; }
    }
}

