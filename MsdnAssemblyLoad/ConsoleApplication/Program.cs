using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string path =@"C:\Users\Ilia\Documents\Visual Studio 2015\Projects\MsdnAssemblyLoad\MyLibrary\bin\Debug\MyLibrary.dll";
            AppDomain domain = AppDomain.CreateDomain("mydomain");

            var res = Loader.Call(domain, path, "MyLibrary.Test", "Hello", new object[] {"Ilia"});

            Console.WriteLine("\nTap to continue...");
            Console.ReadKey(true);
        }
    }

    public class Loader : MarshalByRefObject
    {
        object CallInternal(string dll, string typename, string method, object[] parameters)
        {
            Assembly a = Assembly.LoadFile(dll);
            object o = a.CreateInstance(typename);
            Type t = o.GetType();
            MethodInfo m = t.GetMethod(method);
            return m.Invoke(o, parameters);
        }

        public static object Call(AppDomain domain, string dll, string typename, string method, params object[] parameters)
        {
            Loader ld = (Loader)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(Loader).FullName);
            object result = ld.CallInternal(dll, typename, method, parameters);
            AppDomain.Unload(domain);
            return result;
        }
    }
}
