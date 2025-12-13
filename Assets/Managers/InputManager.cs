using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    //info of the types of inputs you have set up
    private InputSystem_Actions userInputs;

    //moving inputs from the action script
    public InputAction move;

    //menu inputs from the UI script
    public InputAction pause;


    private void Awake()
    {
        userInputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = userInputs.Player.Move;

        pause = userInputs.UI.Pause;

       
        move.Enable();

        pause.Enable();
        

    }
    private void OnDestroy()
    {
        move.Disable();

        pause.Disable();
       
    }

}
