#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <queue>

using namespace std;

void initGrid(char** grid, int gridWidth, int gridHeight, int lineHeight, string line);
void printGrid(char** grid, int gridWidth, int gridHeight);

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
int main()
{
    int width; // the number of cells on the X axis
    cin >> width; cin.ignore();
    int height; // the number of cells on the Y axis
    cin >> height; cin.ignore();

    cerr << "width: " << width << " height: " << height << endl;

    //creating the grid
    char** grid = new char* [height];
    for (int h = 0; h < height; h++)
    {
        grid[h] = new char[width];
    }

    for (int i = 0; i < height; i++) {
        string line;
        getline(cin, line); // width characters, each either 0 or .
        cerr << "Line: " << line << endl;
        initGrid(grid, width, height, i, line); //grid initialization
    }

    printGrid(grid, width, height);

    string coordinatesSeparator = " ";
    string neighborDoesNotExistCoordinates = to_string(-1) + coordinatesSeparator + to_string(-1);
    char cellWithNode = '0';

    //to hold the results
    queue<string> resultsQueue;

    // Write an action using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    //the main double loop
    for (int h = 0; h < height; h++)
    {
        for (int w = 0; w < width; w++)
        {
            string nodeCellCoordinates = "";
            string rightCellCoordinates = "";
            string bottomCellCoordinates = "";
            string result = "";

            //If its a node...
            if (grid[h][w] == cellWithNode)
            {
                //horizontal neighbor
                bool horizontalNeighBorFound = false;
                int w1 = w + 1;
                while (!horizontalNeighBorFound && w1 < width)
                {
                    if (grid[h][w1] == cellWithNode)
                    {
                        horizontalNeighBorFound = true;
                        rightCellCoordinates = std::to_string(w1) + coordinatesSeparator + std::to_string(h);
                    }

                    w1++;
                }

                //vertical neighbor
                bool verticalNeighBorFound = false;
                int h1 = h + 1;
                while (!verticalNeighBorFound && h1 < height)
                {
                    if (height && grid[h1][w] == cellWithNode)
                    {
                        verticalNeighBorFound = true;
                        bottomCellCoordinates = std::to_string(w) + coordinatesSeparator + std::to_string(h1);
                    }

                    h1++;
                }

                result = to_string(w) + coordinatesSeparator + to_string(h);
                if (horizontalNeighBorFound & verticalNeighBorFound)
                {
                    result += coordinatesSeparator + rightCellCoordinates + coordinatesSeparator + bottomCellCoordinates;
                }
                else if (horizontalNeighBorFound & !verticalNeighBorFound)
                {
                    result += coordinatesSeparator + rightCellCoordinates + coordinatesSeparator + neighborDoesNotExistCoordinates;
                }
                else if (!horizontalNeighBorFound & verticalNeighBorFound)
                {
                    result += coordinatesSeparator + neighborDoesNotExistCoordinates + coordinatesSeparator + bottomCellCoordinates;
                }
                else if (!horizontalNeighBorFound & !verticalNeighBorFound)
                {
                    result += coordinatesSeparator + neighborDoesNotExistCoordinates + coordinatesSeparator + neighborDoesNotExistCoordinates;
                }

                resultsQueue.push(result);
            }
        }
    }


    // Three coordinates: a node, its right neighbor, its bottom neighbor
    //cout << "0 0 1 0 0 1" << endl;
    while (!resultsQueue.empty())
    {
        cout << resultsQueue.front() << endl;
        resultsQueue.pop();
    }
}

void initGrid(char** grid, int gridWidth, int gridHeight, int lineHeight, string line)
{
    for (int w = 0; w < gridWidth; w++)
    {
        grid[lineHeight][w] = line[w];
    }
}

void printGrid(char** grid, int gridWidth, int gridHeight)
{
    cerr << "Starting printing the grid..." << endl;

    for (int h = 0; h < gridHeight; h++)
    {
        for (int w = 0; w < gridWidth; w++)
        {
            cerr << grid[h][w];
        }

        cerr << endl;
    }

    cerr << "Ending printing the grid." << endl;
}