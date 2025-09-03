using Library_web_API.persistence;
using Library_web_API.persistence.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_web_API.controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext context;

        public ReservationController (AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation (Reservation reservation)
        {
            Book book = await context.Books.FindAsync(reservation.BookId);
            if (book.NumberOfCopies > 0)
            {
                book.NumberOfCopies--;

                reservation.IsReturned = false;
                context.Reservations.Add(reservation);

                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
            } else
            {
                return StatusCode(400, "All copies of this book are borrowed.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Reservation>> ReturnBook([FromBody] string borrowerName)
        {
            /*Find first bokk by name. Upate NumberOfCopies.
             *Update IsReturned in Reservation
             */
            Reservation reservation = await context.Reservations.FirstOrDefaultAsync(r => r.BorrowerName == borrowerName);
            if (reservation == null)
            {
                return NotFound(borrowerName);
            }
            reservation.IsReturned = true;

            Book book = await context.Books.FindAsync(reservation.BookId);

            if (book == null)
            {
                return NotFound("Reservation was found, but book not.");
            }
            book.NumberOfCopies++;

            await context.SaveChangesAsync();
            return Ok(reservation);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            Reservation reservation = await context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound(id);
            }

            return Ok(reservation);
        }
    }
}
