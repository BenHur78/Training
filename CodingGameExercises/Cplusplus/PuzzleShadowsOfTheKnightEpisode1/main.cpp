#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <cassert>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    int w; // width of the building.
    int h; // height of the building.
    cin >> w >> h; cin.ignore();
    int n; // maximum number of turns before game over.
    cin >> n; cin.ignore();
    int x0;
    int y0;
    cin >> x0 >> y0; cin.ignore();

    int currentTopLeftX = 0;
    int currentTopLeftY = 0;
    int currentBottomLRightX = w - 1;
    int currentBottomLRightY = h - 1;

    int x = x0; //the location of the next window Batman should jump to.
    int y = y0;

    // game loop
    while (1) {
        string bomb_dir; // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
        cin >> bomb_dir; cin.ignore();

        // Write an action using cout. DON'T FORGET THE "<< endl"
        // To debug: cerr << "Debug messages..." << endl;

        //calculate the new window
        if (bomb_dir == "U")
        {
            currentTopLeftX = x;
            //currentTopLeftY = 0;
            currentBottomLRightX = x;
            currentBottomLRightY = y - 1;
        }

        if (bomb_dir == "UR")
        {
            currentTopLeftX = x + 1;
            //currentTopLeftY = 0;
            //currentBottomLRightX = w-1;
            currentBottomLRightY = y - 1;
        }

        if (bomb_dir == "R")
        {
            currentTopLeftX = x + 1;
            currentTopLeftY = y;
            //currentBottomLRightX = w-1;
            currentBottomLRightY = y;
        }

        if (bomb_dir == "DR")
        {
            currentTopLeftX = x + 1;
            currentTopLeftY = y + 1;
            //currentBottomLRightX = w-1;
            //currentBottomLRightY = h-1;
        }

        if (bomb_dir == "D")
        {
            currentTopLeftX = x;
            currentTopLeftY = y + 1;
            currentBottomLRightX = x;
            //currentBottomLRightY = h-1;
        }

        if (bomb_dir == "DL")
        {
            //currentTopLeftX = 0;
            currentTopLeftY = y + 1;
            currentBottomLRightX = x - 1;
            //currentBottomLRightY = h-1;
        }

        if (bomb_dir == "L")
        {
            //currentTopLeftX = 0;
            currentTopLeftY = y;
            currentBottomLRightX = x - 1;
            currentBottomLRightY = y;
        }

        if (bomb_dir == "UL")
        {
            //currentTopLeftX = 0;
            //currentTopLeftY = 0;
            currentBottomLRightX = x - 1;
            currentBottomLRightY = y - 1;
        }

        //vertical window
        if (currentTopLeftX == currentBottomLRightX)
        {
            x = currentTopLeftX;
            y = (currentBottomLRightY + currentTopLeftY) / 2;
        }
        //horizontal window
        else if (currentTopLeftY == currentBottomLRightY)
        {
            x = (currentBottomLRightX + currentTopLeftX) / 2;
            y = currentTopLeftY;
        }
        else
        {
            x = (currentBottomLRightX + currentTopLeftX) / 2;
            y = (currentBottomLRightY + currentTopLeftY) / 2;
        }

        // the location of the next window Batman should jump to.
        //cout << "0 0" << endl;
        cout << x << " " << y << endl;

        cerr << "bomb_dir: " << bomb_dir << endl;
        cerr << "maximum number of turns: " << n << endl;
        cerr << "starting position x0: " << x0 << " y0:" << y0 << endl;
        cerr << "w: " << w << " h: " << h << endl;
        cerr << "currentTopLeftX: " << currentTopLeftX << " currentTopLeftY: " << currentTopLeftY << endl;
        cerr << "currentBottomLRightX: " << currentBottomLRightX << " currentBottomLRightY: " << currentBottomLRightY << endl;

        assert(currentTopLeftX >= 0 && currentTopLeftX <= w - 1);
        assert(currentTopLeftY >= 0 && currentTopLeftY <= h - 1);
        assert(currentBottomLRightX >= 0 && currentBottomLRightX <= w - 1);
        assert(currentBottomLRightY >= 0 && currentBottomLRightY <= h - 1);
    }
}