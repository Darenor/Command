using UnityEngine;

public class SpawnCommand : IUndoableCommand
{
    private readonly GameObject prefab;
    private GameObject spawnedObject;
    public Vector2 Position { get; }

    public SpawnCommand(GameObject prefab, Vector2 position)
    {
        this.prefab = prefab;
        Position = position;
    }

    public void Invoke(Vector2 position)
    {
        if (prefab == null)
            return;

        spawnedObject = Object.Instantiate(
            prefab,
            new Vector3(position.x, position.y, 0f),
            Quaternion.identity
            );
    }

    public void Undo()
    {
        if (spawnedObject != null)
        {
            Object.Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
