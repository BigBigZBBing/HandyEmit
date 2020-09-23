using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.ExternalSupport
{
    public class DynamicInitBLL
    {
        static M_Base _m_base;

        /// <summary>
        /// 3.设置和获取，生成结果放入Base里
        /// </summary>
        public static M_Base M_Base
        {
            get
            {
                return _m_base;
            }

            set
            {
                _m_base = value;
            }
        }
        /// <summary>
        /// 初始化,实现步骤为三步
        /// </summary>
        /// <param name="m_base"></param>
        public DynamicInitBLL(M_Base m_base)
        {
            _m_base = m_base;
            EmitHelper.Create(_m_base._AssamblyName);

        }
        /// <summary>
        /// 1创建单个Class
        /// </summary>
        /// <param name="_class"></param>
        public void Exequte(M_DefineClass _class)
        {

            EmitHelper.CreateClass(_class.NsClassName);
            foreach (var prop in _class.Props)
            {
                EmitHelper.CreateMember(prop.MemberName, prop.MemberType);
            }
            _m_base.DymaticType.Add(new M_DymaticType() { TypeName = _class.NsClassName, DType = EmitHelper.SaveClass() });
        }
        /// <summary>
        /// 1创建多个Class
        /// </summary>
        /// <param name="_classes"></param>
        public void Exequte(List<M_DefineClass> _classes)
        {
            foreach (var item in _classes)
            {
                Exequte(item);
            }
        }
        /// <summary>
        /// 2保存
        /// </summary>
        public void SaveAssembly()
        {
            EmitHelper.Save();
        }


    }
}