namespace Library_web_API.persistence.model
{
    public class Reservation
    {
        public int Id { get; private set; }

        //When copy of book was borrowed
        public DateOnly Date { get; set; }

        public string BorrowerName { get; set; }

        public bool IsReturned { get; set; }

        public int BookId { get; set; }

    }
}
