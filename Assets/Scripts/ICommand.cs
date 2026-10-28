
using UnityEngine;

public interface ICommand
{
    void Invoke(Vector2 position);
}

public interface IUndoableCommand : ICommand
{
    void Undo();
}
