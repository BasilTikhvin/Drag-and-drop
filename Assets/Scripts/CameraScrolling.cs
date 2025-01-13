using UnityEngine;
using UnityEngine.EventSystems;

namespace Dragndrop
{
    public class CameraScrolling : MonoBehaviour, IDragHandler
    {
        [SerializeField] private float _draggingOffset;
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;

        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            Interactable.OnDragging += OnDragging;
        }

        private void OnDestroy()
        {
            Interactable.OnDragging -= OnDragging;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 position = _cameraTransform.position;
            position -= new Vector3(eventData.delta.normalized.x * _scrollSpeed * Time.deltaTime, 0f);
            position.x = Mathf.Clamp(position.x, _leftBorder.position.x, _rightBorder.position.x);
            _cameraTransform.position = position;
        }

        private void OnDragging(float dragPos)
        {
            if (_cameraTransform.position.x + _draggingOffset < dragPos)
            {
                Vector3 position = _cameraTransform.position;
                position += new Vector3(_scrollSpeed * Time.deltaTime, 0f);
                position.x = Mathf.Clamp(position.x, _leftBorder.position.x, _rightBorder.position.x);
                _cameraTransform.position = position;
            }
            else if (_cameraTransform.position.x - _draggingOffset > dragPos)
            {
                Vector3 position = _cameraTransform.position;
                position -= new Vector3(_scrollSpeed * Time.deltaTime, 0f);
                position.x = Mathf.Clamp(position.x, _leftBorder.position.x, _rightBorder.position.x);
                _cameraTransform.position = position;
            }
        }
    }
}