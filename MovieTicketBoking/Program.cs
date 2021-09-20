﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MovieTicketBoking.Exceptions;
using MovieTicketBoking.Scenarios;
using Newtonsoft.Json;

namespace MovieTicketBoking
{
    class Program
    {
        static void Main()
        {
            var pathToMoviesFile = "../../../Files/Movies.json";
            var moviesAsString = File.ReadAllText(pathToMoviesFile);

            var pathToReservetionFile = "../../../Files/Reservetion.json";
            var reservationAsString = File.ReadAllText(pathToReservetionFile);

            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationAsString);
            
            /// Render a table

            RenderMoviesTable(movies);
            RenderMainMenu();

            /// Chouse main menu

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        RenderMoviesTable(movies);
                        RenderMainMenu();
                        break;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SortMovies(movies, pathToMoviesFile);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        new BookMovieScenario(movies, reservations, pathToMoviesFile, pathToReservetionFile).Run();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        reservationAsString = File.ReadAllText(pathToReservetionFile);
                        reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationAsString);
                        CancellationOfReservation(movies, reservations, pathToMoviesFile, pathToReservetionFile);
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Environment.Exit(0);
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);

        }

        private static void CancellationOfReservation(List<Movie> movies, List<Reservation> reservations, string pathToMoviesFile, string pathToReservetionFile)
        {
            try
            {
                Console.WriteLine();
                Console.Write("Enter the number of movie:");

                int movieNumber = Convert.ToInt32(Console.ReadLine());
                var selectMovie = movies[movieNumber-1];

                Console.Clear();
                Console.Write("Enter the number phone of order:");
                var reservationPhoneNumber = Convert.ToInt32(Console.ReadLine());

                var deleteReservation = reservations.Where(obj => (obj.PhoneNumber == reservationPhoneNumber) && (selectMovie.Id == obj.MovieId));
                var deleteReservationId = reservations.LastIndexOf(deleteReservation.FirstOrDefault());

                var reservationMovie = reservations[deleteReservationId];

                reservationMovie.ValidatAvailableMovie(selectMovie.Id, selectMovie.Title);
                reservationMovie.ValidatAvailableNumberOfPhone(reservationPhoneNumber);

                
                File.WriteAllText(pathToMoviesFile, JsonConvert.SerializeObject(movies, Formatting.Indented));

                reservations.RemoveAt(deleteReservationId);
                File.WriteAllText(pathToReservetionFile, JsonConvert.SerializeObject(reservations, Formatting.Indented));

                Console.WriteLine("Yout book was delete");
                Console.WriteLine("Press enter to go back");

                
            }
            catch (NoPhoneNumberException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch(NotMovieException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
        }

        private static void RenderMainMenu()
        {
            // Render main manu

            Console.WriteLine();
            Console.WriteLine("1) Search Movie");
            Console.WriteLine("2) Sort");
            Console.WriteLine("3) Booking a ticket");
            Console.WriteLine("4) Delete reservation");
            Console.WriteLine("5) Add a new movie");
            Console.WriteLine("\nWrite an answer:");
        }

        private static void RenderMoviesTable(List<Movie> movies)
        {
            var maxTitleLenght = movies.Max(movie => movie.Title.Length);

            var TitleColumnName = "Title";

            var leftPaddingForTitle = new string(' ', maxTitleLenght - TitleColumnName.Length);

            Console.WriteLine($"| #  | {TitleColumnName}{leftPaddingForTitle} | Number |");

            foreach (var movieIterator in movies.Select((item, index) => (item, index)))
            {
                var leftPadding = new string(' ', maxTitleLenght - movieIterator.item.Title.Length);
                var rightPadding = new string(' ', 0);
              
                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPadding} |   {rightPadding}{movieIterator.item.NumberOfFreeSeats} |");
            }
        }

        private static void SortMovies(List<Movie> movies, string pathToMoviesFile)
        {
            movies = movies.OrderBy(movie => movie.Title).ToList();
            File.WriteAllText(pathToMoviesFile, JsonConvert.SerializeObject(movies, Formatting.Indented));

            Console.Clear();

            RenderMoviesTable(movies);
            RenderMainMenu();
        }
    }

    public class Movie
    {
        public Guid Id = Guid.NewGuid();
        public string Title { get; set;}
        public int NumberOfFreeSeats { get; set;}

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
        

        internal void ValidatAvailableNumberOfPhone(int reservationPhoneNumber)
        {
            if (PhoneNumber != reservationPhoneNumber)
            {
                throw new NoPhoneNumberException("There's no such number!");
            }
        }

        internal void ValidatAvailableMovie(Guid id, string title)
        {
            if (MovieId != id)
            {
                throw new NotMovieException($"There's no booking! {title}");
            }
        }
    }
}