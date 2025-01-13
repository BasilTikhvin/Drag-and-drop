using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Dragndrop
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Interactable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event UnityAction<float> OnDragging;

        private InteractablesDepth _interactablesDepth;
        private Rigidbody2D _rb;
        private int _index;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            _interactablesDepth.MoveArray(_index);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = position;

            OnDragging?.Invoke(position.x);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rb.isKinematic = false;
        }

        public void SetData(int index, InteractablesDepth interactablesDepth)
        {
            _index = index;
            transform.position = new Vector3(transform.position.x, transform.position.y, _index);

            _interactablesDepth = interactablesDepth;
        }
    }
}
