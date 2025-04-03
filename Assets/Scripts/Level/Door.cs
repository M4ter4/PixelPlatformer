using UnityEngine;

namespace Level
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private RoomReset previousRoom;
        [SerializeField] private RoomReset nextRoom;
        [SerializeField] private CameraController cameraController;

        private void SwitchState(Collider2D other, bool leftToRight)
        {
            if (other.CompareTag("Player"))
            {
                if (other.transform.position.x < transform.position.x == leftToRight)
                {
                    cameraController.Move(nextRoom.RoomCenterPosition);
                    nextRoom.GetComponent<RoomReset>().ActivateRoom(true);
                    previousRoom.GetComponent<RoomReset>().ActivateRoom(false);
                }
                else
                {
                    cameraController.Move(previousRoom.RoomCenterPosition);
                    nextRoom.GetComponent<RoomReset>().ActivateRoom(false);
                    previousRoom.GetComponent<RoomReset>().ActivateRoom(true);
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other) => SwitchState(other, true);

        private void OnTriggerExit2D(Collider2D other) => SwitchState(other, false);
    }
}
