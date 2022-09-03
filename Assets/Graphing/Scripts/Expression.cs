using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression : MonoBehaviour
{
    public TogglesController tc; // to see which expression is disabled
    public GameObject uiForeground; // darkens when this expression is disabled
    public int expressionIndex;

    void Start()
    {
        
    }

    void Update()
    {
        // Check if current expression is disabled
        List<bool> toggleStatus = tc.getToggleStatus();
        if (toggleStatus[expressionIndex] == false)
        {
            uiForeground.SetActive(true);
        }
        else
        {
            uiForeground.SetActive(false);
        }
    }
}
