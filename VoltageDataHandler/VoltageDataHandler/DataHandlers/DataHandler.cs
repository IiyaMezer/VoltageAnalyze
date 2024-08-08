using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.DataHandlers
{
    public class DataHandler
    {

        public Func<List<string>, List<string>> DeleteHeader = data => data.Skip(4).ToList();

    }
}
