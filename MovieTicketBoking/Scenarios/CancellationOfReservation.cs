using MovieTicketBoking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MovieTicketBoking.Repositories;

namespace MovieTicketBoking.Scenarios
{
    public class CancellationOfReservation : IRunnable
    {
        private MovieRepository _movieRepository;
        private ReservationRepository _reservationRepository;

        public CancellationOfReservation(MovieRepository movieRepository, ReservationRepository reservationRepository)
        {
            _movieRepository = movieRepository;
            _reservationRepository = reservationRepository;
        }
        public void Run()
        {
            try
            {
                Console.WriteLine();
                Console.Write("Enter the number of movie:");

                int movieNumber = Convert.ToInt32(Console.ReadLine());
                var selectMovie = _movieRepository.GetAll()[movieNumber - 1];

                Console.Clear();
                Console.Write("Enter the number phone of order:");
                var reservationPhoneNumber = Convert.ToInt32(Console.ReadLine());

                var reservationToCancel = _reservationRepository.FindReservation(reservationPhoneNumber, selectMovie);

                selectMovie.NumberOfFreeSeats += reservationToCancel.NumberSeats;
                _movieRepository.Save();

                _reservationRepository.GetAll().Remove(reservationToCancel);
                _reservationRepository.Save();

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
