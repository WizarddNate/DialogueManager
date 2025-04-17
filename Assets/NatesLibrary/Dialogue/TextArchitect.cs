using UnityEngine;
using System.Collections;
using TMPro;

namespace NatesLibrary.Dialogue //if in subfolder, write .subfoldername
{
    public class TextArchitect
    {
        private TextMeshProUGUI tmpro_ui;
        private TextMeshPro tmpro_world;
        public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

        public string currentText => tmpro.text;
        public string targetText { get; private set; } = "";
        public string preText { get; private set; } = "";
        //private int preTextLength = 0;

        public string fullTargetText => preText + targetText;

        //enum that allows us to choose the specific text animation that we want
        public enum BuildMethod { instant, typewriter, fade }
        public BuildMethod buildMethod = BuildMethod.typewriter;

        //text color
        public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

        //text speed
        public float baseTextSpeed = 0.7f;
        public float textSpeedMultiplier = 1;
        public float textSpeed { get { return baseTextSpeed * textSpeedMultiplier; } set { textSpeedMultiplier = value; } }

        //multiply the amount of characters per second
        private int characterMultiplier = 1;
        public int charactersPerCycle { get { return textSpeed <= 2f ? characterMultiplier : textSpeed <= 2.5 ? characterMultiplier * 2 : characterMultiplier * 3; } }

        //speed up and skip text
        public bool textSpeedIncrease = false;

        //assign ui text to text architect
        public TextArchitect(TextMeshProUGUI tmpro_ui)
        {
            this.tmpro_ui = tmpro_ui;
        }
        //assign 3d space text to text architect
        public TextArchitect(TextMeshPro tmpro_world)
        {
            this.tmpro_world = tmpro_world;
        }

        //since TextArchitect is not a monobehavior, it cant run a coroutine.
        //However, the text object that this script will be attached to can, so that's how this can be here!

        //build brand new text for the text architect
        public Coroutine Build(string text)
        {
            preText = "";
            targetText = text;

            Stop();

            buildProcess = tmpro.StartCoroutine(Building());
            return buildProcess;
        }

        //append text to whatever is already in the text architect. not sure if i want to keep this. Delete later??
        public Coroutine Append(string text)
        {
            preText = tmpro.text;
            targetText = text;

            Stop();

            buildProcess = tmpro.StartCoroutine(Building());
            return buildProcess;
        }

        private Coroutine buildProcess = null;
        public bool isBuilding => buildProcess != null;

        public void Stop()
        {
            if (!isBuilding)
                return;

            tmpro.StopCoroutine(buildProcess);
            buildProcess = null;
        }

        IEnumerator Building()
        {
            Prepare();

            switch (buildMethod)
            {
                case BuildMethod.typewriter:
                    yield return Build_Typewriter();
                    break;

                case BuildMethod.fade:
                    yield return Build_Fade();
                    break;
            }
            OnComplete();
        }

        //basic logic that runs once the build process is done
        private void OnComplete()
        {
            buildProcess = null;
            textSpeedIncrease = false;
        }

        public void ForceComplete()
        {
            switch(buildMethod)
            {
                case BuildMethod.typewriter:
                    tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                    break;
                case BuildMethod.fade:
                    break;
            }

            Stop();
            OnComplete();
        }

        /// <summary>
        /// prepare the text architect for the chosen build method
        /// </summary>
        private void Prepare()
        {
            switch (buildMethod)
            {
                case BuildMethod.instant:
                    Prepare_Instant();
                    break;

                case BuildMethod.typewriter:
                    Prepare_Typewriter();
                    break;

                case BuildMethod.fade:
                    Prepare_Fade();
                    break;
            }
        }

        private void Prepare_Instant()
        {
            tmpro.color = tmpro.color; //makes sure that color stays the same
            tmpro.text = fullTargetText;
            tmpro.ForceMeshUpdate(); //all changes made are applied at this point
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount; //make sure every character is visable on screen
        }

        private void Prepare_Typewriter()
        {
            //reset the text to make sure it's good to start
            tmpro.color = tmpro.color; 
            tmpro.maxVisibleCharacters = 0; 
            tmpro.text = preText;

            //check if pretext is avaliable and visable
            if (preText != "")
            {
                tmpro.ForceMeshUpdate();
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
            }

            //add target text to build
            tmpro.text += targetText;
            tmpro.ForceMeshUpdate();
        }

        private void Prepare_Fade()
        {

        }

        private IEnumerator Build_Typewriter()
        {
            while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
            {
                tmpro.maxVisibleCharacters += textSpeedIncrease ? charactersPerCycle * 5: charactersPerCycle;

                yield return new WaitForSeconds(0.015f / textSpeed);
            }
        }

        private IEnumerator Build_Fade()
        {
            yield return null;
        }
    }
}