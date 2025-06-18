using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarcraftDemo4.Models
{
    public class GameEntity
    {
        [Key]
        public int GameId { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        public int FinalMinerals { get; set; }
        
        public int FinalGas { get; set; }
        
        public int FinalUnitCount { get; set; }
        
        public int FinalUnitCap { get; set; }
        
        public int TotalGameTime { get; set; }
        
        public virtual ICollection<GameStepEntity> GameSteps { get; set; } = new List<GameStepEntity>();
    }
}