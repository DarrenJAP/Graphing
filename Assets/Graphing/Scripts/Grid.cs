using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Grid : MonoBehaviour
{
    public List<TextMeshPro> textY;
    public List<TextMeshPro> textX;

    public int upperY, lowerY;
    public int leftX, rightX;

    public bool hideY;
    public bool hideX;

    public List<LineRenderer> lineRenderers;
    public TogglesController toggleController;

    public float precision;

    void Start()
    {
        // Assign values to TMP objects
        // Hide axis values if chosen to

        for (int i = 0; i < textY.Count; i++)
        {
            if (hideY)
            {
                textY[i].gameObject.SetActive(false);
            }
            else
            {
                textY[i].text = (upperY - i).ToString();
            }
        }

        for (int i = 0; i < textX.Count; i++)
        {
            if (hideX)
            {
                textX[i].gameObject.SetActive(false);
            }
            else
            {
                textX[i].text = (leftX + i).ToString();
            }
        }
    }

    void Update()
    {
        // Disables line that are also disabled in the expression panel

        List<bool> toggleStatus = toggleController.getToggleStatus();

        for (int i = 0; i < toggleStatus.Count; i++)
        {
            lineRenderers[i].gameObject.SetActive(toggleStatus[i]);
        }

        // Precision 1 = one point every 1 meter
        // Precision 0.5 = one point every 0.5 meter
        // and so on...

        if (precision > 0)
        {
            // Loop over all the line renderers (different colors)
            for (int i = 0; i < lineRenderers.Count; i++) 
            {
                // If current line renderer is activated by the user
                if (lineRenderers[i].gameObject.activeInHierarchy)
                {
                    // A value ranging from 0 to 11 (one grid X length)
                    // Will be incremented with precision value
                    // Each increment will have its own point
                    float currentIncrementLocation = 0;

                    // Used to add value to the positions array
                    // Ranging from 0 to 13 because our grid stops halfway through a cell
                    // 0 is grid starting X - 0.5f
                    // 13 is grid ending X + 0.5f
                    int currentLocation = 1;

                    Vector3[] positions = new Vector3[(int)(11 / precision) + 2];

                    // Add a point halfway through a cell behind the grid starting X
                    positions[0] = new Vector3(leftX - 0.5f + 2, Mathf.Pow(leftX - 0.5f, 2), 70);

                    while (currentIncrementLocation < 11)
                    {
                        positions[currentLocation] = new Vector3(leftX + currentIncrementLocation + 2, Mathf.Pow(leftX + currentIncrementLocation, 2), 70);

                        currentIncrementLocation += precision;
                        currentLocation += 1;
                    }

                    // Add a point halfway through a cell infront of the grid ending X
                    positions[(int)(11 / precision) + 1] = new Vector3(leftX + 11 + 0.5f + 2, Mathf.Pow(leftX + 11 + 0.5f, 2), 70);

                    lineRenderers[i].positionCount = positions.Length;
                    lineRenderers[i].SetPositions(positions);
                }
            }
        }
    }
}
