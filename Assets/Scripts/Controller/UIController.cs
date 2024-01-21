using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SampleOne
{
    /// <summary>
    /// UI controller manage to initialize the ListItemView and DetailView and bind callback func to buttons.
    /// UI controller also will resize height of the UI container for ListItemViews based on the counts of them.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        //public ImportController importController;
        public GameObject itemUIPrefab;
        public DetailView detailView; 
        [HideInInspector]
        public CharacterDatabase characterDatabase;
        void Start()
        {
            if (detailView == null)
            {
                detailView = FindFirstObjectByType<DetailView>();
                if (detailView == null)
                    Debug.LogError("detailView cananot be found.");
            }

            if (ImportController.sharedInstance == null)
            {
                Debug.LogError("cannot find ImportController.");
            }
            else
                characterDatabase = ImportController.sharedInstance.ImportCharacterData();

            if (itemUIPrefab == null)
            {
                itemUIPrefab = Resources.Load<GameObject>("Prefab/ItemContainer");
            }
            if (characterDatabase == null)
            {
                Debug.LogError("Character data load failed.");
            }
            else if (itemUIPrefab == null)
            {
                Debug.LogError("UI prefab is not assigned.");
            }
            else
            {
                int itemCount = 0;
                float spacing = GetComponent<VerticalLayoutGroup>().spacing;
                Vector2 itemSize = new Vector2();
                foreach (var character in characterDatabase.characters)
                {
                    GameObject newItemUIContainer = Instantiate(itemUIPrefab, transform);
                    ListItemView listItemView = newItemUIContainer.GetComponent<ListItemView>();
                    listItemView.Init(character, UpdateDetailView);
                    itemSize = listItemView.GetComponent<RectTransform>().sizeDelta;
                    if(itemCount == 0)
                        detailView.Init(character);
                    itemCount += 1;
                }

                RectTransform rectTransform = GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, itemCount * itemSize.y + itemCount * spacing);
                }
                else
                {
                    Debug.LogError("RectTransform component not found on this GameObject.");
                }
            }
        }

        private void UpdateDetailView(CharacterData characterData)
        {
            detailView.UpdateDataFields(characterData);
        }
    }
}
