using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SampleOne
{
    /// <summary>
    /// Import Controller manage the import of JSON data.
    /// It will be called by UI controller when initialize ListItemView.
    /// This is a singleton object in the game.
    /// </summary>
    public class ImportController : MonoBehaviour
    {
        public static ImportController sharedInstance;
        private void Awake()
        {
            sharedInstance = this;
        }
        public CharacterDatabase ImportCharacterData()
        {
            CharacterDatabase characterDatabase;
            string filePath = Path.Combine(Application.streamingAssetsPath, "Data/CharacterData.json");
            if(File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                //characterDatabase = Resources.Load<CharacterDatabase>("Characters/CharacterList");
                characterDatabase = ScriptableObject.CreateInstance<CharacterDatabase>();
                characterDatabase.Import(jsonData);
                Debug.Log("Finish importing JSON");
                return characterDatabase;
            }
            else
            {
                Debug.LogError("Cannot find Character Data file!");
                return null;
            }
        }
    }
}
