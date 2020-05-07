using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorV1 : MonoBehaviour
{

    public int levelDepth = 0;

    public GameObject floor;
    private List<Vector3> listOfPositions;

    private CreateWalls createWalls;
    private LaydownTiles laydownTiles;



    // Start is called before the first frame update
    void Start()
    {

        listOfPositions = new List<Vector3>();
        // setting the floor object //

        // and adding that to the list of positions //
        Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity);
        listOfPositions.Add(new Vector3(0, 0, 0));

        // iterating through the level depths //
        iterateThroughLevelDepth(1);

        // laying down the tiles //
        layTilesDown();
    }



    // laying down the tiles then creating walls //
    void layTilesDown()
    {
        laydownTiles = gameObject.GetComponent<LaydownTiles>();
        bool returnValue = laydownTiles.loadInLocations(this.listOfPositions, floor);
        if (returnValue)
        {
            // pass off to creating the walls //
            createWalls = gameObject.GetComponent<CreateWalls>();
            createWalls.startCreatingWalls(this.listOfPositions);
        }

    }





    // iterating through the level depth //
    void iterateThroughLevelDepth(int startingPoint)
    {
        for (int i = startingPoint; i < levelDepth; i++)
        {
            try
            {
                pickARandomLocation();
            }
            catch
            {
                // if the placement gets boxed in, we go here //
                boxedIn(i);
                break;
            }
        }
    }



    void pickARandomLocation()
    {
        // checking the previous list of positions to see if they contain //
        // the new position //
        // choose a random location based off the previous location //
        Vector3 lastKnownPosition = listOfPositions[listOfPositions.Count - 1];
        Vector3 returnedRandomPosition = returnRandomVector3(lastKnownPosition);
        if (!listOfPositions.Contains(returnedRandomPosition))
        {
            listOfPositions.Add(returnedRandomPosition);
        }
        else
        {
            pickARandomLocation();
        }

    }







    void boxedIn(int leftOffAt)
    {
        // gives back a random location along the way //
        Vector3 randomPosition = listOfPositions[UnityEngine.Random.Range(0, listOfPositions.Count - 1)];
        Vector3 randomLocationBasedOnPosition = returnRandomVector3(randomPosition);
        if (!listOfPositions.Contains(randomLocationBasedOnPosition))
        {
            listOfPositions.Add(randomLocationBasedOnPosition);
            levelDepth--;
            iterateThroughLevelDepth(leftOffAt);
        }
        else
        {
            boxedIn(leftOffAt);
        }
    }








    // comes up with a random location of the next tile //
    Vector3 returnRandomVector3(Vector3 previousVector3)
    {
        int randomDirection = UnityEngine.Random.Range(0, 4);

        float xPosition = 0.0f;
        float zPosition = 0.0f;

        switch (randomDirection)
        {
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



}
