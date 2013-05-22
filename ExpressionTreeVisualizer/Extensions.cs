using System;
using System.Reflection;
using System.Text;

namespace ExpressionTreeVisualizer
{
    public static class Extensions
    {
        public static string GetName(this Type t)
        {
            if (!t.IsGenericType)
            {
                return t.Name;
            }
            else
            {
                var args = t.GetGenericArguments();
                return string.Concat(t.Name.ExtractName(), args.ExtractGenArgs());
            }
        }
        public static string ExtractName(this string name)
        {
            int i = name.LastIndexOf("`");
            if(i>0)
            {
                name = name.Substring(0, 1);
            }
            return name;
        }
        public static string  ExtractGenArgs(this Type[] names)
        {
            var sb = new StringBuilder("<");
            foreach (var arg in names)
            {
                sb.AppendFormat("{0},",arg.GetName());
            }
            return string.Format("{0}>", sb.ToString().TrimEnd(','));
        }
        public static string GetMethodName(this MethodInfo info)
        {
            if (!info.IsGenericMethod)
            {
                return info.Name;
            }
            else
            {
                var args = info.GetGenericArguments();
                return string.Concat(info.Name, args.ExtractGenArgs());
            }
        }
    }
}
