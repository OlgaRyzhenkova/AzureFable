using System;
using System.Collections.Generic;

namespace AzureFable.Services
{
    internal class MazeLayout
    {
        public IReadOnlyList<string> Rows { get; }

        public MazeLayout(IReadOnlyList<string> rows)
        {
            ArgumentNullException.ThrowIfNull(rows);

            Rows = rows;
        }
    }
}
