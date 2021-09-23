
using MovieTicketBoking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using MovieTicketBoking.Repositories;

namespace MovieTicketBoking.Scenarios
{
    public class BookMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private ReservationRepository _reservationRepository;

        public BookMovieScenario(MovieRepository movieRepository, ReservationRepository reservationRepository)
        {
            _movieRepository = movieRepository;
            _reservationRepository = reservationRepository;
        }

        public void Run()
        {
            Console.WriteLine();
            Console.Write("Enter the number of movie:");

            try
            {
                // Get movies

                int movieNumber = Convert.ToInt32(Console.ReadLine());
                var selectedMovie = _movieRepository.GetAll()[movieNumber - 1];

                selectedMovie.ValidatAvailableSeats();

                Console.Clear();
                Console.WriteLine($"You choise: {selectedMovie.Title}");

                // Enter date

                Console.Write("Please, write your Name and surname:");
                string fullName = Console.ReadLine();

                Console.Write("Please, enter your phone number:");
                int phoneNumber = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please, enter number of seats:");
                int numberSeats = Convert.ToInt32(Console.ReadLine());

                ///

                selectedMovie.BookRequestedSeats(numberSeats);
                _reservationRepository.GetAll().Add(new Reservation(selectedMovie.Id, fullName, phoneNumber, numberSeats));

                _reservationRepository.Save();
                _movieRepository.Save();

            }
            catch (NoEnoughtSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (NoSeatsException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("Press enter to go back");
        }
    }
}
