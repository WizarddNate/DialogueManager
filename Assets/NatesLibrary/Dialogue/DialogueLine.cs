using UnityEngine;

namespace NatesLibrary.Dialogue
{
    public class DialogueLine
    {
        public string speaker;
        public string dialogue;
        public string commands;

        public DialogueLine(string speaker, string dialogue, string commands)
        {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
        }
    }
}
