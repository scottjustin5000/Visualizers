using System;
using System.Text;

namespace EfVisualizer
{
    [Serializable]
    public class QueryData
    {
        public string ConnectionString { get; set; }

        public string ExceptionMessage { get; set; }

        public bool IsQuery { get; set; }

        public string SQLCommand { get; set; }

        public void SetExceptionMessage(Exception ex)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ex.Message);
            var innerException = ex.InnerException;
            while (true)
            {
                bool flag = innerException != null;
                if (!flag)
                {
                    break;
                }
                stringBuilder.AppendLine(innerException.Message);
                innerException = innerException.InnerException;
            }
            ExceptionMessage = stringBuilder.ToString();
        }

    }
}
