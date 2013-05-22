using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ExpressionTreeVisualizer
{
    [Serializable]
   public class Attribute :TreeNode
    {
       public Attribute(SerializationInfo info, StreamingContext context)
           : base(info, context)
       {
       }

       public Attribute(object attribute, PropertyInfo propertyInfo)
       {
           Text = string.Concat(propertyInfo.Name, " : ", propertyInfo.PropertyType.GetName());
           ImageIndex = 3;
           SelectedImageIndex = 3;
           var value = propertyInfo.GetValue(attribute, null);
           if (value != null)
           {
               if (value.GetType().IsGenericType &&
                   ReferenceEquals(value.GetType().GetGenericTypeDefinition(), typeof (ReadOnlyCollection<>)))
               {
                   

                   if ((int)value.GetType().InvokeMember("get_Count", BindingFlags.InvokeMethod, null, value, null) == 0)
                   {
                       Text += " : Empty";
                   }
                   else
                   {
                       foreach (var tree in ((IEnumerable)value).Cast<object>())
                       {
                           if (tree is Expression)
                           {
                               Nodes.Add(new Node(tree));
                           }
                           else if (tree is MemberAssignment)
                           {
                               Nodes.Add(new Node(((MemberAssignment) tree).Expression));
                           }
                       }
                   }
               }
               else if (value is Expression)
               {
                   Text += ((Expression) value).NodeType;
                   Nodes.Add(new Node(value));
               }
               else if (value is MethodInfo)
               {
                   Text += " : \"" + ((MethodInfo) value).GetMethodName() + "\"";
               }
               else if (value is Type)
               {
                   Text += " : \"" + ((Type) value).GetName() + "\"";
               }
               else
               {
                   Text += " : \"" + value.ToString() + "\"";
               }
           }
           else
           {
               Text += " : Nothing";
           }
       }
    }
}
