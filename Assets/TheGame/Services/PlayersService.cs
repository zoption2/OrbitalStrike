using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace TheGame
{
    public class PlayersService : IPlayersService
    {
        [Inject] private IPlayerFactory playerFactory;
        [Inject] private IControlFactory controlFactory;

        private readonly Dictionary<int, Player> data = new Dictionary<int, Player>();
        private int availableID = 0;
        private int currentPlayers = 0;
        private int expectedPlayers = 1;
        private int playerTeams = 1;

        public void Initialize(int totalPlayers, int playerTeams)
        {
            expectedPlayers = totalPlayers;
            this.playerTeams = playerTeams;
        }

        public void AddPlayer(int teamID, out IIdentifiers identifiers)
        {
            identifiers = new Identifiers(availableID, teamID);
            var player = playerFactory.Get(identifiers);
            data.Add(availableID, player);
            currentPlayers++;
            availableID++;
            player.control = controlFactory.Get(identifiers);
            player.cameraController.Init(null, expectedPlayers, currentPlayers);
        }

        public Player GetData(int id)
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

        public void SetCameraController(int id, CameraController camera)
        {
            if (data.ContainsKey(id))
            {
                data[id].cameraController = camera;
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
        void AddPlayer(int teamID, out IIdentifiers identifiers);
        Player GetData(int id);
        void RemovePlayer(int ID);
        void Clear();
    }

    [System.Serializable]
    public class PlayerPersonalData
    {
        
    }
}

