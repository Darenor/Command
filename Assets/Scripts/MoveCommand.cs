using UnityEngine;

public class MoveCommand : ICommand
{
    private readonly Transform character;

    public MoveCommand(Transform character)
    {
        this.character = character;
    }

    public void Invoke(Vector2 position)
    {
        if (character == null)
            return;

        character.position = new Vector3(
            position.x,
            position.y,
            character.position.z
            );
    }
}
