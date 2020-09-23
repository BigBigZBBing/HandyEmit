using HandyEmit.SmartEmit.Field;
using HandyEmit.SmartEmit.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 数组管理方案
    /// </summary>
    public class ArrayManager<T> : EmitBasic
    {
        private LocalBuilder stack;
        private ILGenerator il;
        private Int32 maxlen;

        internal ArrayManager(LocalBuilder stack, ILGenerator il, Int32 maxlen) : base(il)
        {
            this.stack = stack;
            this.il = il;
            this.maxlen = maxlen;
        }

        /// <summary>
        /// 推入计算堆
        /// </summary>
        public void PushIn()
        {
            this.il.Emit(OpCodes.Ldloc_S, stack);
        }

        /// <summary>
        /// 推出计算堆
        /// </summary>
        public void PushOn()
        {
            il.Emit(OpCodes.Stloc_S, stack);
        }

        #region Set
        /// <summary>
        /// 数组赋值
        /// </summary>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void Set(Int32 length, T value)
        {
            Validation(length, delegate
            {
                this.il.Emit(OpCodes.Ldloc_S, stack);
                this.il.EmitValue(length);
                this.il.EmitValue(value);
                this.il.Emit(OpCodes.Stelem_Ref);
            });
        }

        /// <summary>
        /// 数组赋值
        /// </summary>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void Set(LocalBuilder length, T value)
        {
            Validation(length, delegate
             {
                 this.il.Emit(OpCodes.Ldloc_S, stack);
                 this.il.Emit(OpCodes.Ldloc_S, length);
                 this.il.EmitValue(value);
                 this.il.Emit(OpCodes.Stelem_Ref);
             });
        }

        /// <summary>
        /// 数组赋值
        /// </summary>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void Set(Int32 length, LocalBuilder value)
        {
            Validation(length, delegate
            {
                this.il.Emit(OpCodes.Ldloc_S, stack);
                this.il.EmitValue(length);
                this.il.Emit(OpCodes.Ldloc_S, value);
                this.il.Emit(OpCodes.Stelem_Ref);
            });
        }

        /// <summary>
        /// 数组赋值
        /// </summary>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void Set(LocalBuilder length, LocalBuilder value)
        {
            Validation(length, delegate
            {
                this.il.Emit(OpCodes.Ldloc_S, stack);
                this.il.Emit(OpCodes.Ldloc_S, length);
                this.il.Emit(OpCodes.Ldloc_S, value);
                this.il.Emit(OpCodes.Stelem_Ref);
            });
        }
        #endregion

        #region Get
        /// <summary>
        /// 数组取值并推入计算堆顶部
        /// </summary>
        /// <param name="length"></param>
        public void Get(Int32 length)
        {
            Validation(length, delegate
            {
                this.il.Emit(OpCodes.Ldloc_S, stack);
                this.il.Int32Map(length);
                this.il.Emit(OpCodes.Ldelem_Ref, stack);
            });
        }

        /// <summary>
        /// 数组取值并推入计算堆顶部
        /// </summary>
        /// <param name="length"></param>
        public void Get(LocalBuilder length)
        {
            Validation(length, delegate
            {
                this.il.Emit(OpCodes.Ldloc_S, stack);
                this.il.Emit(OpCodes.Ldloc_S, length);
                this.il.Emit(OpCodes.Ldelem_Ref, stack);
            });
        }
        #endregion

        #region LinqExtension

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public CanCompute<Int32> Length()
        {
            return this.il.NewInt32(maxlen);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public FieldBoolean Contains(T value)
        {
            var res = this.il.NewBoolean();
            this.PushIn();
            this.il.EmitValue(value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._EnumerableContains);
            res.PushSt();
            return res;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public FieldBoolean Contains(LocalBuilder value)
        {
            var res = this.il.NewBoolean();
            this.PushIn();
            this.il.Emit(OpCodes.Ldloc_S, value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._EnumerableContains);
            res.PushSt();
            return res;
        }

        /// <summary>
        /// 获取索引位置
        /// </summary>
        /// <returns></returns>
        public CanCompute<Int32> IndexOf(T value)
        {
            var res = this.il.NewInt32();
            this.PushIn();
            this.il.EmitValue(value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._ArrayIndexOf);
            res.PushSt();
            return res;
        }

        /// <summary>
        /// 获取索引位置
        /// </summary>
        /// <returns></returns>
        public CanCompute<Int32> IndexOf(LocalBuilder value)
        {
            var res = this.il.NewInt32();
            this.PushIn();
            this.il.Emit(OpCodes.Ldloc_S, value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._ArrayIndexOf);
            res.PushSt();
            return res;
        }

        /// <summary>
        /// 获取索引位置(倒序索引)
        /// </summary>
        /// <returns></returns>
        public CanCompute<Int32> LastIndexOf(T value)
        {
            var res = this.il.NewInt32();
            this.PushIn();
            this.il.EmitValue(value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._ArrayLastIndexOf);
            res.PushSt();
            return res;
        }

        /// <summary>
        /// 获取索引位置(倒序索引)
        /// </summary>
        /// <returns></returns>
        public CanCompute<Int32> LastIndexOf(LocalBuilder value)
        {
            var res = this.il.NewInt32();
            this.PushIn();
            this.il.Emit(OpCodes.Ldloc_S, value);
            this.il.Emit(OpCodes.Call, BaseConst<T>._ArrayLastIndexOf);
            res.PushSt();
            return res;
        }

        #endregion

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private Boolean Validation(Int32 length, Action del)
        {
            if (length > maxlen || length < 0)
                throw new Exception("beyond index limit!");
            del?.Invoke();
            return true;
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private Boolean Validation(LocalBuilder length, Action del)
        {
            var _true = this.il.DefineLabel();
            var _false = this.il.DefineLabel();
            this.il.Emit(OpCodes.Ldloc_S, length);
            this.il.Emit(OpCodes.Ldc_I4, maxlen);
            this.il.Emit(OpCodes.Bgt_S, _true);
            this.il.Emit(OpCodes.Ldloc_S, length);
            this.il.Emit(OpCodes.Ldc_I4_0);
            this.il.Emit(OpCodes.Ble_S, _true);
            this.il.MarkLabel(_true);
            this.il.Emit(OpCodes.Ldstr, "beyond index limit!");
            this.il.Emit(OpCodes.Newobj, typeof(Exception).GetConstructor(new Type[] { typeof(string) }));
            this.il.Emit(OpCodes.Throw);
            this.il.MarkLabel(_false);
            del?.Invoke();
            return true;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="field"></param>
        public static implicit operator LocalBuilder(ArrayManager<T> field) => field.stack;
    }
}
