using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository.ExternalSupport
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    internal sealed class DbResultAttribute : Attribute
    {

        public DbResultAttribute()
            : this(true) { }

        public DbResultAttribute(bool defaultAsDbColumn)
        {
            DefaultAsDbColumn = defaultAsDbColumn;
        }

        public bool DefaultAsDbColumn { get; private set; }

    }
}
