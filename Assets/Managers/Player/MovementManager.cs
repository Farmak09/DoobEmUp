using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : PlayerElement
{
    private PointerController pointer;
    public override void Awake()
    {
        type = TypeOfPlayerScripts.Movement;
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        player.inputManager.press.started += MouseDown;
        player.inputManager.press.canceled += MouseUp;

        pointer = GetComponentInChildren<PointerController>();
    }

    public override void PlayerUpdate()
    {
        if (player.stats.selected)
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

                player.stats.selected = true;
            }
        }
    }

    private void CursorVisibility(bool value)
    {
        Cursor.visible = value;
    }
    private float MousePosToGameUnits()
    {
        float ret = 12f * Input.mousePosition.x / Camera.main.scaledPixelWidth - 6f;
        if (ret < -6f || ret > 6f)
        { 
            ret = ret < 0 ? -6f : 6f;
            ClampMousePosition(ret);
        }
        pointer.SetPosition(ret);
        return ret;
    }

    private void ClampMousePosition(float border)
    {
        Vector2 newPos = new((border + 6f) * Camera.main.scaledPixelWidth / 12f, Input.mousePosition.y);
        Mouse.current.WarpCursorPosition(newPos);
    }

    private void MovePlayer()
    {
        float direction = MousePosToGameUnits() - transform.position.x;
        float speed = player.stats.GetSpeed(direction);
        UpdatePosition(speed);
        UpdateAnimation(speed);
        UpdatePointer(speed);
    }

    private void UpdatePointer(float speed)
    {
        pointer.Move(speed);
    }

    private void UpdatePosition(float speed)
    {
        transform.position += speed * Time.deltaTime * Vector3.right;
    }
    private void UpdateAnimation(float speed)
    {
        PlayerAnimationManager animator = (PlayerAnimationManager)player.FindScriptInList(TypeOfPlayerScripts.Animation);
        animator.UpdateMovementAnimationData(speed);
    }
    private void MouseUp(InputAction.CallbackContext context)
    {
        CursorVisibility(true);

        player.stats.selected = false;
    }
}
