using System;

namespace Insanity
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (InsanityGame game = new InsanityGame())
            {
                game.Run();
            }
        }
    }
#endif
}

