using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generator : MonoBehaviour
{
    // dimensions of the level //
    public int levelDepth = 0;

    // how many branches
    public int levelBranches = 0;

    // how long those branches are //
    public int levelBranchesDepth = 0;

    // the style of the level //
    public int levelPalette = 0;




    private GameObject floor;

    private List<Vector3> listOfPositions;



    // Start is called before the first frame update
    void Start()
    {
        // where it all begins... //
        startPlacement();
    }



    void startPlacement() {
        listOfPositions = new List<Vector3>();

        // array of floors and walls //
        floor = Resources.Load<GameObject>("Floors and Walls " + levelPalette + "/Floor") as GameObject;

        // creating the first tile at 0, 0, 0 //
        Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity);

        // the first position of the array of positions is 0, 0, 0 //
        listOfPositions.Add(new Vector3(0, 0, 0));

        // laying out the main path //
        for (int i = 1; i < levelDepth; i++)
        {
            // using a try catch method to make sure the tiles dont box themselves in //
            try
            {
                pickRandomSpotAndPlaceTileDown();
            }
            catch {
                // a boxed in scenario //
                
                break;
            }
            
        }

        // laying out the individual branches //
        for (int j = 0; j < levelBranches; j++)
        {
            createRandomBranch();
        }

    }













    void pickRandomSpotAndPlaceTileDown() {
        bool isAlreadyInPlace = false;

        Vector3 lastKnownPosition = listOfPositions[listOfPositions.Count - 1];

        Vector3 returnedRandomPosition = returnRandomVector3(lastKnownPosition);
        
        for (int j = 0; j < listOfPositions.Count; j++)
        {
            Vector3 takenPosition = listOfPositions[j];
            if (takenPosition == returnedRandomPosition) {

                // doing some calculations if the floor placed down is boxed in //
                isAlreadyInPlace = true;
            }
        }

        // if there isnt a tile already in that place then we set one down //
        if (!isAlreadyInPlace)
        {
            Instantiate(floor, returnedRandomPosition, Quaternion.identity);
            // the first position of the array of positions is 0, 0, 0 //
            listOfPositions.Add(returnedRandomPosition);
        }

        // if there is a tile already in that place then we should do a random position again //
        else
        {
            pickRandomSpotAndPlaceTileDown();
        }

        // Debug.Log("in a jam? " + inAJam);
        
        Debug.Log("number of places " + listOfPositions.Count);
    }








    void createRandomBranch() {


    }
















    // comes up with a random location of the next tile //
    Vector3 returnRandomVector3(Vector3 previousVector3) {
        int randomDirection = UnityEngine.Random.Range(0, 4);

        float xPosition = 0.0f;
        float zPosition = 0.0f;

        switch (randomDirection) {
            case 0:
                xPosition = -10.0f;
                break;
            case 1:
                xPosition = 10.0f;
                break;
            case 2:
                zPosition = -10.0f;
                break;
            case 3:
                zPosition = 10.0f;
                break;
            default:
                break;
        }

        float newXPosition = previousVector3.x + xPosition;
        float newZPosition = previousVector3.z + zPosition;

        Vector3 returnVector3 = new Vector3(newXPosition, 0, newZPosition);
        return returnVector3;
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
