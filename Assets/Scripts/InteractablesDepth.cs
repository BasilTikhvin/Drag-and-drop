using UnityEngine;

namespace Dragndrop
{
    public class InteractablesDepth : MonoBehaviour
    {
        private Interactable[] interactables;

        private void Start()
        {
            interactables = FindObjectsOfType<Interactable>();

            SetArrayData();
        }

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
