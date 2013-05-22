using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ExpressionTreeVisualizer
{
    [Serializable]
    public class Node :TreeNode
    {
        public Node(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
        public Node(object value)
        {
            Type type = value.GetType();
            Text= type.GetName();

            if (value is Expression)
            {
                ImageIndex = 2;
                SelectedImageIndex = 2;

                PropertyInfo[] propertyInfos = null;
                if (type.IsGenericType && object.ReferenceEquals(type.GetGenericTypeDefinition(), typeof(Expression<>)))
                {
                    propertyInfos = type.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                }
                else
                {
                    propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                }

                foreach (var propertyInfo in propertyInfos)
                {

                    if (propertyInfo.Name != "nodeType")
                    {
                        Nodes.Add(new Attribute(value, propertyInfo));
                    }
                }
            }
            else
            {
                ImageIndex = 4;
                SelectedImageIndex = 4;
                Text = "\"" + value.ToString() + "\"";
            }
        }
    }
}
