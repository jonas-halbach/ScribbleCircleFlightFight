using UnityEngine;
using System.Collections;

/// <summary>
/// This Command-Object will be used to move a vehicle in a given direction.
/// </summary>
public class MoveCommand : ICommand {

    private Direction direction;

    /// <summary>
    /// Giving the direction, to which the vehicle shall be moved
    /// </summary>
    /// <param name="direction">The moving-direction</param>
    public MoveCommand(Direction direction) {
        this.direction = direction;
    }

    /// <summary>
    /// Executing the moving command
    /// </summary>
    /// <param name="vehicle">The vehicle which shall be moved</param>
    public void Execute(Vehicle vehicle) {
        vehicle.Move(direction);
    }
}

public enum Direction {
    left,
    right
}
