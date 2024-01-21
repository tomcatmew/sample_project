using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleOne
{
    /// <summary>
    /// The DetailView will attach to the character detail UI gameobject
    /// It has access to access the image and text field of detail UI
    /// </summary>
    public class DetailView : MonoBehaviour
    {
        public GameObject avatarDetail;
        public GameObject characterNameDetail;
        public void Init(CharacterData initCharacterData)
        {
            if (avatarDetail == null)
            {
                avatarDetail = transform.Find("AvatarDetail").gameObject;
                if (avatarDetail == null)
                    Debug.LogError("No child gameobject called Avatar.");
            }
            if (characterNameDetail == null)
            {
                characterNameDetail = transform.Find("CharacterNameDetail").gameObject;
                if (characterNameDetail == null)
                    Debug.LogError("No child gameobject called characterName.");
            }
            UpdateDataFields(initCharacterData);
        }
        public void UpdateDataFields(CharacterData characterData)
        {
            Image image = avatarDetail.GetComponent<Image>();
            image.sprite = characterData.avatar;
            Text text = characterNameDetail.GetComponent<Text>();
            text.text = characterData.name;
        }
    }
}