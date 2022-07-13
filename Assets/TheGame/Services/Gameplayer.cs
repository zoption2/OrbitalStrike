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
        void TryGetModule(int id, Action onSuccess, Action onFail);
        void LeaveModule();
    }
}

