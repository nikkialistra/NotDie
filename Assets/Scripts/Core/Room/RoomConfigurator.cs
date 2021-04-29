using System.Linq;
using UnityEngine;

namespace Core.Room
{
    public class RoomConfigurator : MonoBehaviour
    {
        public PolygonCollider2D PolygonFloorBounds => _polygonFloorBounds;
        public PolygonCollider2D PolygonWallBounds => _polygonWallBounds;
        public EdgeCollider2D PolygonFloorBorder => _polygonFloorBorder;
        
        [Header("Bounds")]
        [SerializeField] private PolygonCollider2D _polygonFloorBounds;
        [SerializeField] private PolygonCollider2D _polygonWallBounds;
        [SerializeField] private EdgeCollider2D _polygonFloorBorder;
        [SerializeField] private EdgeCollider2D _polygonWallBorder;

        [Header("Corners")]
        [SerializeField] private GameObject _cornerLeftUp;
        [SerializeField] private GameObject _cornerRightUp;
        [SerializeField] private GameObject _cornerLeftDown;
        [SerializeField] private GameObject _cornerRightDown;

        [Header("Doors")]
        [SerializeField] private GameObject _bottomDoor;
        [SerializeField] private GameObject _upDoor;
        [SerializeField] private GameObject _rightDoor;
        [SerializeField] private GameObject _leftDoor;

        [Header("Settings")]
        [Range(0, 20)]
        [SerializeField] private float _width;
        [Range(0, 20)]
        [SerializeField] private float _height;
        [Range(0, 8)]
        [SerializeField] private float _perspective;

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
            PlaceDoors();
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
        
        private void PlaceDoors()
        {
            _upDoor.transform.localPosition = new Vector2(_width / 2 , 0);
            _bottomDoor.transform.localPosition = new Vector2(_width / 2 , -_height);

            _rightDoor.transform.localPosition = new Vector2(_width + _perspective / 2, -_height / 2);
            _leftDoor.transform.localPosition = new Vector2(-_perspective / 2, -_height / 2);
        }

        private void MakeCollider()
        {
            var leftUpPosition = _cornerLeftUp.transform.localPosition;
            var rightUpPosition = _cornerRightUp.transform.localPosition;
            var rightDownPosition = _cornerRightDown.transform.localPosition;
            var leftDownPosition1 = _cornerLeftDown.transform.localPosition;
            Vector2[] points =
            {
                new Vector2(leftUpPosition.x, leftUpPosition.y),
                new Vector2(rightUpPosition.x, rightUpPosition.y),
                new Vector2(rightDownPosition.x, rightDownPosition.y),
                new Vector2(leftDownPosition1.x, leftDownPosition1.y),
                new Vector2(leftUpPosition.x, leftUpPosition.y)
            };

            var wallPoints = points.Select(x => x + new Vector2(0, 0.6f)).ToArray();

            PolygonFloorBounds.points = points;
            PolygonFloorBorder.points = points;

            _polygonWallBorder.points = wallPoints;
            PolygonWallBounds.points = wallPoints;
        }
    }
}
