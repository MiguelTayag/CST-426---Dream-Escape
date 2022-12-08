using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public PlayerMovement _playerMovement;
    WeaponSwitch _weaponSwitch;

    // Start is called before the first frame update
    void Start()
    {
        ICommand toggleWeaponCommand = new ToggleWeaponCommand(_playerMovement);
        _weaponSwitch = new WeaponSwitch(toggleWeaponCommand);
    }

    // Update is called once per frame
    void Update()
    {

        // Using command pattern to toggle weapon on and off
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _weaponSwitch.toggleWeapon();
        }

    }
}
