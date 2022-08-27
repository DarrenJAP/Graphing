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
}
