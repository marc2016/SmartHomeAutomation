using System;
using System.Linq;

using SQLite.Net.Attributes;

namespace Server.Data.Models
{
    public class Switch : ModelBase
    {
        #region Properties

        public int Channel { get; set; }
        
        public string Name { get; set; }

        public string SystemCode { get; set; }

        #endregion
    }
}