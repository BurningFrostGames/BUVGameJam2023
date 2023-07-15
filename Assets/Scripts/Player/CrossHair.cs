using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private RectTransform crossHair;

    public GameObject Player;

    public float aimSize = 25f;
    public float idleSize = 50f;
    public float walkSize = 75f;
    public float runJumpSize = 125f;
    public float currentSize = 50f;
    public float speed = 10f;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Vector3 mousePosition;
    public float moveSpeed = 1f;

    private void Update()
    {
        if (Aiming)
        {
            currentSize = Mathf.Lerp(currentSize, aimSize, Time.deltaTime * speed);
        }
        else if (Walking)
        {
            currentSize = Mathf.Lerp(currentSize, walkSize, Time.deltaTime * speed);
        }
        else if (Running)
        {
            currentSize = Mathf.Lerp(currentSize, runJumpSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, idleSize, Time.deltaTime * speed);
        }

        crossHair.sizeDelta = new Vector2(currentSize, currentSize);
        //mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        transform.position = Input.mousePosition;
        Cursor.visible = false;
    }

    private bool Walking
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    private bool Running
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public bool Aiming
    {
        get
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!Walking && !Running)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    //private void OnMouseEnter()
    //{
    //    Cursor.SetCursor(null, hotSpot, cursorMode);
    //    mousePosition = Input.mousePosition;
    //    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //    transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}
}