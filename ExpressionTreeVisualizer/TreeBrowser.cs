using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressionTreeVisualizer
{
    public class TreeBrowser :TreeView
    {
        public void Add(Node node)
        {
            Nodes.Add(node);
        }
    }
}
