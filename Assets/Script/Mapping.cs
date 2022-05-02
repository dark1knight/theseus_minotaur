using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapping : MonoBehaviour
{ //previous String tile 1,1,1,1,1,1;1,0,0,0,0,1;1,0,0,0,0,1;1,0,0,1,0,2;1,0,0,1,0,1;1,1,1,1,1,1
    public Tilemap maze;
    public int MapDimentsion;// Dimension of The Grid
    public static Mapping Instance;
    public int[,] Map; // the grid used to map Floor = 0, Wall = 1, Exit = 2
    public string MapString; // string used to form the map at the awake
    
    void Awake()
    {
        maze = gameObject.GetComponent<Tilemap>();
        SetMapString();
        Map = new int[MapDimentsion, MapDimentsion];
        TranslateMapString();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void reverseString() { //reverses the resulted lines of the automated mapString, since the string is being generated from bottom to top rather then the desired top to bottom
        string[] lines = MapString.Split(';');
        MapString = "";
        for (int index = lines.Length - 1; index >= 0; index--) {
            MapString += lines[index] + ";";
        }
        MapString = MapString.Remove(MapString.Length - 1);
    }


    public void SetMapString() {
        
        int numberOfCells = 0;// always nxn grid

        foreach (var TilePosition in maze.cellBounds.allPositionsWithin) //first iteration in order to get the MapDimension so that the dimension doe not need to be set manually.
        {
            numberOfCells++;
        }
        MapDimentsion = (int)Mathf.Sqrt(numberOfCells);

        int indexOfCell = 0;
        foreach (var TilePosition in maze.cellBounds.allPositionsWithin) { // second iteration in order to set the string of the grid
            numberOfCells++;
            if (!maze.HasTile(TilePosition))
            {
                continue;
            }
            else {
                if (maze.GetTile(TilePosition).name == "Floor")
                {
                    MapString += "0,";
                    indexOfCell++;
                }
                else if (maze.GetTile(TilePosition).name == "Wall")
                {
                    MapString += "1,";
                    indexOfCell++;
                }
                else if (maze.GetTile(TilePosition).name == "Exit") {
                    MapString += "2,";
                    indexOfCell++;
                }
                if (indexOfCell % MapDimentsion == 0) {
                    MapString = MapString.Remove(MapString.Length - 1);//in case its the last collum of a line, remove the last char, which is ',' from the string and add ';'
                    MapString += ";";
                }
            }
        }
        MapString = MapString.Remove(MapString.Length - 1); //removes the last ';' since its unecessary
        reverseString();
    }
    private void TranslateMapString() {// function used to transform the string used to form the grid. using ';' to separate arrays and ',' individual cells 
        string[] separateLineString = new string[MapDimentsion];       
        separateLineString = MapString.Split(';');

        for (int indexX = 0; indexX < MapDimentsion; indexX++) {

            string[] separatedCells = separateLineString[indexX].Split(',');
            for (int IndexY = 0; IndexY < MapDimentsion; IndexY++) {
                Map[IndexY, indexX] = int.Parse(separatedCells[IndexY]);
                
            }
        }
    }
}
