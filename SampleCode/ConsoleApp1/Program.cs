using System;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var www = new AAAA();

            var zxczxczxc=www.BBBB(typeof(xxxx));
            var a = (xxxx)zxczxczxc;
            a.Do();
             
        }
    }

    public class AAAA:DispatchProxy
    {
        /// <summary>
        /// 这个Function应该是能返回一个动态劫持后的那个对象的
        /// </summary>
        /// <param name="target"></param>
        public AAAA BBBB(Type targetType)
        {
            var asd = typeof(DispatchProxy).GetMethod(nameof(DispatchProxy.Create));
            asd = asd.MakeGenericMethod(targetType, typeof(AAAA));
            var res = asd.Invoke(null, null);
            //var res1=(AAAA)Activator.CreateInstance(typeof(AAAA));
            return (AAAA)res;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("执行前");
            Console.WriteLine($"{targetMethod.Name}");
            var res = "";
            Console.WriteLine("执行后");
            return res;
        }
    }


    public interface xxxx
    {
        void Do();
    }
}
