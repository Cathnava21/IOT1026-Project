namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        public GelatinousCube(Location location)
        {
            _location = location;

        }

        public Location getLocation()
        {
            return _location;
        }
        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("The Gelatinous Cube achieved its mission and managed to reach you", ConsoleColor.Magenta);
            hero.Kill("You can't breathe and you die.");
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
            var heroLocation = hero.Location;
            int dx = heroLocation.Column - _location.Column;
            int dy = heroLocation.Row - _location.Row;

            Location? newLocation;
            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
            {
                newLocation = SwapLocation(map,_location,heroLocation);
                if (newLocation != null)
                {
                    // Successfully moved to the hero's location
                    _location = newLocation;
                    Activate(hero, map);
                }
            }
            else
            {
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (dx > 0)
                    {
                        var swapLocation = new Location(_location.Row, _location.Column + 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Row, _location.Column - 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                else
                {
                    if (dy > 0)
                    {
                        var swapLocation = new Location(_location.Column, _location.Row + 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Column, _location.Row - 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                //This means the gel was not able to move
                if (newLocation == null)
                {
                    ConsoleHelper.WriteLine ("You hear a frustated gurgling noise from somewhere within the catacombs",ConsoleColor.DarkGreen);
                }
            }

        }
        private Location? SwapLocation(Map map, Location currentLocation, Location newLocation)
        {
            if (map.IsOnMap(newLocation) && !map.GetRoomAtLocation(newLocation).IsActive)
            {
                map.GetRoomAtLocation(currentLocation).RemoveMonster();
                map.GetRoomAtLocation(newLocation).AddMonster(this);
                _location = newLocation;
                return newLocation;
            }
            return null;
        }
    }
}


