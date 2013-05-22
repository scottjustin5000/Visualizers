using System;

namespace ExpressionTreeVisualizer
{
    [Serializable]
    public class EtContainer
    {
        public Node TreeNode { get; set; }
        public string Expression { get; set; }

        public EtContainer(Node node, string expression)
        {
           TreeNode = node;
           Expression = expression;
        }
    }
}
