using MinotaurLabyrinth;

namespace MinotaurLabyrinthTest
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void PitRoomTest()
        {
            //Seed the RandomNumberGenerator 
            RandomNumberGenerator.SetSeed(1);

            Pit PitRoom = new Pit();
            Hero hero = new Hero();
            Map map = new Map(1, 1);

            PitRoom.Activate(hero, map);
            Assert.AreEqual(PitRoom.IsActive, false);
            Assert.AreEqual(hero.IsAlive, true);

            hero.HasSword = true;
            PitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, true);

            Pit newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, true);

            newPitRoom.Activate(hero, map);
            newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, false);
        }
        [TestMethod]
        public void MinotaurTest()
        {
            Hero hero = new Hero();
            Minotaur minotaur = new Minotaur();
            Map map = new Map(4, 4);
            hero.HasSword = true;
            Assert.AreEqual(hero.HasSword, true);

            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(0, 2));
            Assert.AreEqual(hero.HasSword, false);


            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(0, 3));

            hero.Location = new Location(3, 1);
            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(2, 3));
        }
        [TestMethod]
        public void GelatinousMove()
        {
            // Arrange
            var heroLocation = new Location(5, 5);
            var gelLocation = new Location(6, 6);
            GelatinousCube gel = new(gelLocation);
            Hero hero = new(heroLocation);
            Map map = new Map(8, 8);

            map.GetRoomAtLocation(gelLocation).AddMonster(gel);
            gel.Move(hero, map);

            Assert.AreEqual(heroLocation, gel.getLocation());
            Assert.IsFalse(hero.IsAlive);
        }
        [TestMethod]
        public void GelMoveToActiveTest()
        {
            var heroLocation = new Location(5, 5);
            var gelLocation = new Location(1, 1);
            var hero = new Hero(heroLocation);
            var gel = new GelatinousCube(gelLocation);
            var map = new Map(4, 4);
            map.GetRoomAtLocation(gelLocation).AddMonster(gel);
            map.SetRoomAtLocation(new Location(1, 2), RoomType.Pit);

            gel.Move(hero, map);
            //The gelatinous cube can not move 
            var expectedLocation = new Location(1, 1);
            Assert.AreEqual(expectedLocation, gel.getLocation());
            gel.Move(hero, map);
            //The gelatinous cube can not move 
            expectedLocation = new Location(1, 1);
            Assert.AreEqual(expectedLocation, gel.getLocation());
        }
    }
}
