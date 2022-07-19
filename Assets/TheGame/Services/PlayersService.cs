using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace TheGame
{
    public class PlayersService : IPlayersService
    {
        [Inject] private IPlayerFactory playerFactory;
        

        private readonly Dictionary<int, IPlayer> data = new Dictionary<int, IPlayer>();
        private int availableID = 0;
        private int currentPlayers = 1;
        private int expectedPlayers = 1;
        private int playerTeams = 1;

        public void Initialize(int totalPlayers, int playerTeams)
        {
            expectedPlayers = totalPlayers;
            this.playerTeams = playerTeams;
        }

        public IPlayer AddPlayer(int teamID, out IIdentifiers identifiers)
        {
            identifiers = new Identifiers(availableID, teamID);
            var player = playerFactory.Get(identifiers, expectedPlayers, currentPlayers);
            data.Add(availableID, player);
            currentPlayers++;
            availableID++;
            return player;
        }

        public IPlayer GetData(int id)
        {
            if (data.ContainsKey(id))
            {
                return data[id];
            }
            throw new System.ArgumentException();
        }

        public void RemovePlayer(int ID)
        {
            if (data.ContainsKey(ID))
            {
                data.Remove(ID);
                currentPlayers--;
            }
        }

        public void Clear()
        {
            //save players progress
            //clear data
        }

    }

    public interface IPlayersService
    {
        void Initialize(int totalPlayers, int playerTeams);
        IPlayer AddPlayer(int teamID, out IIdentifiers identifiers);
        IPlayer GetData(int id);
        void RemovePlayer(int ID);
        void Clear();
    }

    [System.Serializable]
    public class PlayerPersonalData
    {
        
    }
}

