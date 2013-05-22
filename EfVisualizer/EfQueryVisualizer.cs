using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace EfVisualizer
{
    public class EfQueryVisualizer : DialogDebuggerVisualizer
    {
        override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {

            if (windowService != null)
            {
                if (objectProvider != null)
                {
                    var obj = objectProvider.GetObject() as QueryData;
                    //  flag1 = obj == null;
                    if (obj != null)
                    {
                        var form = new EfVisualizerForm();
                        if (!string.IsNullOrEmpty(obj.ExceptionMessage))
                        {
                            form.SetDebugText(string.Format("Error:{0}{1}", Environment.NewLine, obj.ExceptionMessage));
                        }
                        else
                        {
                            form.SetDebugText(obj.SQLCommand);
                        }
                        DialogResult dialogResult = windowService.ShowDialog(form);


                    }
                }
                else
                {
                    throw new ArgumentNullException("objectProvider");
                }
            }
            else
            {
                throw new ArgumentNullException("windowService");
            }
        }
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerDevelopmentHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(EfQueryVisualizer), typeof(EfVisualizerObjectSource), true);
            visualizerDevelopmentHost.ShowVisualizer();
        }
    }
}
