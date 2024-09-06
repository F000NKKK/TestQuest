using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Parsers
{
    internal interface IParser<T>
    {
        IEnumerable<T> Parse(string filePath);
    }
}
