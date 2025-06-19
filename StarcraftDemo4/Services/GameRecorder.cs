using System;
using System.Collections.Generic;
using System.Linq;
using StarcraftDemo4.Data;
using StarcraftDemo4.Models;

namespace StarcraftDemo4.Services
{
    public class GameRecorder : IDisposable
    {
        private StarcraftDbContext _context;
        private GameEntity? _currentGame;
        private int _stepCounter;

        public GameRecorder()
        {
            _context = new StarcraftDbContext();
            _context.Database.EnsureCreated();
        }

        public void StartNewGame()
        {
            _currentGame = new GameEntity
            {
                StartTime = DateTime.Now,
                FinalMinerals = 0,
                FinalGas = 0,
                FinalUnitCount = 0,
                FinalUnitCap = 0,
                TotalGameTime = 0
            };
            
            _context.Games.Add(_currentGame);
            _context.SaveChanges();
            _stepCounter = 0;
        }

        public void RecordGameStep(Move move, State gameState)
        {
            if (_currentGame == null)
                return;

            _stepCounter++;
            
            var gameStep = new GameStepEntity
            {
                GameId = _currentGame.GameId,
                StepNumber = _stepCounter,
                MoveType = move.str,
                MoveDescription = GetMoveDescription(move),
                ObjectBuilt = GetObjectName(move.obj),
                GameTimeAtStep = gameState.totalTime,
                MineralsAtStep = gameState.minerals,
                GasAtStep = gameState.gas,
                UnitCountAtStep = gameState.unit_Count,
                UnitCapAtStep = gameState.unit_Cap,
                StepTimestamp = DateTime.Now
            };

            _context.GameSteps.Add(gameStep);
            _context.SaveChanges();
        }

        public void FinishGame(State finalState)
        {
            if (_currentGame == null)
                return;

            _currentGame.EndTime = DateTime.Now;
            _currentGame.FinalMinerals = finalState.minerals;
            _currentGame.FinalGas = finalState.gas;
            _currentGame.FinalUnitCount = finalState.unit_Count;
            _currentGame.FinalUnitCap = finalState.unit_Cap;
            _currentGame.TotalGameTime = finalState.totalTime;

            _context.SaveChanges();
        }

        private string GetMoveDescription(Move move)
        {
            switch (move.str)
            {
                case "Unit":
                    return $"Built unit: {GetObjectName(move.obj)}";
                case "Structure":
                    return $"Built structure: {GetObjectName(move.obj)}";
                case "Addon":
                    return $"Built addon: {GetObjectName(move.obj)}";
                case "Upgrade":
                    return $"Researched upgrade: {GetObjectName(move.obj)}";
                case "EnergyUnit":
                    return $"Used energy to build: {GetObjectName(move.obj)}";
                case "LiftoffFrom":
                    return $"Lifted off {GetObjectName(move.obj)} from {GetObjectName(move.obj2)}";
                case "Landon":
                    return $"Landed {GetObjectName(move.obj)} on {GetObjectName(move.obj2)}";
                case "MoveGas":
                    return "Moved worker to gas";
                case "MoveMineral":
                    return "Moved worker to minerals";
                default:
                    return $"Unknown move: {move.str}";
            }
        }

        private string GetObjectName(object obj)
        {
            if (obj == null)
                return "Unknown";
                
            return obj.GetType().Name;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public List<GameEntity> GetAllGames()
        {
            return _context.Games.OrderByDescending(g => g.StartTime).ToList();
        }

        public GameEntity GetGameWithSteps(int gameId)
        {
            return _context.Games
                .Where(g => g.GameId == gameId)
                .Select(g => new GameEntity
                {
                    GameId = g.GameId,
                    StartTime = g.StartTime,
                    EndTime = g.EndTime,
                    FinalMinerals = g.FinalMinerals,
                    FinalGas = g.FinalGas,
                    FinalUnitCount = g.FinalUnitCount,
                    FinalUnitCap = g.FinalUnitCap,
                    TotalGameTime = g.TotalGameTime,
                    GameSteps = g.GameSteps.OrderBy(s => s.StepNumber).ToList()
                })
                .FirstOrDefault();
        }
    }
}