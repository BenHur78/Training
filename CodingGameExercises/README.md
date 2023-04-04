# Introduction 
This folder purpose is to solve Codingame exercises.

## Puzzle - There is no spoon - Episode 1
- What I will learn
    - In this puzzle, you have to detect special characters from a string. You also have to store input values into a grid to explore it. You have to go through all elements from a grid (certainly using a double loop) and from those points, iterate again on some elements of the grid. Solving this puzzle make you learn the concept of nested loop.
- Learning Opportunities
    - Lists
- Statement
    - The goal is to find, when they exist, the horizontal and vertical neighbors nodes from a two dimensional array. The difficulty is in the number of nested loops that this puzzle can make you write. Do not get lost!
- Description
    - The game is played on a rectangular grid with a given size. Some cells contain power nodes. The rest of the cells are empty.The goal is to find, when they exist, the horizontal and vertical neighbors of each node.
- Rules
    - To do this, you must find each (x1,y1) coordinates containing a node, and display the (x2,y2) coordinates of the next node to the right, and the (x3,y3) coordinates of the next node to the bottom within the grid. If a neighbor does not exist, you must output the coordinates -1 -1 instead of (x2,y2) and/or (x3,y3).
- What I have Learned
    - Lists
- Folder location:
_..\CodingGameExercises\Cplusplus\PuzzleThereIsNoSpoonEpisode1_

## Puzzle - Shadows of the Knight - Episode 1
- Description
    - You will look for the hostages on a given building by jumping from one window to another using your grapnel gun. Your goal is to jump to the window where the hostages are located in order to disarm the bombs. Unfortunately, you have a limited number of jumps before the bombs go off...
- Rules
    - Before each jump, the heat-signature device will provide you with the direction of the bombs based on your current position: U (Up) / UR / R (Right) / DR / D (Down) / DL/ L (Left) / UP
- What I have Learned
    - Binary Search
    - Intervals
- Folder location:
_..\CodingGameExercises\Cplusplus\PuzzleShadowsOfTheKnightEpisode1_


# How To's

## Using Visual Studio 2022 (VS) to run c++
1. Open VS
2. Click _Continue without code_
3. Click _File-> Open -> CMAKE..._
