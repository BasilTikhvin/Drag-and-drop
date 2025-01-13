using UnityEngine;

namespace Dragndrop
{
    public class InteractablesDepth : MonoBehaviour
    {
        private Interactable[] interactables;

        private void Start()
        {
            // ѕолучаем изначально все предметы в сцене, таким образом можно расставить по глубине абсолютно все предметы в сцене
            interactables = FindObjectsOfType<Interactable>();

            SetArrayData();
        }

        /* ¬ данном методе мы передвигаем вз€тый предмет на первое место в массиве и соответсвенно делаем его первым в очереди на отрисовку,
        далее сдвигаем по пор€дку предметы, которые необходимо сдвинуть, те что не нужно, просто переносим в новый массив дл€ удобства
        затем обновл€ем изначальный массив, но уже с новым пор€дком и передаем данные в каждый предмет */
        public void MoveArray(int index)
        {
            Interactable[] newArray = new Interactable[interactables.Length];

            newArray[0] = interactables[index];

            for (int i = 0; i < index; i++)
            {
                newArray[i + 1] = interactables[i];
            }

            for (int i = index + 1; i < interactables.Length; i++)
            {
                newArray[i] = interactables[i];
            }

            interactables = newArray;

            SetArrayData();
        }

        private void SetArrayData()
        {
            for (int i = 0; i < interactables.Length; i++)
            {
                interactables[i].SetData(i, this);
            }
        }
    }
}
