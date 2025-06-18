using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarcraftDemo4.Models
{
    public class GameStepEntity
    {
        [Key]
        public int StepId { get; set; }
        
        [ForeignKey("Game")]
        public int GameId { get; set; }
        
        public int StepNumber { get; set; }
        
        public string MoveType { get; set; } = string.Empty;
        
        public string MoveDescription { get; set; } = string.Empty;
        
        public string ObjectBuilt { get; set; } = string.Empty;
        
        public int GameTimeAtStep { get; set; }
        
        public int MineralsAtStep { get; set; }
        
        public int GasAtStep { get; set; }
        
        public int UnitCountAtStep { get; set; }
        
        public int UnitCapAtStep { get; set; }
        
        public DateTime StepTimestamp { get; set; }
        
        public virtual GameEntity Game { get; set; } = null!;
    }
}