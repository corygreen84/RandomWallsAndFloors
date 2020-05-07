using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWalls : MonoBehaviour
{

    // gets the locations //
    public List<Vector3> locations;
    private GameObject wallSide;
    private GameObject wallBack;


    // Start is called before the first frame update
    public void Start()
    {
        locations = new List<Vector3>();
        wallSide = Resources.Load<GameObject>("Floors and Walls " + 0 + "/Wall_side") as GameObject;
        wallBack = Resources.Load<GameObject>("Floors and Walls " + 0 + "/Wall_back") as GameObject;
    }

    // injecting all the locations //
    public void startCreatingWalls(List<Vector3> _locations) {
        this.locations = _locations;
        iterateThroughLocations();
    }

    void iterateThroughLocations()
    {

        //GameObject[] floors;
        //floors = GameObject.FindGameObjectsWithTag("floor");
        
        foreach (var i in this.locations)
        {
            bool toTheLeft = false;
            bool toTheRight = false;
            bool toTheBottom = false;
            bool toTheTop = false;
            foreach (var j in this.locations)
            {
                if (j != i)
                {
                    var heading = j - i;

 
                    // to the left of the tile //
                    if (heading.x == -10.0f && heading.z == 0.0f) {
                        toTheLeft = true;
                    }

                    // to the right of the tile //
                    if (heading.x == 10.0f && heading.z == 0.0f) {
                        toTheRight = true;
                    }

                    // to the top of the tile //
                    if (heading.x == 0.0f && heading.z == 10.0f) {
                        toTheTop = true;
                    }

                    // to the bottom of the tile //
                    if (heading.x == 0.0f && heading.z == -10.0f) {
                        toTheBottom = true;
                    }
                    
                }
            }

            //cDebug.Log("\n for Tile - " + i + " Left - " + toTheLeft + ", Right - " + toTheRight + ", Top - " + toTheTop + ", Bottom - " + toTheBottom);


            if (!toTheLeft) {
                setToLeftOfFloor(i);
            }

            if (!toTheRight) {
                setToRightOfFloor(i);
            }

            if (!toTheBottom) {
                setToBackOfFloor(i);
            }

            if (!toTheTop) {
                setToFrontOfFloor(i);
            }


        }


        

    }



    void setToLeftOfFloor(Vector3 location)
    {
        if (wallSide != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.x += -5.0f;
            locationOfWall.y += 2.5f;
            Instantiate(wallSide, locationOfWall, Quaternion.identity);
        }
    }

    void setToRightOfFloor(Vector3 location)
    {
        if (wallSide != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.x += 5.0f;
            locationOfWall.y += 2.5f;
            Instantiate(wallSide, locationOfWall, Quaternion.identity);
        }
    }

    void setToBackOfFloor(Vector3 location)
    {
        if (wallBack != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.y += 2.5f;
            locationOfWall.z += -5.0f;
            Instantiate(wallBack, locationOfWall, Quaternion.Euler(0, 90, 0));
        }
    }

    void setToFrontOfFloor(Vector3 location)
    {
        if (wallBack != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.y += 2.5f;
            locationOfWall.z += 5.0f;
            Instantiate(wallBack, locationOfWall, Quaternion.Euler(0, 90, 0));
        }
    }

}
