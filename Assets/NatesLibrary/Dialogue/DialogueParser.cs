using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NatesLibrary.Dialogue
{
    public class DialogueParser
    {
        private const string commandRegexPattern = "\\w*[^\\s]\\("; //a word of any length as long as it is not proceceded by white space

        //pass in the string straight from the dialogue menu
        public static NatesLibrary.Dialogue.DialogueLine Parse(string rawLine)
        {
            //Debug.Log($"Parsing Line: '{rawLine}'");

            (string speaker, string dialogue, string commands) = RipContent(rawLine);

            Debug.Log($"Speaker = '{speaker}'\nDialogue = '{dialogue}'\nCommands = '{commands}'");

            return new NatesLibrary.Dialogue.DialogueLine( speaker, dialogue, commands );
        }


        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = rawLine.Length - 1;
            bool isEscaped = false;

            for (int i = 0; i < rawLine.Length; i++)
            {
                //check for quotation mark characters within the dialogue.
                //This allows for characters to "speak" like "this" without breaking the line of dialogue
                char current = rawLine[i];
                if (current == '\\')
                    isEscaped = !isEscaped;  //check if the character character is a quotation mark and it's not an escaped quote
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == rawLine.Length - 1)
                        dialogueEnd = i;
                        break;
                }  //if it's just a generic character, you wanna make is isEscaped is false
                else
                    isEscaped = false;
            }


            //Identify Command Pattern. Commands allow us to run logic from the dialogue parser
            Regex commandRegex = new Regex(commandRegexPattern);
            Match match = commandRegex.Match(rawLine);
            int commandStart = -1;
            if (match.Success)
            {
                commandStart = match.Index;

                if (dialogueStart == -1 && dialogueEnd == -1)
                    return ("", "", rawLine.Trim());
            }

            //Debug.Log("dialogStart: " + dialogueStart + " dialogEnd:" + dialogueEnd + " c " + commandStart);

            //Figure out whether the line is dialogue or if it is a multi word argument in a command
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                //if we're here, then we know that we have valid dialogue
                
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"","\""); //god thats cursesd
                if (commandStart != -1)
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
                commands = rawLine;
            else
                speaker = rawLine;


            return (speaker, dialogue, commands);
        }
    }
}
