
using MovieTicketBoking.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieTicketBoking.Scenarios
{
    public class BookMovieScenario : IRunnable
    {
        private List<Movie> _movies;
        private List<Reservation> _reservations;

        private string _pathToReservetionFile;
        private string _pathToMoviesFile;
        

        public BookMovieScenario(List<Movie> movies, List<Reservation> reservations, string pathToMoviesFile, string pathToReservetionFile)
        {
            _movies = movies;
            _reservations = reservations;
            _pathToMoviesFile = pathToMoviesFile;
            _pathToReservetionFile = pathToReservetionFile;
        }

        public void Run()
        {
            Console.WriteLine();
            Console.Write("Enter the number of movie:");

            try
            {
                // Get movies

                int movieNumber = Convert.ToInt32(Console.ReadLine());
                var selectedMovie = _movies[movieNumber - 1];

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
                _reservations.Add(new Reservation(selectedMovie.Id, fullName, phoneNumber, numberSeats));

                File.WriteAllText(_pathToReservetionFile, JsonConvert.SerializeObject(_reservations, Formatting.Indented));
                File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));

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
