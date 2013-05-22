using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace EfVisualizer
{
       public class EfContextVisualizer:DialogDebuggerVisualizer
    {

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService != null)
            {
                if (objectProvider != null)
                {
                    var obj = objectProvider.GetObject() as QueryData;
         
                    if (obj !=null)
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
                        bool flag;
                        if (!objectProvider.IsObjectReplaceable)
							{
								flag = true;
							}
							else
							{
								flag = dialogResult != DialogResult.OK;
							}
							if (!flag)
							{
								objectProvider.ReplaceObject(obj);
							}
				
                     
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
            VisualizerDevelopmentHost visualizerDevelopmentHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(EfContextVisualizer), typeof(EfContextVisualizerObjectSource), true);
            visualizerDevelopmentHost.ShowVisualizer();
        }
    }
}

