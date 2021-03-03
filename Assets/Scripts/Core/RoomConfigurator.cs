using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class RoomConfigurator : MonoBehaviour
    {
        [Header("Corners")] 
        [SerializeField] private GameObject CornerLeftUp;
        [SerializeField] private GameObject CornerRightUp;
        [SerializeField] private GameObject CornerLeftDown;
        [SerializeField] private GameObject CornerRightDown;

        [Header("Settings")]
        [Range(0, 20)]
        [SerializeField] private float Width;
        [Range(0, 20)]
        [SerializeField] private float Height;
        [Range(0, 8)]
        [SerializeField] private float Perspective;

        private EdgeCollider2D _collider;

        private void Awake() => _collider = GetComponent<EdgeCollider2D>();

        private void Start()
        {
            SetupRoom();
            MakeCollider();
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            SetupRoom();
        }
        #endif

        private void SetupRoom()
        {
            CenterRoom();
            PlaceCorners();
        }

        private void CenterRoom()
        {
            transform.position = new Vector2(-Width / 2, Height / 2);
        }

        private void PlaceCorners()
        {
            CornerLeftUp.transform.localPosition = Vector2.zero;
            CornerRightUp.transform.localPosition = new Vector2(Width, 0);

            CornerLeftDown.transform.localPosition = new Vector2(0 - Perspective, -Height);
            CornerRightDown.transform.localPosition = new Vector2(Width + Perspective, -Height);
        }

        private void MakeCollider()
        {
            Vector2[] points =
            {
                new Vector2(CornerLeftUp.transform.localPosition.x, CornerLeftUp.transform.localPosition.y),
                new Vector2(CornerRightUp.transform.localPosition.x, CornerRightUp.transform.localPosition.y),
                new Vector2(CornerRightDown.transform.localPosition.x, CornerRightDown.transform.localPosition.y),
                new Vector2(CornerLeftDown.transform.localPosition.x, CornerLeftDown.transform.localPosition.y),
                new Vector2(CornerLeftUp.transform.localPosition.x, CornerLeftUp.transform.localPosition.y)
            };

            _collider.points = points;
        }
    }
}
