using System;
using Zenject;

namespace TheGame
{
    public class Gameplay
    {
        [Inject] private IPlayersService playersService;
        [Inject] private IShipService shipService;
        [Inject] private IGameSettings settings;

        private int playerTeams = 1;

        public void CreatePlayers()
        {
            playersService.Initialize(settings.TotalPlayers, 1);
            for (int i = 0, j = settings.TotalPlayers; i < j; i++)
            {
                playersService.AddPlayer(0, out IIdentifiers identifiers);
            }
        }

        public void SetModule(int id, IModule module)
        {

        }
    }
}

