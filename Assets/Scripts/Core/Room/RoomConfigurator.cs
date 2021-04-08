using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.Room
{
    public class RoomConfigurator : MonoBehaviour
    {
        public PolygonCollider2D PolygonFloorBounds => _polygonFloorBounds;
        public PolygonCollider2D PolygonWallBounds => _polygonWallBounds;
        
        public EdgeCollider2D PolygonFloorBorder => _polygonFloorBorder;
        public EdgeCollider2D PolygonWallBorder => _polygonWallBorder;

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
        
        private PolygonCollider2D _polygonFloorBounds;
        private PolygonCollider2D _polygonWallBounds;
        
        private EdgeCollider2D _polygonFloorBorder;
        private EdgeCollider2D _polygonWallBorder;

        [Inject]
        public void Construct([Inject(Id = "floor")] PolygonCollider2D polygonFloorBounds, [Inject(Id = "wall")] PolygonCollider2D polygonWallBounds,
            [Inject(Id = "floorBorder")] EdgeCollider2D polygonFloorBorder, [Inject(Id = "wallBorder")] EdgeCollider2D polygonWallBorder)
        {
            _polygonFloorBounds = polygonFloorBounds;
            _polygonWallBounds = polygonWallBounds;

            _polygonFloorBorder = polygonFloorBorder;
            _polygonWallBorder = polygonWallBorder;
        }

        private void Start()
        {
            SetupRoom();
            MakeCollider();
        }

        #if UNITY_EDITOR
        private void OnValidate() => SetupRoom();
        #endif

        private void SetupRoom()
        {
            CenterRoom();
            PlaceCorners();
            PlaceDoors();
        }

        private void CenterRoom() => transform.position = new Vector2(-_width / 2, _height / 2);

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
            Vector2[] points =
            {
                new Vector2(_cornerLeftUp.transform.localPosition.x, _cornerLeftUp.transform.localPosition.y),
                new Vector2(_cornerRightUp.transform.localPosition.x, _cornerRightUp.transform.localPosition.y),
                new Vector2(_cornerRightDown.transform.localPosition.x, _cornerRightDown.transform.localPosition.y),
                new Vector2(_cornerLeftDown.transform.localPosition.x, _cornerLeftDown.transform.localPosition.y),
                new Vector2(_cornerLeftUp.transform.localPosition.x, _cornerLeftUp.transform.localPosition.y)
            };

            var wallPoints = points.Select(x => x + new Vector2(0, 0.6f)).ToArray();

            _polygonFloorBounds.points = points;
            _polygonFloorBorder.points = points;

            _polygonWallBorder.points = wallPoints;
            _polygonWallBounds.points = wallPoints;
        }
    }
}
