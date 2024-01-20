using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleOne
{
    /// <summary>
    /// a class is used to parse the JSON data to CharacterData.
    /// </summary>
    public class JSONImporter
    {
        public List<CharacterData> Import(string jsonData)
        {
            List<CharacterData> characterdata = new List<CharacterData>();
            char[] whitespace = new char[] { ' ', '\t', '\r', '\n', ',', ':' };
            string[] words = jsonData.Split(whitespace, System.StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            ReadBracketLeft(words, ref index);
            int brackets = 1;
            int infLoop = 10000;
            while (brackets > 0 && --infLoop > 0)
            {
                ReadObjectKey(words, ref index);
                ReadBracketLeft(words, ref index);
                brackets += 1;
                ReadNameKey(words, ref index);
                string temptName = words[index++].Replace("\"", "");
                ReadSpritePathKey(words, ref index);
                string temptSpritePath = words[index++].Replace("\"", "");
                ReadSpriteIndexKey(words, ref index);
                int temptSpriteIndex;
                bool success = int.TryParse(words[index++], out temptSpriteIndex);
                if (success)
                {
                    Sprite[] avatarAll = Resources.LoadAll<Sprite>(temptSpritePath);
                    Sprite temptAvatar = avatarAll[temptSpriteIndex];
                    CharacterData temptCharacter = new CharacterData(temptName, temptSpritePath, temptSpriteIndex, temptAvatar);
                    characterdata.Add(temptCharacter);
                }
                else
                {
                    Debug.LogError("incorrect format of sprite index.");
                }
                while (ReadBracketRight(words, ref index))
                {
                    brackets -= 1;
                }
                
            }
            if (infLoop <= 0)
                Debug.LogError("JSON file doesn't follow the format : Left and Right brackets don't match.");

            return characterdata;
        }
        private void ReadSpritePathKey(string[] words, ref int index)
        {
            if (words[index++] != "\"sprite_path\"")
                Debug.LogError("JSON file doesn't follow the format : Sprite Path key error.");
        }

        private void ReadSpriteIndexKey(string[] words, ref int index)
        {
            if (words[index++] != "\"sprite_index\"")
                Debug.LogError("JSON file doesn't follow the format : Sprite Index key error.");
        }
        private void ReadNameKey(string[]words, ref int index)
        {
            if (words[index++] != "\"name\"")
                Debug.LogError("JSON file doesn't follow the format : Name key error.");
        }
        private void ReadObjectKey(string[] words, ref int index)
        {
            if (words[index++].Substring(0, 3) != "\"c_")
                Debug.LogError("JSON file doesn't follow the format : object key error.");
        }
        private void ReadBracketLeft(string[] words, ref int index)
        {
            if (words[index++] != "{")
                Debug.LogError("JSON file doesn't follow the format : { not found");
        }

        private bool ReadBracketRight(string[] words, ref int index)
        {
            if (index >= words.Length)
                return false;
            bool isRight = words[index] == "}";
            if(isRight)
            {
                index += 1;
            }
            return isRight;
        }
    }

}
