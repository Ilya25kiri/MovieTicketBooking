using MovieTicketBoking.Exceptions;
using MovieTicketBoking.Repositories;
using MovieTicketBoking.Scenarios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace MovieTicketBoking
{
    public class SpecialBookMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private ReservationRepository _reservationRepository;

        private Movie _specificMovie;

        public SpecialBookMovieScenario(MovieRepository movieRepository, Movie specificMovie, ReservationRepository reservationRepository)
        {
            _movieRepository = movieRepository;
            _reservationRepository = reservationRepository;

            _specificMovie = specificMovie;
        }

        public void Run()
        {

            try
            {

                _specificMovie.ValidatAvailableSeats();

                Console.Clear();
                Console.WriteLine($"You choise: {_specificMovie.Title}");

                // Enter date

                Console.Write("Please, write your Name and surname:");
                string fullName = Console.ReadLine();

                Console.Write("Please, enter your phone number:");
                int phoneNumber = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please, enter number of seats:");
                int numberSeats = Convert.ToInt32(Console.ReadLine());

                ///

                _specificMovie.BookRequestedSeats(numberSeats);
                _reservationRepository.GetAll().Add(new Reservation(_specificMovie.Id, fullName, phoneNumber, numberSeats));

                _movieRepository.Save();
                _reservationRepository.Save();
                

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
