using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SampleOne
{
    /// <summary>
    /// ImportController manage the import of JSON data from streamingAsset folder.
    /// It will be called by UI controller when initialize ListItemView.
    /// This will be a singleton object in the game.
    /// Attach to ImportController gameobject in the scene
    /// </summary>
    public class ImportController : MonoBehaviour
    {
        public static ImportController sharedInstance { get; private set; }
        private void Awake()
        {
            sharedInstance = this;
        }
        // Import JSON from path and parse to CharacterDatabase scriptable object, asset will be stored in stremaingAsset folder
        public CharacterDatabase ImportCharacterData(string path)
        {
            // Create the scriptable object of character database which stores all character information
            CharacterDatabase characterDatabase;
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            if(File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
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
