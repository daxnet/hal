namespace Hal.Example.Models
{
    public class MeetingRoom
    {
        #region Public Fields

        public static readonly IEnumerable<MeetingRoom> FakeRooms = new List<MeetingRoom>
        {
            new() { ID = 1, Name = "Mercury", Seats = 10},
            new() { ID = 2, Name = "Venus", Seats = 8},
            new() { ID = 3, Name = "Earth", Seats = 15},
            new() { ID = 4, Name = "Mars", Seats = 14},
            new() { ID = 5, Name = "Jupiter", Seats = 30},
            new() { ID = 6, Name = "Saturn", Seats = 25},
            new() { ID = 7, Name = "Uranus", Seats = 18},
            new() { ID = 8, Name = "Neptune", Seats = 20}
        };

        #endregion Public Fields

        #region Public Properties

        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Seats { get; set; }

        #endregion Public Properties
    }
}
