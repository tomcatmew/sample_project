using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleOne
{
    /// <summary>
    /// Defines the model that stores all characters data.
    /// After importing, Character data will be all stored in CharacterDatabase scriptable object.
    /// </summary>
    public class CharacterData
    {
        public string name { get; private set; }
        public string spritePath { get; private set; }
        public int spriteIndex { get; private set; }
        public Sprite avatar { get; private set; }
        public CharacterData(string name, string spritePath, int spriteIndex, Sprite avatar)
        {
            this.name = name;
            this.spritePath = spritePath;
            this.spriteIndex = spriteIndex;
            this.avatar = avatar;
        }
    }
    // Scriptable object is added to asset menu, but we don't need to create the scriptable object from editor
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "SampleOne/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        public List<CharacterData> characters;
        public void Import(string jsonData)
        {
            JSONImporter importer = new JSONImporter();
            characters = importer.Import(jsonData);
        }
}

}
