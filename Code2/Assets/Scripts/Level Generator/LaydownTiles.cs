using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaydownTiles : MonoBehaviour { 

    public List<Vector3> listOfLocations;
    public Material material;
    private GameObject floor;

    private float lowestX = 0.0f;
    private float highestX = 0.0f;
    private float lowestZ = 0.0f;
    private float highestZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // takes in locations and the floor //
    public bool loadInLocations(List<Vector3> locations, GameObject floor) {
        this.listOfLocations = locations;
        this.floor = floor;
        return this.layTileDown(this.listOfLocations, this.floor);
    }
    

    bool layTileDown(List<Vector3> locations, GameObject _floor)
    {
        Debug.Log("amount of tiles " + this.listOfLocations.Count);

        bool done = false;
        // we start at 1 because 0 has already been laid down //
        for (int k = 1; k < locations.Count; k++)
        {
            if (_floor != null)
            {
                if (locations[k].x <= lowestX) {
                    lowestX = locations[k].x;
                }

                if (locations[k].x >= highestX) {
                    highestX = locations[k].x;
                }

                if (locations[k].z <= lowestZ) {
                    lowestZ = locations[k].z;
                }

                if (locations[k].z >= highestZ) {
                    highestZ = locations[k].z;
                }

                

                Instantiate(_floor, locations[k], Quaternion.identity);
            }



            if (k == locations.Count - 1)
            {
                // return true;
                done = true;
            }
            else {
                done = false;
            }
        }
        return done;
        
    }

    
}
