using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    //info of the types of inputs you have set up
    private InputSystem_Actions userInputs;

    //mouse inputs for the player
    public InputAction press;

    //menu inputs from the UI script
    public InputAction pause;


    private void Awake()
    {
        userInputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        press = userInputs.Player.Press;

        pause = userInputs.UI.Pause;


        press.Enable();

        pause.Enable();
        

    }
    private void OnDestroy()
    {
        press.Disable();

        pause.Disable();
       
    }

}
