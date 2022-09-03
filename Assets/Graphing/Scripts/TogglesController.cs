using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TogglesController : MonoBehaviour
{
    public int toggleAmount;
    List<bool> toggleStatus;

    void Start()
    {
        toggleStatus = Enumerable.Repeat(false, toggleAmount).ToList();
    }

    public void toggleButton(int buttonIndex)
    {
        toggleStatus[buttonIndex] = !toggleStatus[buttonIndex];

        for (int i = 0; i < toggleStatus.Count; i++)
        {
            Debug.Log("Button " + (i + 1).ToString() + ": " + toggleStatus[i]);
        }
    }

    public List<bool> getToggleStatus()
    {
        return toggleStatus;
    }
}
