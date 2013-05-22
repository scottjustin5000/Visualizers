using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace EfVisualizer
{
    public class EfContextVisualizerObjectSource : VisualizerObjectSource
    {

        public override void GetData(object target, Stream outgoingData)
        {
            QueryData sqlString = null;
            try
            {
                if (target as IQueryable == null)
                {
                    if (target as DbContext != null)
                    {
                        sqlString = (target as DbContext).ToSqlString();
                    }
                }
                else
                {
                    sqlString = (target as IQueryable).ToSqlString();
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                sqlString = new QueryData();
                sqlString.SetExceptionMessage(exception);
            }
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(outgoingData, sqlString);
        }
    }
}
