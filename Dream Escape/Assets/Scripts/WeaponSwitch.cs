using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    ICommand _onCommand;

    public WeaponSwitch(ICommand onCommand)
    {
        _onCommand = onCommand;
    }

    public void toggleWeapon()
    {
        _onCommand.Execute();
    }
}
