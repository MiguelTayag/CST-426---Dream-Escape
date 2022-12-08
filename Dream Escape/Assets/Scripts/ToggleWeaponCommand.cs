using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeaponCommand : ICommand
{

    PlayerMovement _playerMovement;

    public ToggleWeaponCommand(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }


    public void Execute()
    {
        _playerMovement.togglePlayerWeapon();
    }

}
