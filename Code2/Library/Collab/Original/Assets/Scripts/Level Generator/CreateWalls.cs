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

        GameObject[] floors;
        floors = GameObject.FindGameObjectsWithTag("floor");
        
        foreach (var i in this.locations)
        {
            
            foreach (var j in this.locations)
            {
                if (j != i)
                {
                    var heading = j - i;
                    var distance = heading.magnitude;
                    var direction = heading / distance;
                    if (!float.IsNaN(direction.z) && (!float.IsNaN(direction.x)))
                    {



                        // placement to the right //
                        if (heading.x > 0.0f && heading.x < 10.1f && heading.x != 0.0f)
                        {
                            if (heading.z == 0.0f)
                            {
                                Debug.Log(i + " placement to the right of " + j);
                                atRight = true;

                            }
                        }

                        // placement to the left //
                        else if (heading.x < 0.0f && heading.x > -10.1f && heading.x != 0.0f)
                        {
                            if (heading.z == 0.0f)
                            {
                                Debug.Log(i + " placement to the left of " + j);
                                atLeft = true;
                            }
                        }

                        

                        if (heading.z > 0.0f && heading.z < 10.1f && heading.z != 0.0f)
                        {
                            if (heading.x == 0.0f)
                            {
                                Debug.Log(i + " placement to the top of " + j);
                                atTheTop = true;
                            }
                        }

                        else if (heading.z < 0.0f && heading.z > -10.1f && heading.z != 0.0f) {
                            if (heading.x == 0.0f) {
                                Debug.Log(i + " placement to the bottom of " + j);
                                atTheBottom = true;
                            }
                        }


                    }
                }
            }

            if (!atTheTop) {
                setToFrontOfFloor(i);
            }
            

            



        }
        

    }



    void setToLeftOfFloor(Vector3 location)
    {
        if (wallSide != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.x = -5.0f;
            locationOfWall.y = 2.5f;
            Instantiate(wallSide, locationOfWall, Quaternion.identity);
        }
    }

    void setToRightOfFloor(Vector3 location)
    {
        if (wallSide != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.x = 5.0f;
            locationOfWall.y = 2.5f;
            Instantiate(wallSide, locationOfWall, Quaternion.identity);
        }
    }

    void setToBackOfFloor(Vector3 location)
    {
        if (wallBack != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.y = 2.5f;
            locationOfWall.z = 5.0f;
            Instantiate(wallBack, locationOfWall, Quaternion.Euler(0, 90, 0));
        }
    }

    void setToFrontOfFloor(Vector3 location)
    {
        if (wallBack != null)
        {
            Vector3 locationOfWall = location;
            locationOfWall.y = 2.5f;
            locationOfWall.z = -5.0f;
            Instantiate(wallBack, locationOfWall, Quaternion.Euler(0, 90, 0));
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
