using UnityEngine;
using System.Collections;

/// <summary>
/// This command shall be used to make a vehicle to shoot.
/// </summary>
public class ShootCommand : ICommand {

    /// <summary>
    /// Executing the command.
    /// </summary>
    /// <param name="vehicle">The vehicle which shall shoot.</param>
    public void Execute(Vehicle vehicle) {

        vehicle.Shoot();
    }
}
