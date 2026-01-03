using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : PlayerElement
{
    // Start is called before the first frame update
    private void Start()
    {
        inputManager.press.started += MouseDown;
        inputManager.press.canceled += MouseUp;
    }

    public override void GameUpdate()
    {
        if (stats.Controlled)
        {
            MovePlayer();
        }
    }


    private void MouseDown(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, LayerMask.GetMask("Puppeteer")))
        {
            if (raycastHit.transform != null)
            {
                CursorVisibility(false);

                stats.Controlled = true;
            }
        }
    }

    private void CursorVisibility(bool value)
    {
        Cursor.visible = value;
    }
    private float MousePosToGameUnits()
    {
        float ret = 10f * Input.mousePosition.x / Camera.main.scaledPixelWidth - 5f;
        if (ret < -5f) ret = -5f;
        if (ret > 5f) ret = 5f;

        return ret;
    }
    private void MovePlayer()
    {
        float direction = MousePosToGameUnits() - transform.position.x;
        UpdatePosition(stats.GetSpeed(direction));
    }

    private void UpdatePosition(float speed)
    {
        transform.position += speed * Time.deltaTime * Vector3.right;
    }

    private void MouseUp(InputAction.CallbackContext context)
    {
        CursorVisibility(true);

        stats.Controlled = false;
    }
}
