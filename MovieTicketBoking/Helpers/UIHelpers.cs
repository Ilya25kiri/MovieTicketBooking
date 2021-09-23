using System;
using System.Collections.Generic;
using System.Linq;
using MovieTicketBoking.Repositories;

namespace MovieTicketBoking.Helpers
{
    public class UIHelpers
    {
        private MovieRepository _movieRepository;
        private ReservationRepository _reservationRepository;

        public UIHelpers(MovieRepository movieRepository, ReservationRepository reservationRepository)
        {
            _movieRepository = movieRepository;
            _reservationRepository = reservationRepository;
        }

        public void RenderMoviesTable()
        {
            var movies = _movieRepository.GetAll();

            var maxTitleLenght = movies.Max(movie => movie.Title.Length);

            var TitleColumnName = "Title";

            var leftPaddingForTitle = new string(' ', maxTitleLenght - TitleColumnName.Length);

            Console.WriteLine($"| #  | {TitleColumnName}{leftPaddingForTitle} | Number |");

            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var leftPadding = new string(' ', maxTitleLenght - movieIterator.item.Title.Length);
                var rightPadding = new string(' ', 0);

                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPadding} |   {rightPadding}{movieIterator.item.NumberOfFreeSeats}   |");
            }
        }

        public void RendeerReservationTable()
        {
            Console.Clear();

            var maxTitleLenght = _reservationRepository.GetAll().Max(reservation => reservation.FullName.Length);

            var titleColumnName = "Name";

            var leftPaddingForTitle = new string(' ', maxTitleLenght - titleColumnName.Length);
            Console.WriteLine($"| #  | Name{leftPaddingForTitle} | Number |");
            foreach (var reservationsIterator in _reservationRepository.GetAll().Select((item, index) => (item, index)))
            {
                var leftPadding = new string(' ', maxTitleLenght - reservationsIterator.item.FullName.Length);

                var number = reservationsIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {reservationsIterator.item.FullName}{leftPadding} |   {reservationsIterator.item.NumberSeats}    |");
            }
        }

        public void RenderMainMenu()
        {
            // Render main manu

            Console.WriteLine();
            Console.WriteLine("1) Search Movie");
            Console.WriteLine("2) Sort");
            Console.WriteLine("3) Booking a ticket");
            Console.WriteLine("4) Cancel reservation");
            Console.WriteLine("5) Add a new movie");
            Console.WriteLine("6) Check the booking list");
            Console.WriteLine("7) Check comments of movies");
            Console.WriteLine("\nWrite an answer:");
        }
    }
}
