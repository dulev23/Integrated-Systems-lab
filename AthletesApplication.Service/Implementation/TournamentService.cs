using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AthletesApplication.Service.Implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<AthleteInTournament> _athleteInTournamentRepository;
        private readonly IParticipationService _participationService;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<AthleteInTournament> athleteInTournamentRepository, IParticipationService participationService)
        {
            _tournamentRepository = tournamentRepository;
            _athleteInTournamentRepository = athleteInTournamentRepository;
            _participationService = participationService;
        }

        public Tournament CreateTournament(string userId)
        {
            // TODO: Implement method
            // Hint: Look at Auditory exercises - OrderProducts method in ShoppingCartService
            // Get all Participations by current user
            var participations = _participationService.GetAllByCurrentUser(userId);

            var tournament = new Tournament
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                OwnerId = userId
            };
            // Create new Tournament and insert in database
            _tournamentRepository.Insert(tournament);
            // Create new AthletesInTournament using Athletes from the Participations and insert in database
            var athletesInTournament = participations.Select(x => new AthleteInTournament
            {
                AthleteId = x.AthleteId,
                Athlete = x.Athlete,
                TournamentId = tournament.Id,
                Tournament = tournament
            });
            
            foreach(var item in athletesInTournament)
            {
                _athleteInTournamentRepository.Insert(item);
            }
            // Delete all Participations
            foreach(var item in participations)
            {
                _participationService.DeleteById(item.Id);
            }
            // Return Tournament
            return tournament;
        }


        // Bonus task
        public Tournament GetTournamentDetails(Guid id)
        {
            // TODO: Implement method
            return _tournamentRepository.Get(selector: x => x,
                                            predicate: x => x.Id == id,
                                            include: x => x.Include(y => y.AthleteInTournaments)
                                                           .ThenInclude(z => z.Athlete));
        }
    }
}
