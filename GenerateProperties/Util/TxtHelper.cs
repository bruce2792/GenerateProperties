using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Util
{
    public class TxtHelper
    {
        public static List<string> GetTxtFileString(string filePath)
        {
            List<string> ret = new List<string>();
            try
            {

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ret.Add(line);
                    }
                }
            }
            catch (IOException e)
            {
                
            }
            return ret;
        }

    }
}
