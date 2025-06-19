-- Sample Data for StarcraftDemo4 Database
-- This script inserts sample game data for testing purposes

USE StarcraftDemo4;
GO

-- Insert sample games
IF NOT EXISTS (SELECT 1 FROM Games)
BEGIN
    PRINT 'Inserting sample game data...';
    
    -- Sample Game 1
    INSERT INTO Games (StartTime, EndTime, FinalMinerals, FinalGas, FinalUnitCount, FinalUnitCap, TotalGameTime)
    VALUES 
    (DATEADD(day, -2, GETDATE()), DATEADD(day, -2, DATEADD(minute, 8, GETDATE())), 450, 75, 28, 36, 480);
    
    DECLARE @GameId1 int = SCOPE_IDENTITY();
    
    -- Sample steps for Game 1
    INSERT INTO GameSteps (GameId, StepNumber, MoveType, MoveDescription, ObjectBuilt, GameTimeAtStep, MineralsAtStep, GasAtStep, UnitCountAtStep, UnitCapAtStep, StepTimestamp)
    VALUES 
    (@GameId1, 1, 'Unit', 'Built unit: SCV', 'SCV', 17, 25, 0, 7, 12, DATEADD(day, -2, DATEADD(second, 17, GETDATE()))),
    (@GameId1, 2, 'Structure', 'Built structure: SupplyDepot', 'SupplyDepot', 47, 75, 0, 7, 20, DATEADD(day, -2, DATEADD(second, 47, GETDATE()))),
    (@GameId1, 3, 'Unit', 'Built unit: SCV', 'SCV', 64, 50, 0, 8, 20, DATEADD(day, -2, DATEADD(second, 64, GETDATE()))),
    (@GameId1, 4, 'Structure', 'Built structure: Barracks', 'Barracks', 129, 25, 0, 8, 20, DATEADD(day, -2, DATEADD(second, 129, GETDATE()))),
    (@GameId1, 5, 'Unit', 'Built unit: Marine', 'Marine', 154, 75, 0, 9, 20, DATEADD(day, -2, DATEADD(second, 154, GETDATE())));
    
    -- Sample Game 2
    INSERT INTO Games (StartTime, EndTime, FinalMinerals, FinalGas, FinalUnitCount, FinalUnitCap, TotalGameTime)
    VALUES 
    (DATEADD(day, -1, GETDATE()), DATEADD(day, -1, DATEADD(minute, 6, GETDATE())), 320, 150, 22, 28, 360);
    
    DECLARE @GameId2 int = SCOPE_IDENTITY();
    
    -- Sample steps for Game 2
    INSERT INTO GameSteps (GameId, StepNumber, MoveType, MoveDescription, ObjectBuilt, GameTimeAtStep, MineralsAtStep, GasAtStep, UnitCountAtStep, UnitCapAtStep, StepTimestamp)
    VALUES 
    (@GameId2, 1, 'Structure', 'Built structure: SupplyDepot', 'SupplyDepot', 30, 50, 0, 6, 20, DATEADD(day, -1, DATEADD(second, 30, GETDATE()))),
    (@GameId2, 2, 'Structure', 'Built structure: Refinery', 'Refinery', 75, 25, 0, 6, 20, DATEADD(day, -1, DATEADD(second, 75, GETDATE()))),
    (@GameId2, 3, 'MoveGas', 'Moved worker to gas', 'Unknown', 80, 25, 0, 6, 20, DATEADD(day, -1, DATEADD(second, 80, GETDATE()))),
    (@GameId2, 4, 'Structure', 'Built structure: Barracks', 'Barracks', 145, 75, 15, 6, 20, DATEADD(day, -1, DATEADD(second, 145, GETDATE()))),
    (@GameId2, 5, 'Unit', 'Built unit: Marine', 'Marine', 170, 125, 25, 7, 20, DATEADD(day, -1, DATEADD(second, 170, GETDATE())));
    
    -- Sample Game 3 (Recent)
    INSERT INTO Games (StartTime, EndTime, FinalMinerals, FinalGas, FinalUnitCount, FinalUnitCap, TotalGameTime)
    VALUES 
    (DATEADD(hour, -2, GETDATE()), DATEADD(hour, -2, DATEADD(minute, 10, GETDATE())), 680, 200, 35, 44, 600);
    
    DECLARE @GameId3 int = SCOPE_IDENTITY();
    
    -- Sample steps for Game 3
    INSERT INTO GameSteps (GameId, StepNumber, MoveType, MoveDescription, ObjectBuilt, GameTimeAtStep, MineralsAtStep, GasAtStep, UnitCountAtStep, UnitCapAtStep, StepTimestamp)
    VALUES 
    (@GameId3, 1, 'Unit', 'Built unit: SCV', 'SCV', 17, 25, 0, 7, 12, DATEADD(hour, -2, DATEADD(second, 17, GETDATE()))),
    (@GameId3, 2, 'Structure', 'Built structure: SupplyDepot', 'SupplyDepot', 47, 75, 0, 7, 20, DATEADD(hour, -2, DATEADD(second, 47, GETDATE()))),
    (@GameId3, 3, 'Structure', 'Built structure: Barracks', 'Barracks', 112, 25, 0, 7, 20, DATEADD(hour, -2, DATEADD(second, 112, GETDATE()))),
    (@GameId3, 4, 'Addon', 'Built addon: Reactor', 'Reactor', 162, 75, 25, 7, 20, DATEADD(hour, -2, DATEADD(second, 162, GETDATE()))),
    (@GameId3, 5, 'Unit', 'Built unit: Marine', 'Marine', 187, 125, 25, 8, 20, DATEADD(hour, -2, DATEADD(second, 187, GETDATE()))),
    (@GameId3, 6, 'Unit', 'Built unit: Marine', 'Marine', 212, 175, 25, 9, 20, DATEADD(hour, -2, DATEADD(second, 212, GETDATE()))),
    (@GameId3, 7, 'Structure', 'Built structure: Factory', 'Factory', 287, 25, 50, 9, 20, DATEADD(hour, -2, DATEADD(second, 287, GETDATE()))),
    (@GameId3, 8, 'EnergyUnit', 'Used energy to build: Mule', 'Mule', 320, 75, 75, 10, 20, DATEADD(hour, -2, DATEADD(second, 320, GETDATE())));
    
    PRINT 'Sample data inserted successfully!';
END
ELSE
BEGIN
    PRINT 'Sample data already exists, skipping insert.';
END
GO