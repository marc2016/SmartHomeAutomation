using System;
using System.Linq;

using SQLite.Net.Attributes;

namespace Server.Data.Models
{
    public abstract class ModelBase
    {
        #region Properties

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        #endregion
    }
}