using System;
using Things.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views
{ 
    public class InventoryView : MonoBehaviour
    {
        private VisualElement _rootVisualElement;

        private readonly Image[] _items = new Image[5];

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _items[0] = _rootVisualElement.Q<Image>("item0");
            _items[1] = _rootVisualElement.Q<Image>("item1");
            _items[2] = _rootVisualElement.Q<Image>("item2");
            _items[3] = _rootVisualElement.Q<Image>("item3");
            _items[4] = _rootVisualElement.Q<Image>("item4");
        }

        public void SetItem(int index, Item item)
        {
            if (index < 0 || index > 4)
            {
                throw new IndexOutOfRangeException();
            }

            _items[index].sprite = item.InInventory;
        }
    }
}