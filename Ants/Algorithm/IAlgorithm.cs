using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    public interface IAlgorithm
    {
        OutputAlgorithm Execute();
        bool IsFinished();
    }
}
