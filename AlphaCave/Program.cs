using AlphaCave.Core.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave
{
    public static class Program
    {
        public static void Main()
        {
            using (var game = new AlphaCaveGame())
            {
                game.Run();
            }
        }
    }
}
