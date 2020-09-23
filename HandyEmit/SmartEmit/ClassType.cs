using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 常用修饰符(便捷)
    /// </summary>
    public enum ClassType
    {
        公共 = TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        公共抽象 = TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        公共静态 = TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        内部 = TypeAttributes.NotPublic | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        内部抽象 = TypeAttributes.NotPublic | TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        内部静态 = TypeAttributes.NotPublic | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit,
        公共接口 = TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.AnsiClass,
        内部接口 = TypeAttributes.NotPublic | TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.AnsiClass,
        结构体 = TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.SequentialLayout | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit
    }
}
