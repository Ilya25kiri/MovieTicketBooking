using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MovieTicketBoking.Repositories
{
    public class MovieRepository
    {
        private List<Movie> _movies;
        private string _pathToMoviesFile = "../../../Files/Movies.json";

        public MovieRepository()
        {
            _movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(_pathToMoviesFile));
        }
       
        public List<Movie> GetAll()
        {
            var pathToMoviesFile = "../../../Files/Movies.json";
            var moviesAsString = File.ReadAllText(pathToMoviesFile);

            return JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);
        }

        public Movie FindMovie(string titleToSearch, string genreToSearch)
        {
            return _movies.Where(item => item.Title.ToLower().Contains(titleToSearch) && item.Genre.ToLower().Contains(genreToSearch)).First();

        }

        public void Save()
        {
            File.WriteAllText(_pathToMoviesFile, JsonConvert.SerializeObject(_movies, Formatting.Indented));
        }
    }
}
