using System;
using System.Collections.Generic;
using System.Linq;
using MovieTicketBoking.Repositories;
using MovieTicketBoking.SpecialScenarios;

namespace MovieTicketBoking.Scenarios
{
    public class SearchMovieScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        private ReservationRepository _reservationRepository;

        public SearchMovieScenario(MovieRepository movieRepository, ReservationRepository reservationRepository)
        {
            _movieRepository = movieRepository;
            _reservationRepository = reservationRepository;
        }

        public void Run()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter the genge of movies:");
                var genreToSearch = Console.ReadLine();

                Console.Write("Enter the name of movies: ");
                var titleToSearch = Console.ReadLine();

                Movie foundMovie =  _movieRepository.FindMovie(titleToSearch, genreToSearch);
                
                Console.WriteLine();
                Console.WriteLine("Сongratulations, the movie has been found");
                Console.WriteLine($"| {foundMovie.Title} | {foundMovie.NumberOfFreeSeats} | {foundMovie.Genre} |");

                ConsoleKeyInfo keyInfo;
                Console.WriteLine();

                Console.WriteLine("1 Booking Movie");
                Console.WriteLine("2 Check the booking list");
                Console.WriteLine("3 Check comments of movies");
                Console.WriteLine("4 Go back to menu");
                Console.Write("\nWrite an answer:");
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SpecialBookMovieScenario(_movieRepository, foundMovie, _reservationRepository).Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        new SpecialShowBookingListScenario(_reservationRepository).Run();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new ShowCommentsScenario(_movieRepository).Run();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movies didn't find");

                Console.WriteLine();
                Console.WriteLine("1 Add new movie");

                Console.WriteLine("2 Exit to menu");
                Console.WriteLine("Write your answer: ");

                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new AddingNewMoviesScenario(_movieRepository).Run();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.Enter:
                        break;
                }
            }
            
        }
    }
}
