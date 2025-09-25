using System.Text;

namespace Code.Common.Helpers
{
    public class StringUpdater
    {
        static readonly StringBuilder StringBuilder = new StringBuilder();
        
        public static string UpdateString(string newString)
        {
            StringBuilder.Clear();
            StringBuilder.AppendLine(newString);
            return StringBuilder.ToString();
        }
    }

}
