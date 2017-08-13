using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Entities
{
    /// <summary>
    /// Basiklasse für alle Charakter
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// Aktuelle Position des Charaters
        /// </summary>
        public Coordinate Position { get; set; }
    }
}
