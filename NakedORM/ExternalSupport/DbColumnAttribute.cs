using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository.ExternalSupport
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbColumnAttribute : Attribute
    {

        public DbColumnAttribute() { }

        public string ColumnName { get; set; }

        public bool Ignore { get; set; }

    }
}
