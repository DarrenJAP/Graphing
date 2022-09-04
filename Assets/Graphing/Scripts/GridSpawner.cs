using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject gridPrefab;
    public Camera cam;

    public float precision = 1f;

    public TogglesController togglesController;

    List<Vector2> gridsSpawned;
    List<Vector2> gridsStandby;

    void Awake()
    {
        gridsSpawned = new List<Vector2>() { new Vector2 (0f, 0f) };
        gridsStandby = new List<Vector2>();
    }

    void Update()
    {
        // Check neighboring grids not spawned yet
        for (int i = 0; i < gridsSpawned.Count; i++)
        {
            // All sides of existing spawned grids to standby spawn new grid when user pans there

            Vector2 north = new Vector2(gridsSpawned[i].x,gridsSpawned[i].y + 11);
            Vector2 west = new Vector2(gridsSpawned[i].x - 11, gridsSpawned[i].y);
            Vector2 east = new Vector2(gridsSpawned[i].x + 11, gridsSpawned[i].y);
            Vector2 south = new Vector2(gridsSpawned[i].x, gridsSpawned[i].y -11);

            Vector2 northWest = new Vector2(gridsSpawned[i].x - 11, gridsSpawned[i].y + 11);
            Vector2 northEast = new Vector2(gridsSpawned[i].x + 11, gridsSpawned[i].y + 11);
            Vector2 southWest = new Vector2(gridsSpawned[i].x - 11, gridsSpawned[i].y - 11);
            Vector2 southEast = new Vector2(gridsSpawned[i].x + 11, gridsSpawned[i].y - 11);

            List<Vector2> allSides = new List<Vector2>() { north, west, east, south, 
                northWest, northEast, southWest, southEast };

            for (int x = 0; x < allSides.Count; x++)
            {
                // if grid not already added into the scene
                if (gridsSpawned.FindIndex(item => allSides[x].x == item.x && allSides[x].y == item.y) < 0)
                { 
                    // if grid not already added in gridsStandby
                    if (gridsStandby.FindIndex(item => allSides[x].x == item.x && allSides[x].y == item.y) < 0)
                    {
                        gridsStandby.Add(allSides[x]);
                    }
                }
            }
        }

        // Grid spawning candidates

        if (gridsStandby.Count > 0)
        {
            for (int i = 0; i < gridsStandby.Count; i++)
            {
                Vector3 newGridViewportPoint = cam.WorldToViewportPoint(new Vector3(gridsStandby[i].x, gridsStandby[i].y, 0));

                // If user can see center of candidate grid

                if (newGridViewportPoint.x > 0 && newGridViewportPoint.x < 1 &&
                    newGridViewportPoint.y > 0 && newGridViewportPoint.y < 1)
                {
                    // Spawn grid prefab with correct x y values

                    GameObject go = (GameObject)Instantiate(gridPrefab, new Vector3(gridsStandby[i].x, gridsStandby[i].y, 0), new Quaternion(0f, 0f, 0f, 0f));
                    go.GetComponent<Grid>().leftX = (int)gridsStandby[i].x - 5;
                    go.GetComponent<Grid>().rightX = (int)gridsStandby[i].x + 5;
                    go.GetComponent<Grid>().lowerY = (int)gridsStandby[i].y - 5;
                    go.GetComponent<Grid>().upperY = (int)gridsStandby[i].y + 5;

                    if (gridsStandby[i].y != 0)
                        go.GetComponent<Grid>().hideX = true;

                    if (gridsStandby[i].x != 0)
                        go.GetComponent<Grid>().hideY = true;

                    go.GetComponent<Grid>().precision = precision;
                    go.GetComponent<Grid>().toggleController = togglesController;

                    gridsSpawned.Add(gridsStandby[i]);
                }
            }

            // Clear spawning candidate grids
            gridsStandby = new List<Vector2>();
        }
    }
}
