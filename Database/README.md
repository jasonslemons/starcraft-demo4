# Database Setup for StarcraftDemo4

This folder contains SQL scripts to set up and manage the database for the StarCraft Demo 4 application.

## Files

### CreateDatabase.sql
Creates the complete database schema including:
- **Games** table: Stores game metadata and final statistics
- **GameSteps** table: Stores individual moves and game state at each step
- Indexes for performance optimization
- Foreign key relationships

### SampleData.sql
Inserts sample game data for testing purposes:
- 3 sample games with different scenarios
- Multiple game steps showing various move types
- Realistic timestamps and game progression

### Queries.sql
Contains useful analysis queries:
- Game statistics and summaries
- Build order analysis
- Resource efficiency metrics
- Move type distributions
- Game comparison queries

## Database Schema

### Games Table
| Column | Type | Description |
|--------|------|-------------|
| GameId | int (PK, Identity) | Unique game identifier |
| StartTime | datetime2 | When the game began |
| EndTime | datetime2 | When the game ended (nullable) |
| FinalMinerals | int | Minerals at game end |
| FinalGas | int | Gas at game end |
| FinalUnitCount | int | Number of units at game end |
| FinalUnitCap | int | Supply cap at game end |
| TotalGameTime | int | Game duration in seconds |

### GameSteps Table
| Column | Type | Description |
|--------|------|-------------|
| StepId | int (PK, Identity) | Unique step identifier |
| GameId | int (FK) | Reference to Games table |
| StepNumber | int | Sequential step number within game |
| MoveType | nvarchar(50) | Type of move (Unit, Structure, etc.) |
| MoveDescription | nvarchar(200) | Human-readable move description |
| ObjectBuilt | nvarchar(100) | Name of object that was built |
| GameTimeAtStep | int | Game time when step occurred |
| MineralsAtStep | int | Minerals at this step |
| GasAtStep | int | Gas at this step |
| UnitCountAtStep | int | Unit count at this step |
| UnitCapAtStep | int | Unit cap at this step |
| StepTimestamp | datetime2 | Real-world timestamp of step |

## Setup Instructions

### Option 1: SQL Server Management Studio
1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Run `CreateDatabase.sql` to create the schema
4. Optionally run `SampleData.sql` to add test data

### Option 2: Command Line (sqlcmd)
```bash
sqlcmd -S server_name -E -i CreateDatabase.sql
sqlcmd -S server_name -E -i SampleData.sql
```

### Option 3: Entity Framework Code First
The application uses Entity Framework Code First, so the database will be automatically created when you run the console application for the first time.

## Connection Strings

### Local SQL Server
```
Server=(localdb)\\mssqllocaldb;Database=StarcraftDemo4;Trusted_Connection=true;
```

### Azure SQL Database
```
Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=StarcraftDemo4;User ID=username;Password=password;
```

## Performance Notes

The schema includes indexes on:
- `Games.StartTime` for chronological queries
- `GameSteps.GameId` for joining with Games
- `GameSteps.GameId, StepNumber` for step sequence queries

For production use with large datasets, consider adding additional indexes based on your query patterns.