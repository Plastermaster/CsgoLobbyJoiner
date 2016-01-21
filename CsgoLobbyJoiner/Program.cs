using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;

using Steam4NET;
using Newtonsoft.Json;

namespace CsgoLobbyJoiner
{
    class Program
    {
        static int Main(string[] args)
        {
            if (Steamworks.Load(true))
            {
                Console.WriteLine("Found Steam");
            }
            else
            {
                Console.WriteLine("Failed");
                return -1;
            }

            ISteam006 steam006 = Steamworks.CreateSteamInterface<ISteam006>();
            if (steam006 == null)
            {
                Console.WriteLine("steam006 is null !");
                return -1;
            }

            ISteamClient012 steamclient = Steamworks.CreateInterface<ISteamClient012>();
            if (steamclient == null)
            {
                Console.WriteLine("steamclient is null !");
                return -1;
            }

            IClientEngine clientengine = Steamworks.CreateInterface<IClientEngine>();
            if (clientengine == null)
            {
                Console.WriteLine("clientengine is null !");
                return -1;
            }

            int pipe = steamclient.CreateSteamPipe();
            if (pipe == 0)
            {
                Console.WriteLine("Failed to create a pipe");
                return -1;
            }

            int user = steamclient.ConnectToGlobalUser(pipe);
            if (user == 0 || user == -1)
            {
                Console.WriteLine("Failed to connect to global user");
                return -1;
            }

            var cuser = clientengine.GetIClientUser<IClientUser>(user, pipe);


            Console.WriteLine($"Your id: {cuser.GetSteamID()}");

            Console.WriteLine("(right click the 'join' button on your Steam profile and copy the URL, then ONLY paste then number here)");
            Console.WriteLine("Enter LobbyID  :");
            long lobbyID = Convert.ToInt64(Console.ReadLine());
            
            
            Console.WriteLine($"Your set lobby is: {lobbyID}. Starting search! ");
            for (;;)
            {
                Console.WriteLine($"Joining {lobbyID}, hit a key to join the next");

                System.Diagnostics.Process.Start($"steam://joinlobby/730/{lobbyID++}/{cuser.GetSteamID().ConvertToUint64()}");

                Console.ReadKey();
            }
        }
    }
}
