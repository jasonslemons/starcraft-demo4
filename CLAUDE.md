# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

StarcraftDemo4 is a C# console application that simulates StarCraft 2 Terran build orders and resource management. The project has recently been upgraded from .NET Framework 4.8 to .NET 8.0 and includes Entity Framework Core integration for database persistence.

## Development Commands

**Build and Run:**
```bash
cd StarcraftDemo4
dotnet build
dotnet run
```

**Database Operations:**
The application uses Entity Framework Core with SQL Server LocalDB. The database is automatically created on first run via `EnsureCreated()` in the GameRecorder service.

## Architecture Overview

### Core Game Engine
- **State**: Central game state management (resources, units, structures, time progression)
- **Player**: Base class for all game actions (building, training, resource management)
- **AutoPlayer**: AI implementation that randomly selects from valid moves using `PossibleMoves()`
- **SingleGame**: Game execution engine with three main methods:
  - `PlayAutoGame()` - Runs automated AI games with database recording
  - `ReplayGame()` - Replays a sequence of moves with database recording  
  - `PlayGame()` - Interactive console gameplay

### Game Objects Hierarchy
- **Structure**: Base class for all buildings (Command Centers, Barracks, etc.)
  - **ProducingStructure**: Buildings that can train units or research upgrades
  - **ExtendableProducingStructure**: Buildings that can have addons (Tech Labs, Reactors)
- **Unit**: Base class for all units (SCVs, Marines, etc.)
- **Upgrade**: Research improvements
- **Addon**: Building attachments for extended functionality

### Database Integration
- **Models/**: Entity Framework entities (`GameEntity`, `GameStepEntity`)
- **Data/**: DbContext implementation (`StarcraftDbContext`)
- **Services/**: Game recording service (`GameRecorder`) that automatically saves all games and moves

### Key Game Mechanics
- **Resource System**: Accurate mineral/gas harvesting simulation with worker efficiency curves
- **Build Prerequisites**: Complex requirement checking via `meetReqs()` methods
- **Time Progression**: All actions consume game time, tracked in `State.totalTime`
- **Terran Specifics**: Building lift/land mechanics, addon swapping, orbital command energy management

## Move System Architecture

The game uses a `Move` struct to represent all player actions:
- `obj`: The primary object being acted upon
- `obj2`: Secondary object (used for addon operations)  
- `str`: Move type identifier ("Unit", "Structure", "Addon", "Upgrade", "LiftoffFrom", etc.)

All moves are processed through `SingleGame.MakeMove()` and automatically recorded to the database with complete game state snapshots.

## Database Schema

Games are stored with full step-by-step history:
- **Games**: Metadata, start/end times, final resources and unit counts
- **GameSteps**: Individual moves with timestamps, game state, and move descriptions

Use `GameRecorder.GetGameWithSteps(gameId)` to retrieve complete game histories for analysis.

## Entry Points

- **Test.Main()**: Runs simulation games via `GameOptimizer.PlayBaseGames()`
- **GameOptimizer**: Manages multiple game execution and results display
- Console output includes database-retrieved game summaries after simulation completion

 