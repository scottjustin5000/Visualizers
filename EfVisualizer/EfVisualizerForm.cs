using System.Windows.Forms;

namespace EfVisualizer
{
    public partial class EfVisualizerForm : Form
    {
        public EfVisualizerForm()
        {
            InitializeComponent();
        }
        public void SetDebugText(string message)
        {
            sqlText.Text = message;
        }
    }
}
