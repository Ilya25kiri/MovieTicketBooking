using MovieTicketBoking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTicketBoking.Scenarios
{
    public class CancellationOfReservation : IRunnable
    {
        private List<Movie> _movies;
        private List<Reservation> _reservations;

        private string _pathToReservetionFile;
        private string _pathToMoviesFile;

        public CancellationOfReservation(List<Movie> movies, List<Reservation> reservations, string pathToMoviesFile, string pathToReservetionFile)
        {
            _movies = movies;
            _reservations = reservations;
            _pathToMoviesFile = pathToMoviesFile;
            _pathToReservetionFile = pathToReservetionFile;
        }
        public void Run()
        {
            try
            {
                Console.WriteLine();
                Console.Write("Enter the number of movie:");

                int movieNumber = Convert.ToInt32(Console.ReadLine());
                var selectMovie = _movies[movieNumber - 1];

                Console.Clear();
                Console.Write("Enter the number phone of order:");
                var reservationPhoneNumber = Convert.ToInt32(Console.ReadLine());

                var reservationToCancel = _reservations.Where(obj => (obj.PhoneNumber == reservationPhoneNumber)
                                                                  && (selectMovie.Id == obj.MovieId)).First();

                File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));

                _reservations.Remove(reservationToCancel);
                File.WriteAllText(_pathToReservetionFile, JsonConvert.SerializeObject(_reservations, Formatting.Indented));

                Console.WriteLine("Yout book was delete");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Booking not infind");
            }
            Console.WriteLine("Press enter to go back");
        }
    }
}
