using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class RoomConfigurator : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Header("Corners")] 
            public GameObject CornerLeftUp;
            public GameObject CornerRightUp;
            public GameObject CornerLeftDown;
            public GameObject CornerRightDown;

            [Header("Settings")]
            [Range(0, 20)]
            public float Width;
            [Range(0, 20)]
            public float Height;
            [Range(0, 8)]
            public float Perspective;
        }

        [SerializeField] private Settings _settings;

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
            transform.position = new Vector2(-_settings.Width / 2, _settings.Height / 2);
        }

        private void PlaceCorners()
        {
            _settings.CornerLeftUp.transform.localPosition = Vector2.zero;
            _settings.CornerRightUp.transform.localPosition = new Vector2(_settings.Width, 0);

            _settings.CornerLeftDown.transform.localPosition = new Vector2(0 - _settings.Perspective, -_settings.Height);
            _settings.CornerRightDown.transform.localPosition = new Vector2(_settings.Width + _settings.Perspective, -_settings.Height);
        }

        private void MakeCollider()
        {
            Vector2[] points =
            {
                new Vector2(_settings.CornerLeftUp.transform.localPosition.x, _settings.CornerLeftUp.transform.localPosition.y),
                new Vector2(_settings.CornerRightUp.transform.localPosition.x, _settings.CornerRightUp.transform.localPosition.y),
                new Vector2(_settings.CornerRightDown.transform.localPosition.x, _settings.CornerRightDown.transform.localPosition.y),
                new Vector2(_settings.CornerLeftDown.transform.localPosition.x, _settings.CornerLeftDown.transform.localPosition.y),
                new Vector2(_settings.CornerLeftUp.transform.localPosition.x, _settings.CornerLeftUp.transform.localPosition.y)
            };

            _collider.points = points;
        }
    }
}
