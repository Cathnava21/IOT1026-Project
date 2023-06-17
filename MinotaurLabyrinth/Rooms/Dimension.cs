namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a portal room, which contains portal that can take the sword.
    /// </summary>
    public class Dimension : Room
    {
        static Dimension()
        {
            RoomFactory.Instance.Register(RoomType.Dimension, () => new Dimension());
        }

        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.Dimension;

        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Activates the portal, causing the hero to potentially lose consequences.
        /// </summary>
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                ConsoleHelper.WriteLine("You walk into the room and feel the presence of a portal to another dimension. ", ConsoleColor.Red);
                // Could update these probabilities to be based off the hero attributes
                double chanceOfSuccess = hero.HasSword ? 0.25 : 0.75;

                if (hero.HasSword)
                {
                    ConsoleHelper.WriteLine("You desperately try to run away from the portal and grab the sword tightly.", ConsoleColor.DarkMagenta);
                    if (RandomNumberGenerator.NextDouble() < chanceOfSuccess)
                    {
                        IsActive = false;
                        ConsoleHelper.WriteLine("You manage to successfully escape from the portal suddenly the portal disappears. Now run to the exit.", ConsoleColor.DarkRed);
                    }
                    else
                    {
                        ConsoleHelper.WriteLine("You manage to successfully escape from the portal but instead, you lost the sword, you will have to retrieve it again.", ConsoleColor.Green);
                        hero.HasSword = false;
                    }
                }
                else
                {
                    ConsoleHelper.WriteLine("You desperately try to run away from the portal.", ConsoleColor.DarkMagenta);
                    hero.Kill("You have fallen into the portal and died.");
                }
            }
        }

        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Red)
                            : base.Display();
        }

        /// <summary>
        /// Displays sensory information about the portal, based on the hero's distance from it.
        /// </summary>
        /// <param name="hero">The hero sensing the portal.</param>
        /// <param name="heroDistance">The distance between the hero and the portal room.</param>
        /// <returns>Returns true if a message was displayed; otherwise, false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (!IsActive)
            {
                if (base.DisplaySense(hero, heroDistance))
                {
                    return true;
                }
                if (heroDistance == 0)
                {
                    ConsoleHelper.WriteLine("You shudder as you recall your near death experience with the now defunct portal in this room.", ConsoleColor.DarkGray);
                    return true;
                }
            }
            else if (heroDistance == 1 || heroDistance == 2)
            {
                ConsoleHelper.WriteLine(heroDistance == 1 ? "You feel a stranger thing. There is a portal in a nearby room!" : "Your intuition tells you that something dangerous is nearby", ConsoleColor.DarkGray);
                return true;
            }
            return false;
        }
    }
}
