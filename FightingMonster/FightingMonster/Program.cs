using System;
using FightingMonter;

namespace FightingMonster
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FightingMonsterGame game = new FightingMonsterGame())
            {
                game.Run();
            }
        }
    }
#endif
}

