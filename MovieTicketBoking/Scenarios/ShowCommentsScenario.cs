using System;
using System.Linq;
using MovieTicketBoking.Repositories;

namespace MovieTicketBoking.Scenarios
{
    public class ShowCommentsScenario : IRunnable
    {
        private MovieRepository _movieRepository;

        public ShowCommentsScenario(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Run()
        {
            Console.Clear();

            var maxTitleLenght = _movieRepository.GetAll().Max(movie => movie.Title.Length);

            var titleColumnName = "Title";

            var leftPaddingForTitle = new string(' ', maxTitleLenght - titleColumnName.Length);

            Console.WriteLine($"| #  | {titleColumnName}{leftPaddingForTitle} | Comments");

            foreach (var movieIterator in _movieRepository.GetAll().Select((item, index) => (item, index)))
            {
                var leftPadding = new string(' ', maxTitleLenght - movieIterator.item.Title.Length);
                
                var number = movieIterator.index + 1;
                Console.WriteLine($"| {number.ToString("D2")} | {movieIterator.item.Title}{leftPadding} |   {movieIterator.item.Comment}");
                Console.WriteLine();
            }
            Console.WriteLine("Press enter to go back");
        }
    }
}
