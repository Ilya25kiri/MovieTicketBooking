using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketBoking.Scenarios
{
    public class SearchMovieScenario : IRunnable
    {
        private List<Movie> _movies;
        public SearchMovieScenario(List<Movie> movies)
        {
            _movies = movies;
        }
        public void Run()
        {
            try
            {
                Console.Clear();
                Console.Write("Enter the name of movies: ");
                var TitleToSearch = Console.ReadLine();

                var foundMovie = _movies.Where(item => item.Title.ToLower().Contains(TitleToSearch)).First();
                
                Console.WriteLine();
                Console.WriteLine("Сongratulations, the movie has been found");
                Console.WriteLine($"| {foundMovie.Title} | {foundMovie.NumberOfFreeSeats} |");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine();
                Console.WriteLine("Movies didn't find");
            }
            Console.WriteLine();
            Console.WriteLine("Press enter to go back");
        }
    }
}
