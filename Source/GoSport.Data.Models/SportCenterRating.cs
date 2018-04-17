using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoSport.Data.Models
{
    public class SportCenterRating
    {
        [Key, ForeignKey("SportCenter")]
        public int SportCenterId { get; set; }

        public virtual SportCenter SportCenter { get; set; }

        public double Total { get; set; }

        public int TotalVotes { get; set; }

        public double Comfort { get; set; }

        public int ComfortVotes { get; set; }

        public double Service { get; set; }

        public int ServiceVotes { get; set; }

        public double Price { get; set; }

        public int PriceVotes { get; set; }

        public double Trainers { get; set; }

        public int TrainersVotes { get; set; }
    }
}