using System;
using System.Collections.Generic;
using System.Linq;
using MovieTicketBoking.Helpers;
using MovieTicketBoking.Repositories;

namespace MovieTicketBoking.Scenarios
{
    public class ShowBookingListScenario : IRunnable
    {
        private UIHelpers uIHelpers;
        private ReservationRepository _reservationRepository;

        public ShowBookingListScenario( ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void Run()
        {
            uIHelpers.RendeerReservationTable();

            Console.WriteLine();
            Console.WriteLine("Press enter to go back");
        }
    }
}
