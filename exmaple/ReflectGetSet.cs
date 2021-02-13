using BenchmarkDotNet.Attributes;
using ILWheatBread;
using ILWheatBread.SmartEmit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace exmaple
{
    public class ReflectGetSet
    {
        [Benchmark(Description = "ReflectSet")]
        public void ReflectSet()
        {
            for (int i = 0; i < 5000; i++)
            {
                Model model = new Model();

                typeof(Model).GetProperty("Id").GetSetMethod().Invoke(model, new object[] { 9999 });
                typeof(Model).GetProperty("Name").GetSetMethod().Invoke(model, new object[] { "张炳彬" });
                typeof(Model).GetProperty("Old").GetSetMethod().Invoke(model, new object[] { 999 });
                typeof(Model).GetProperty("LoginTime").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
                typeof(Model).GetProperty("LoginTime1").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
                typeof(Model).GetProperty("LoginTime2").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
                typeof(Model).GetProperty("LoginTime3").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
                typeof(Model).GetProperty("LoginTime4").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
                typeof(Model).GetProperty("LoginTime5").GetSetMethod().Invoke(model, new object[] { DateTime.Now });
            }

        }

        static Action<Model> deleg;

        [Benchmark(Description = "EmitSet")]
        public void EmitSet()
        {
            for (int i = 0; i < 5000; i++)
            {
                Model model = new Model();

                if (deleg == null)
                    deleg = SmartBuilder.DynamicMethod<Action<Model>>(string.Empty, builder =>
                    {
                        var value = builder.ArgumentRef<Model>(0);
                        var entity = builder.NewEntity<Model>(value);
                        entity.SetValue("Id", builder.NewInt64(9999));
                        entity.SetValue("Name", builder.NewString("张炳彬"));
                        entity.SetValue("Old", builder.NewInt32(999));
                        entity.SetValue("LoginTime", builder.NewDateTime());
                        entity.SetValue("LoginTime1", builder.NewDateTime());
                        entity.SetValue("LoginTime2", builder.NewDateTime());
                        entity.SetValue("LoginTime3", builder.NewDateTime());
                        entity.SetValue("LoginTime4", builder.NewDateTime());
                        entity.SetValue("LoginTime5", builder.NewDateTime());
                        builder.Return();
                    });

                deleg.Invoke(model);
            }
        }
    }
}
