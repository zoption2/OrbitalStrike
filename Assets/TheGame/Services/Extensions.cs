namespace TheGame
{
    public static class Extensions
    {
        public static void ChangeModule(this IModule current, IModule changeTo, IPlayer player)
        {
            if (changeTo.IsAvailable())
            {
                current?.LeaveModule(player);
                changeTo.JoinModule(player);
                player.Module = changeTo;
                player.CameraControl.ChangeCameraTarget(changeTo.transform, changeTo.CameraPrefs);
            }
        }
    }
}

