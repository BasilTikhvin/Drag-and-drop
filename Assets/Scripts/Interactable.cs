using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Dragndrop
{
    // В тз было указано, что падение предмета осуществляется под действием гравитации, логичнее всего показалось использовать Rigidbody
    [RequireComponent(typeof(Rigidbody2D))]
    public class Interactable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Событие необходимое для реализации перемещения по сцене с взятым в руку любым предметом
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
            // Чтобы предмет не падал, пока мы его перетаскиваем - сбрасываем скорость на ноль и делаем Rigidbody невосприимчивым к гравитации 
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            _interactablesDepth.MoveArray(_index);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Перетаскиваем предмет
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = position;

            // Отправляем событие и данные для реализации передвижения по сцене
            OnDragging?.Invoke(position.x);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Возвращаем Rigidbody в норму
            _rb.isKinematic = false;
        }

        // Метод для реализации глубины предметов в сцене
        public void SetData(int index, InteractablesDepth interactablesDepth)
        {
            _index = index;
            transform.position = new Vector3(transform.position.x, transform.position.y, _index);

            _interactablesDepth = interactablesDepth;
        }
    }
}
