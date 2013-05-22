using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ExpressionTreeVisualizer
{
    public class EtVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService != null)
            {
                var container =
                    objectProvider.GetObject() as EtContainer;
                if (container != null)
                {
                    var form = new ExpressionTreeVisualizerForm();
                    form.Init(container.Expression, container);
                    DialogResult dialogResult = windowService.ShowDialog(form);
                }
                else
                {
                    throw new ApplicationException("invalid objectProvider");
                }
            }
            else
            {
                throw new ApplicationException("This debugger does not support modal visualizers");
            }


        }
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerDevelopmentHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(EtVisualizer), typeof(EtVisualizerObjectSource), true);
            visualizerDevelopmentHost.ShowVisualizer();
        }
    }
}
