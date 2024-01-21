using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleOne
{
    /// <summary>
    /// the ListItemView will attach to each of the character UI gameobject prefabs in the list
    /// It has access to the image and text fields
    /// </summary>
    public class ListItemView : MonoBehaviour
    {
        public GameObject avatar;
        public GameObject characterName;
        public Button button;
        public void Init(CharacterData characterData, Action<CharacterData> callback)
        {
            if (avatar == null)
            {
                avatar = transform.Find("Avatar").gameObject;
                if (avatar == null)
                    Debug.LogError("No child gameobject called Avatar.");
            }
            if (characterName == null)
            {
                characterName = transform.Find("CharacterName").gameObject;
                if (characterName == null)
                    Debug.LogError("No child gameobject called characterName.");
            }
            if (button == null)
                button = GetComponent<Button>();
            Image image = avatar.GetComponent<Image>();
            image.sprite = characterData.avatar;
            Text text = characterName.GetComponent<Text>();
            text.text = characterData.name;
            // Bind the callback function to button
            button.onClick.AddListener(() => callback(characterData));
        }
    }
}
