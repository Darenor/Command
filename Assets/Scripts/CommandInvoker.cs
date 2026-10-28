
using System.Collections.Generic;
using UnityEngine;

public enum CommandType
{
    Move,
    Spawn
}

public class CommandInvoker : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private GameObject prefabToSpawn;

    private MoveCommand moveCommand;

    private readonly List<SpawnCommand> pendingCommands =
        new List<SpawnCommand>();

    private readonly List<SpawnCommand> spawnHistory =
        new List<SpawnCommand>();

    private void Awake()
    {
        moveCommand = new MoveCommand(character);
    }

    public void InvokeCommand(CommandType type, Vector2 position)
    {
        switch (type)
        {
            case CommandType.Move:
                moveCommand.Invoke(position);
                break;

            case CommandType.Spawn:
                SpawnCommand command =
                    new SpawnCommand(prefabToSpawn, position);

                pendingCommands.Add(command);
                spawnHistory.Add(command);

                Debug.Log(
                    $"Команда спавна добавлена в очередь: {position}"
                );
                break;
        }
    }

    public void ExecuteQueuedCommands()
    {
        foreach (SpawnCommand command in pendingCommands)
        {
            command.Invoke(command.Position);
        }

        pendingCommands.Clear();
    }

    public void UndoLastSpawn()
    {
        if (spawnHistory.Count == 0)
        {
            Debug.Log("Нет действий для отмены.");
            return;
        }

        int lastIndex = spawnHistory.Count - 1;
        SpawnCommand command = spawnHistory[lastIndex];

        pendingCommands.Remove(command);

        command.Undo();

        spawnHistory.RemoveAt(lastIndex);
    }

    private Vector2 GetCommandPosition(SpawnCommand command)
    {
        return command.Position;
    }
}
