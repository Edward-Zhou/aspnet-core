using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public class AreaAddress: Entity
    {
        public int Sorting { get; set; }
        public string AreaName { get; set; }
        public int? ParentId { get; set; }
        public AreaAddress Parent { get; set; }
        public int Deep { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}
