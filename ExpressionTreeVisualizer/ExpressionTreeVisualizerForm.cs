using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows.Forms;

namespace ExpressionTreeVisualizer
{
    public partial class ExpressionTreeVisualizerForm : Form
    {
        public ExpressionTreeVisualizerForm()
        {
            InitializeComponent();
        }
         public void Init(string ex, EtContainer container)
         {
             textBox1.Text = string.IsNullOrEmpty(ex) ? ex : "";
           treeBrowser1.Add(container.TreeNode);
             treeBrowser1.ExpandAll();
         }
    }

}
