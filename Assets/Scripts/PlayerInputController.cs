using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CommandInvoker commandInvoker;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = GetMouseWorldPosition();

            commandInvoker.InvokeCommand(
                CommandType.Move,
                position
                );
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 position = GetMouseWorldPosition();

            commandInvoker.InvokeCommand(
                CommandType.Spawn,
                position
                );
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            commandInvoker.ExecuteQueuedCommands();
        }

        if (Input.GetKeyDown(KeyCode.Z) &&
            (Input.GetKey(KeyCode.LeftControl) ||
            Input.GetKey(KeyCode.RightControl)))
        {
            commandInvoker.UndoLastSpawn();
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = -mainCamera.transform.position.z;

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        return new Vector2(worldPosition.x, worldPosition.y);
    }
}
