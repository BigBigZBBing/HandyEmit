using System;
using System.Collections.Generic;

namespace Core.Repository.ExternalSupport
{
    public class M_Base
    {
        public string _AssamblyName { get; set; }
        public List<M_DymaticType> DymaticType { get; set; } = new List<M_DymaticType>();
    }
    public class M_DymaticType
    {
        public string TypeName { get; set; }
        public Type DType { get; set; }
    }
    public class M_DefineClass
    {
        public string NsClassName { get; set; }
        public IEnumerable<M_ClassMember> Props { get; set; }
    }
    public class M_ClassMember
    {
        public string MemberName { get; set; }
        public Type MemberType { get; set; }
    }

}