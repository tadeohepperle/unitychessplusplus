using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    // Update is called once per frame
    bool mouseDown = false;
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var obj = hit.transform.gameObject;
            ChessPieceRepresentation cpr = obj.GetComponent<ChessPieceRepresentation>();
            if (cpr != null)
            {
                cpr.OnHover();
                if (Input.GetMouseButtonDown(0) && !mouseDown)
                {
                    mouseDown = true;
                    cpr.OnClicked();
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) mouseDown = false;
    }
}
