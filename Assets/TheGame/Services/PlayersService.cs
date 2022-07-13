using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TheGame
{
    public class PlayersService : IPlayersService
    {
        private readonly Dictionary<int, TempData> data = new Dictionary<int, TempData>();
        private int availableID = 0;

        public void AddPlayer(int teamID, out IIdentifiers identifiers)
        {
            var playerData = new TempData(availableID, teamID);
            data.Add(availableID, playerData);

            availableID++;
            identifiers = playerData.identifiers;
        }

        public PlayerPersonalData GetPlayerData(int id)
        {
            if (data.ContainsKey(id))
            {
                return data[id].personalData;
            }
            throw new System.ArgumentException();
        }

        public void RemovePlayer(int ID)
        {
            if (data.ContainsKey(ID))
            {
                data.Remove(ID);
            }
        }

        public void SetCameraController(int id, CameraController camera)
        {
            if (data.ContainsKey(id))
            {
                data[id].cameraController = camera;
            }
        }

        [System.Serializable]
        private class TempData
        {
            public IIdentifiers identifiers;
            public CameraController cameraController;
            public PlayerPersonalData personalData;

            public TempData(int id, int teamID)
            {
                identifiers = new Identifiers(id, teamID);
            }
        }
    }

    public interface IPlayersService
    {
        void AddPlayer(int teamID, out IIdentifiers identifiers);
        void RemovePlayer(int ID);
    }

    [System.Serializable]
    public class PlayerPersonalData
    {
        
    }
}

