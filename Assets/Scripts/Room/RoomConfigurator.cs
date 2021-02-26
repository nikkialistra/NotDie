using System;
using UnityEngine;

namespace Room
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class RoomConfigurator : MonoBehaviour
    {
        [Header("Corners")] 
        [SerializeField] private GameObject _cornerLeftUp;
        [SerializeField] private GameObject _cornerRightUp;
        [SerializeField] private GameObject _cornerLeftDown;
        [SerializeField] private GameObject _cornerRightDown;

        [Header("Settings")]
        [Range(0, 20)]
        [SerializeField] private float _width;
        [Range(0, 20)]
        [SerializeField] private float _height;
        [Range(0, 8)]
        [SerializeField] private float _perspective;

        private EdgeCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<EdgeCollider2D>();
        }

        private void Start()
        {
            SetupRoom();
            MakeCollider();
        }

        private void OnValidate()
        {
            SetupRoom();
        }

        private void SetupRoom()
        {
            CenterRoom();
            PlaceCorners();
        }

        private void CenterRoom()
        {
            transform.position = new Vector2(-_width / 2, _height / 2);
        }

        private void PlaceCorners()
        {
            _cornerLeftUp.transform.localPosition = Vector2.zero;
            _cornerRightUp.transform.localPosition = new Vector2(_width, 0);

            _cornerLeftDown.transform.localPosition = new Vector2(0 - _perspective, -_height);
            _cornerRightDown.transform.localPosition = new Vector2(_width + _perspective, -_height);
        }

        private void MakeCollider()
        {
            Vector2[] points =
            {
                new Vector2(_cornerLeftUp.transform.localPosition.x, _cornerLeftUp.transform.localPosition.y),
                new Vector2(_cornerRightUp.transform.localPosition.x, _cornerRightUp.transform.localPosition.y),
                new Vector2(_cornerRightDown.transform.localPosition.x, _cornerRightDown.transform.localPosition.y),
                new Vector2(_cornerLeftDown.transform.localPosition.x, _cornerLeftDown.transform.localPosition.y),
                new Vector2(_cornerLeftUp.transform.localPosition.x, _cornerLeftUp.transform.localPosition.y)
            };

            _collider.points = points;
        }
    }
}
