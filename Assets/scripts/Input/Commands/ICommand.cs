using UnityEngine;
using System.Collections;

/// <summary>
/// This interface shall be used to control a vehicle.
/// </summary>
public interface ICommand {

    void Execute(Vehicle vehicle);
}
