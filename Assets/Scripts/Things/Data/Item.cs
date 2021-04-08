using UnityEngine;

namespace Things.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        [Header("Icons")] 
        public Sprite PickUp;
        public Sprite InInventory;

        [Space]
        [TextArea(3, 10)]
        public string Description;
        [Space]
        [TextArea(3, 10)]
        public string Note;
    }
}