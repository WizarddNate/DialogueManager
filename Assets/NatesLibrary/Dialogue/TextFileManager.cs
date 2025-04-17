using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NatesLibrary.Dialogue
{
    public class TextFileManager
    {
        public static List<string> ReadTextFile(string filePath, bool includeBlankLines = true)
        {
            if (!filePath.StartsWith('/'))
                filePath = TextFilePaths.root + filePath;

            List<string> lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        //checks for each line of text in the text file to either not be blank, or if includeBlankLines is true
                        if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                            lines.Add(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Debug.LogError($"File not found: '{ex.FileName}'");
            }
            return lines;
        }

        public static List<string> ReadTextAsset(string filePath, bool includeBlankLines = true)
        {
            TextAsset asset = Resources.Load<TextAsset>(filePath);
            if (asset == null)
            {
                Debug.LogError($"Asset not found: {filePath}");
                return null;
            }

            return ReadTextAsset(asset, includeBlankLines);
        }

        public static List<string> ReadTextAsset(TextAsset asset, bool includeBlankLines = true)
        {
            List<string> lines = new List<string>();

            using (StringReader sr = new StringReader(asset.text))
            {
                //peeking to see if we have a line avaliable,
                //if we're at -1 then we are at the end of the file
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    if (includeBlankLines || !string.IsNullOrEmpty(line))
                        lines.Add(line);
                }
            }

            return lines;
        }

    }
}
