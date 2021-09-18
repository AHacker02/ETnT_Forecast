using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DbSets
{
    public abstract class Lookup:BaseEntity
    {
        public string Value { get; set; }
    }
}
