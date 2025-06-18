-- 1) Make the databse
Set NoCount On -- Turns off the annoying "One Row affected" messages 
USE [master]
GO
If Exists(Select Name from master.Sys.databases where name = 'SC2ResultsGames')
 Begin 
	Drop Database[SC2ResultsGames]
 End 
Go
 
CREATE DATABASE [SC2ResultsGames]
GO

USE [SC2ResultsGames]
GO

-- 2) Make the tables
If Exists(Select Name from master.Sys.databases where name = 'Games')
 Begin 
	Drop Table[Games]
 End 
GO 
If Exists(Select Name from master.Sys.databases where name = 'Results')
 Begin 
	Drop Table[Results]
 End 
GO 
--PRIMARY KEY 
CREATE TABLE [dbo].[Games]
(
    [Keyy] [int] PRIMARY KEY IDENTITY,
	[GameId] [int] NOT NULL ,
	[Move] [varchar](100) NOT NULL,
)
GO

CREATE TABLE [dbo].[Results]
(
	[GameId] [int] NOT NULL PRIMARY KEY,
	[Minerals] [int] NOT NULL,
	[Gas] [int] NOT NULL,
	[Marines] [int] NOT NULL,
	[Marauder] [int] NOT NULL,

	[SCV] [int] NOT NULL,
    [Time] [int] NOT NULL,
	[Refinery] [int] NOT NULL,
	[CommandCenter] [int] NOT NULL,
	[Barracks] [int] NOT NULL,
	
	[TechLab] [int] NOT NULL,
	[Reactor] [int] NOT NULL,
	[SupplyDepot] [int] NOT NULL,
	[MoveList] VARBINARY(MAX) NOT NULL,
)
GO

--CREATE TABLE [dbo].[EmployeeProjectHours]
--(
--	[EmployeeId] [int] NOT NULL,
--	[ProjectId] [int] NOT NULL,
--	[Date] [datetime] NOT NULL,
--	[Hours] [decimal](18, 2) NOT NULL,
--	CONSTRAINT [PK_EmployeeProjectHours] PRIMARY KEY CLUSTERED 
--		([EmployeeId] ASC,[ProjectId] ASC,[Date] ASC)
--)
--GO


If Exists(Select Name from SC2ResultsGames.Sys.Objects where name = 'pInsGame')
 Begin 
	Drop Proc pInsGame
 End 
GO 
Create Proc pInsGame
( @GameId int,@Move varchar(100))
  AS
  Begin
    Declare @RC int = 0
    Begin Try  
		Insert into [dbo].[Games] Values (@GameId, @Move)
	Set @RC = 100 -- Indicates Success			
    End Try
    Begin Catch
		Set @RC = -100 --Indicates Error			
    End Catch	
    Return @RC
  End	
GO

Create Proc pInsResult
( @GameId int, @Minerals int, @Gas int, @Marines int, @Marauder int, 
@SCV int, @Time int, @Refinery int, @CommandCenter int, 
@Barracks int, @TechLab int, @Reactor int, @SupplyDepot int, @MoveList VARBINARY(MAX))
  AS
  Begin
    Declare @RC int = 0
    Begin Try   
		Insert into [dbo].[Results] Values (@GameId, @Minerals, @Gas, @Marines, @Marauder, 
		@SCV, @Time, @Refinery, @CommandCenter, @Barracks, @TechLab, @Reactor, @SupplyDepot,
		@MoveList)
	Set @RC = 100 -- Indicates Success			
    End Try
    Begin Catch
		Set @RC = -100 --Indicates Error		
    End Catch	
    Return @RC
  End	
GO

Create Proc pFindMarineMoves
  AS
    Declare @RC int = 0, @MMarine int, @MGameId int
    Begin Try
		Select @MMarine = MAX(Marines) from Results
		Select @MGameId = GameId from Results
		where [Marines]=@MMarine
  		Select Move from Games 	
  		where [GameId]= @MGameId 
		Set @RC = 100 -- Indicates Success	
    End Try
    Begin Catch
		Set @RC = -100 --Indicates Error
    End Catch	
    Return @RC
GO

Exec pFindMarineMoves 

If Exists(Select Name from SC2ResultsGames.Sys.Objects where name = 'pFindMarineGameId')
 Begin 
	Drop Proc pFindMarineGameId
 End 
GO 

Alter Proc pFindMarineGameId
--(@MMarine int output)
AS
Declare @MMarine int

	Select @MMarine = MAX(Marines) from Results
	Select GameId from Results 
	where [Marines]=@MMarine 
	--Select @MGameId = 
	--GameId from Results
	--where [Marines] = @MMarine
GO

--DECLARE @var int
--Exec pFindMarineGameId @MMarine = @var output
--select @var
delete  from Results
delete  from Games
Declare @MMin int
	Select @MMin = MAX(Minerals) from Results
	Select GameId from Results 
	where [Minerals]=@MMin
	
select * from Results 
where GameId=106

select * from Results