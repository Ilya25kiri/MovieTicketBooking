using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MovieTicketBoking.Exceptions;
using MovieTicketBoking.Helpers;
using MovieTicketBoking.Repositories;
using MovieTicketBoking.Scenarios;
using Newtonsoft.Json;

namespace MovieTicketBoking
{
    class Program
    {
        static void Main()
        {
            var movieRepository = new MovieRepository();
            var reservationRepository = new ReservationRepository();
            var uiHelper = new UIHelpers(movieRepository, reservationRepository);

            /// Render a table

            uiHelper.RenderMoviesTable();
            uiHelper.RenderMainMenu();

            /// Chouse main menu

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        uiHelper.RenderMoviesTable();
                        uiHelper.RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        new SearchMovieScenario(movieRepository, reservationRepository).Run();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies(movieRepository);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movieRepository, reservationRepository).Run();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        new CancellationOfReservation( movieRepository, reservationRepository).Run();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        new AddingNewMoviesScenario(movieRepository).Run();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        new ShowBookingListScenario(reservationRepository).Run();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        new ShowCommentsScenario(movieRepository).Run();
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);

        }

        private static void SortMovies(MovieRepository movieRepository)
        {
            movieRepository.GetAll().OrderBy(movie => movie.Title).ToList();
            movieRepository.Save();

            Console.WriteLine("Press enter to go back");
        }
    }

    public class Movie
    {
        public Guid Id = Guid.NewGuid();

        public Movie()
        {
        }

        public Movie(string title, int numberOfFreeSeats, string genre, int rating, string comment)
        {
            Title = title;
            NumberOfFreeSeats = numberOfFreeSeats;
            Genre = genre;
            Rating = rating;
            Comment = comment;
        }

        public string Title {get; set;}
        public int NumberOfFreeSeats {get; set;}
        public int Rating {get; set;}
        public string Genre {get; set;}
        public string Comment { get; set; }

        internal void BookRequestedSeats(int requestedSeats)
        {
            if (NumberOfFreeSeats < requestedSeats)
            {
                throw new NoEnoughtSeatsException("Our apologies, there's no free seats available you need!");
            }

            NumberOfFreeSeats -= requestedSeats;
        }

        internal void ValidatAvailableSeats()
        {
            if (NumberOfFreeSeats == 0)
            {
                throw new NoSeatsException($"There's no free seats! {Title}");
            }
        }
        
    }

    public class Reservation
    {
        public Guid MovieId { get; set;}
        public string FullName { get; set;}
        public int PhoneNumber { get; set;}
        public int NumberSeats { get; set;}
        public Reservation(Guid movieId, string fullName, int phoneNumber, int numberSeats)
        {
            MovieId = movieId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            NumberSeats = numberSeats;
        }
    }
}
