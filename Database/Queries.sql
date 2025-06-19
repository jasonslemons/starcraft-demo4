-- Useful Queries for StarcraftDemo4 Database
-- This script contains helpful queries for analyzing game data

USE StarcraftDemo4;
GO

-- View all games with basic statistics
SELECT 
    GameId,
    StartTime,
    EndTime,
    TotalGameTime,
    FinalMinerals,
    FinalGas,
    FinalUnitCount,
    FinalUnitCap,
    CAST(FinalUnitCount AS FLOAT) / FinalUnitCap * 100 AS SupplyUsagePercent
FROM Games
ORDER BY StartTime DESC;

-- View games played in the last 24 hours
SELECT 
    GameId,
    StartTime,
    TotalGameTime,
    FinalMinerals + FinalGas AS TotalResources,
    FinalUnitCount
FROM Games
WHERE StartTime >= DATEADD(hour, -24, GETDATE())
ORDER BY StartTime DESC;

-- Game summary with step counts
SELECT 
    g.GameId,
    g.StartTime,
    g.TotalGameTime,
    g.FinalMinerals,
    g.FinalGas,
    COUNT(gs.StepId) AS TotalSteps,
    AVG(CAST(gs.GameTimeAtStep AS FLOAT)) AS AvgStepTime
FROM Games g
LEFT JOIN GameSteps gs ON g.GameId = gs.GameId
GROUP BY g.GameId, g.StartTime, g.TotalGameTime, g.FinalMinerals, g.FinalGas
ORDER BY g.StartTime DESC;

-- Most common build orders (first 5 moves)
SELECT 
    STRING_AGG(MoveDescription, ' -> ') WITHIN GROUP (ORDER BY StepNumber) AS BuildOrder,
    COUNT(*) AS GameCount
FROM (
    SELECT 
        GameId,
        StepNumber,
        MoveDescription,
        ROW_NUMBER() OVER (PARTITION BY GameId ORDER BY StepNumber) as rn
    FROM GameSteps
) ranked
WHERE rn <= 5
GROUP BY GameId
ORDER BY GameCount DESC;

-- Resource efficiency analysis
SELECT 
    GameId,
    TotalGameTime,
    FinalMinerals + FinalGas AS TotalResources,
    (FinalMinerals + FinalGas) / CAST(TotalGameTime AS FLOAT) AS ResourcesPerSecond,
    FinalUnitCount / CAST(TotalGameTime AS FLOAT) * 100 AS UnitsPerSecond
FROM Games
WHERE TotalGameTime > 0
ORDER BY ResourcesPerSecond DESC;

-- Move type distribution across all games
SELECT 
    MoveType,
    COUNT(*) AS MoveCount,
    COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS Percentage
FROM GameSteps
GROUP BY MoveType
ORDER BY MoveCount DESC;

-- Games with longest duration
SELECT TOP 10
    GameId,
    StartTime,
    TotalGameTime,
    FinalMinerals,
    FinalGas,
    FinalUnitCount
FROM Games
ORDER BY TotalGameTime DESC;

-- Average game statistics
SELECT 
    COUNT(*) AS TotalGames,
    AVG(CAST(TotalGameTime AS FLOAT)) AS AvgGameTime,
    AVG(CAST(FinalMinerals AS FLOAT)) AS AvgFinalMinerals,
    AVG(CAST(FinalGas AS FLOAT)) AS AvgFinalGas,
    AVG(CAST(FinalUnitCount AS FLOAT)) AS AvgFinalUnits,
    MAX(TotalGameTime) AS LongestGame,
    MIN(TotalGameTime) AS ShortestGame
FROM Games;

-- Step-by-step resource progression for a specific game
-- (Replace @GameId with actual game ID)
DECLARE @GameId INT = 1;
SELECT 
    StepNumber,
    GameTimeAtStep,
    MoveDescription,
    MineralsAtStep,
    GasAtStep,
    UnitCountAtStep,
    MineralsAtStep - LAG(MineralsAtStep, 1, 0) OVER (ORDER BY StepNumber) AS MineralChange,
    GasAtStep - LAG(GasAtStep, 1, 0) OVER (ORDER BY StepNumber) AS GasChange
FROM GameSteps
WHERE GameId = @GameId
ORDER BY StepNumber;

-- Find games with similar build patterns
WITH FirstMoves AS (
    SELECT 
        GameId,
        STRING_AGG(ObjectBuilt, ',') WITHIN GROUP (ORDER BY StepNumber) AS EarlyBuild
    FROM (
        SELECT GameId, StepNumber, ObjectBuilt,
               ROW_NUMBER() OVER (PARTITION BY GameId ORDER BY StepNumber) as rn
        FROM GameSteps
        WHERE MoveType IN ('Unit', 'Structure')
    ) ranked
    WHERE rn <= 3
    GROUP BY GameId
)
SELECT 
    EarlyBuild,
    COUNT(*) AS GameCount,
    AVG(CAST(g.TotalGameTime AS FLOAT)) AS AvgDuration,
    AVG(CAST(g.FinalMinerals + g.FinalGas AS FLOAT)) AS AvgFinalResources
FROM FirstMoves fm
JOIN Games g ON fm.GameId = g.GameId
GROUP BY EarlyBuild
HAVING COUNT(*) > 1
ORDER BY GameCount DESC;