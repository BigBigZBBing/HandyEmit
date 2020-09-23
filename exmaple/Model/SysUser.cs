using NakedORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using System.Text;

namespace exmaple.Model
{
    [Table("SysUser")]
    public class SysUser
    {
        [Key]
        [PagerKey]
        public Int64? Id { get; set; }

        public String Name { get; set; }

        public Int32? Old { get; set; }

        //public DateTime? Today { get; set; }

        //public Boolean? Married { get; set; }

        //public TimeSpan? Bron { get; set; }

        //public Decimal? Income { get; set; }

        //public Double? Childrens { get; set; }

        //public Single? _Single { get; set; }

        //public Char[] Chars { get; set; }

        //public Byte[] Bytes { get; set; }

    }
}
