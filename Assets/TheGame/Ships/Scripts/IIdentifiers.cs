namespace TheGame
{
    public interface IIdentifiers
    {
        int ID { get; }
        int TeamID { get; }
        PlayerType PlayerType { get; }
    }

    public struct Identifiers : IIdentifiers
    {
        public int ID { get; private set; }
        public int TeamID { get; private set; }
        public PlayerType PlayerType { get; private set; }

        public Identifiers(int id, int teamID, PlayerType playerType = PlayerType.human)
        {
            ID = id;
            TeamID = teamID;
            PlayerType = playerType;
        }
    }
}

