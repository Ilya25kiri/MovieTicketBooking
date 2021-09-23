using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using MovieTicketBoking.Repositories;
namespace MovieTicketBoking.Scenarios
{
    internal class AddingNewMoviesScenario : IRunnable
    {
        private MovieRepository _movieRepository;
        
        public AddingNewMoviesScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public void Run()
        {
            Movie createMovie = new();

            Console.Clear();
            Console.Write("Write movie's name: ");
            createMovie.Title = Console.ReadLine();

            Console.Write("Write number of free seats: ");
            createMovie.NumberOfFreeSeats = Convert.ToInt32(Console.ReadLine());

            Console.Write("Write movie's genre: ");
            createMovie.Genre = Console.ReadLine();

            Console.Write("Write the rating of this movie: ");
            createMovie.Rating = Convert.ToInt32(Console.ReadLine());

            Console.Write("Write a comment of this movie: ");
            createMovie.Comment = Console.ReadLine();

            _movieRepository.GetAll().Add(new Movie(createMovie.Title, createMovie.NumberOfFreeSeats, createMovie.Genre, createMovie.Rating, createMovie.Comment));

            _movieRepository.Save();
        }
    }
}