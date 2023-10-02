using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTextureHeld;
    public Vector2 cursorClick;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.SetCursor(cursorTexture, cursorClick, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorTextureHeld, cursorClick, CursorMode.ForceSoftware);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, cursorClick, CursorMode.ForceSoftware);
        }
        

    }
}
