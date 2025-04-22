using NatesLibrary.Dialogue;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            PromptAdvance();
    }

    public void PromptAdvance()
    {
        DialogueManager.instance.OnUserPrompt_Next();
    }
}
