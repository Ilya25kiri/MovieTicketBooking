using System;
using System.Collections.Generic;
using MovieTicketBoking.Repositories;
using MovieTicketBoking.Scenarios;

namespace MovieTicketBoking.SpecialScenarios
{
    public class SpecialShowBookingListScenario : IRunnable
    {
        ReservationRepository _reservationRepository;

        public SpecialShowBookingListScenario(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void Run()
        {

        }
    }
}
