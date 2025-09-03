using System.ComponentModel.DataAnnotations;

namespace Library_web_API.persistence.model
{
    public class Book
    {
        public enum Categories { Fantasy, Romance, Science, History, Crime_novel, Horror, Comic }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public Categories Category {get; set;}

        public int NumberOfCopies { get; set; }

        public List<Reservation> Reservations { get; private set; } = new ();
    }
}