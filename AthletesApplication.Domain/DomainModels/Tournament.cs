using AthletesApplication.Domain.IdentityModels;

namespace AthletesApplication.Domain.DomainModels
{
    public class Tournament : BaseEntity
    {
        //датум на креирање, референца до корисникот кој ја иницира акцијата)
        public DateTime DateCreated { get; set; }
        public string? OwnerId { get; set; }
        public AthletesApplicationUser? Owner { get; set; }
        public virtual ICollection<AthleteInTournament>? AthleteInTournaments { get; set; }
    }
}