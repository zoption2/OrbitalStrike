using System;
using Zenject;
using UnityEngine;

namespace TheGame
{
    public class Gameplayer
    {
        [Inject] private IPlayersService playersService;
        [Inject] private IShipService shipService;

        public void SetModule(int id, IModule module)
        {

        }
    }

    public interface IModule
    {
        bool IsBusy { get; }
        bool TryGetModule(int id);
        void LeaveModule();
    }
}

