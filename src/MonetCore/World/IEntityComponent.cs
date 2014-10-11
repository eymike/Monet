using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.World
{
    public interface IEntityComponent
    {
        Type ComponentType { get; }
    }
}
