using UnityEngine;

namespace Level
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform previousRoom;
        [SerializeField] private Transform nextRoom;
        [SerializeField] private CameraController cameraController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Метод CompareTag более эффективен, чем прямое сравнение строки
            {
                if (other.transform.position.x < transform.position.x)
                {
                    cameraController.Move(nextRoom.position);
                    nextRoom.GetComponent<RoomReset>().ActivateRoom(true);
                    previousRoom.GetComponent<RoomReset>().ActivateRoom(false);
                }
                else
                {
                    cameraController.Move(previousRoom.position);
                    nextRoom.GetComponent<RoomReset>().ActivateRoom(false);
                    previousRoom.GetComponent<RoomReset>().ActivateRoom(true);
                }
            }
        }
    }
}
