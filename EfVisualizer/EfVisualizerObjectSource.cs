using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace EfVisualizer
{
    public class EfVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            QueryData sqlString = null;
            try
            {
                if (target as ObjectQuery == null)
                {
                    if (target as ObjectContext != null)
                    {
                        sqlString = (target as ObjectContext).ToSqlString();
                    }
                }
                else
                {
                    sqlString = (target as ObjectQuery).ToSqlString();
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                sqlString = new QueryData();
                sqlString.SetExceptionMessage(exception);
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(outgoingData, sqlString);
        }
    }
}
