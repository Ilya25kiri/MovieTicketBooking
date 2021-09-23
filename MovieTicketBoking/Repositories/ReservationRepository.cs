using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MovieTicketBoking.Repositories
{
    public class ReservationRepository
    {
        private List<Reservation> _reservations;
        private string _pathToReservetionFile = "../../../Files/Reservetion.json";

        public ReservationRepository()
        {
            _reservations = JsonConvert.DeserializeObject<List<Reservation>>(File.ReadAllText(_pathToReservetionFile));
        }
        public List<Reservation> GetAll()
        {
            var pathToReservetionFile = "../../../Files/Reservetion.json";
            var reservationAsString = File.ReadAllText(pathToReservetionFile);

            return JsonConvert.DeserializeObject<List<Reservation>>(reservationAsString);
        }

        public Reservation FindReservation(int reservationPhoneNumber, Movie selectMovie)
        {
            return _reservations.Where(obj => (obj.PhoneNumber == reservationPhoneNumber)
                                              && (selectMovie.Id == obj.MovieId)).First();
        }

        public void Save()
        {
            File.WriteAllText(_pathToReservetionFile, JsonConvert.SerializeObject(_reservations, Formatting.Indented));
        }

    }
}
