# StarCraft Demo 4 - Build Order Simulator

A simplified C# console application that simulates StarCraft 2 Terran build orders and resource management.

## Overview

This project models the economic and build order aspects of StarCraft 2's Terran race, allowing for simulation and analysis of different build strategies through automated gameplay.

## Features

- **Resource Management**: Accurate mineral and gas harvesting simulation
- **Unit Production**: Build SCVs, Marines, Marauders, and other Terran units
- **Structure Construction**: Build Supply Depots, Barracks, Refineries, and other structures
- **Advanced Mechanics**: Terran-specific features like building lift/land and addon swapping
- **AI Player**: Automated decision-making that randomly selects from valid moves
- **Game Statistics**: Track resource counts, unit populations, and build timings

## Key Classes

- `Player`: Base class handling all building, training, and resource actions
- `AutoPlayer`: AI implementation that makes random decisions from available moves
- `State`: Game state management (resources, units, structures, time progression)
- `SingleGame`: Game execution engine and move processing
- `GameOptimizer`: Runs multiple simulation games and displays results

## Requirements

- .NET Framework 4.8
- Visual Studio 2019 or later (or any compatible C# compiler)

## Usage

1. Open `StarcraftDemo4Sln.sln` in Visual Studio
2. Build and run the project
3. The console application will run 10 automated games and display results

## Project Structure

- Core simulation files are in the `StarcraftDemo4/` directory
- Database-related files have been moved to `archive/` folder
- Web project components have been removed for simplicity

## Changes Made

This version has been simplified from the original:
- Removed Entity Framework and database dependencies
- Removed ASP.NET MVC web interface
- Cleaned up project files and references
- Streamlined for console-only operation
- Updated to target .NET Framework 4.8

## Running Simulations

The `Test.cs` file contains the main entry point. By default, it runs 10 simulation games and displays:
- Game completion status
- Final resource counts (minerals/gas)
- Unit population statistics
- Total number of moves performed
- Game duration in seconds