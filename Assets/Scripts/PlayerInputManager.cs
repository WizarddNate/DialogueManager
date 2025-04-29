using NatesLibrary.Dialogue;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    public static Vector2 PlayerMovement;

    private PlayerInput playerInput; 
    private InputAction moveAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
            
        moveAction = playerInput.actions["Move"];
    }

    void Update()
    {
        //read player movement
        PlayerMovement = moveAction.ReadValue<Vector2>();

        //allows the dialogue to advance whenever the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            PromptAdvance();
    }

    public void PromptAdvance()
    {
        DialogueManager.instance.OnUserPrompt_Next();
    }
    
}
