namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        public GelatinousCube (Location location){
            _location = location;
            
        }

        public Location getLocation() {
            return _location;
        }
        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("You have encountered the minotaur! He charges at you and knocks you into another room.", ConsoleColor.Magenta);
        }


        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            return false;
        }


        public override DisplayDetails Display()
        {
            return new DisplayDetails("[C]", ConsoleColor.Green);
        }

        public void Move(Hero hero, Map map)
        {   
            map.GetRoomAtLocation(_location).RemoveMonster();
            _location = new Location (_location.Row +1, _location.Column);
            map.GetRoomAtLocation(_location).AddMonster(this);
        }
    }
}
