using System;

namespace TowersAbstractStrategy
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AbstractStrategyGame())
                game.Run();
        }
    }
}
