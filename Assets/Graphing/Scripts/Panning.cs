using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
    public Camera cam;
    public float sensitivity;

    bool mouseHeld;
    Vector2 currentMousePos;
    Vector2 lastMousePos;

    void Start()
    {
        currentMousePos = Input.mousePosition;
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        // Panning movement

        if (Input.GetMouseButtonDown(0))
        {
            mouseHeld = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseHeld = false;
        }

        if (mouseHeld)
        {
            lastMousePos = currentMousePos;
            currentMousePos = Input.mousePosition;

            Vector2 deltaMousePos = currentMousePos - lastMousePos;
            Vector2 newMousePos = new Vector2(cam.transform.position.x - deltaMousePos.x * sensitivity,
                cam.transform.position.y - deltaMousePos.y * sensitivity);
            cam.transform.position = newMousePos;
        }

        if (!mouseHeld)
        {
            currentMousePos = Input.mousePosition;
            lastMousePos = currentMousePos;
        }

        // Zooming movement

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize--;
            sensitivity -= .003f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize++;
            sensitivity += .003f;
        }
    }
}
