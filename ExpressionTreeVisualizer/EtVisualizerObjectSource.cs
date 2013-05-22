using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ExpressionTreeVisualizer
{
    public class EtVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            var expression = target as Expression;
            var node = new Node(expression);
            var container = new EtContainer(node, expression.ToString());
            Serialize(outgoingData,container);
       
        }
    }
}
