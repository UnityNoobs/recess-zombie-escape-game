using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerINV : MonoBehaviour {
    public List<Weapon> weapons = new List<Weapon>();
    public int playerNumber = 1;
    private int currentWeaponIndex = 0;
    private bool buttonNextPressed = false;
    private bool buttonPrevPressed = false;
    private float axis;
    // Update is called once per frame
    void Update () {
        // Next weapon
        float axis = Input.GetAxis("P" + playerNumber + "WeaponScroll");

        if (axis > 0 && !buttonNextPressed && PlayerHasWeapon())
        {
            currentWeaponIndex = NextWeaponIndex();
            buttonNextPressed = true;
        } 
        // Previous weapon
        if (axis < 0 && !buttonPrevPressed && PlayerHasWeapon())
        {
            currentWeaponIndex = PrevWeaponIndex();
            buttonPrevPressed = true;
        }
        // Reset button flags
        if (axis == 0 && buttonNextPressed)
        {
            buttonNextPressed = false;
        }

        if (axis == 0 && buttonPrevPressed)
        {
            buttonPrevPressed = false;
        }
    }

    public Weapon getCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }

    public void addWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public bool PlayerHasWeapon()
    {
        return weapons.Count > 0;
    }
    public int NextWeaponIndex()
    {
        if(currentWeaponIndex == weapons.Count - 1)
        {
            currentWeaponIndex = 0;
        }
        else
        {
            currentWeaponIndex++;
        }
        Debug.Log("The Player now has: " + weapons[currentWeaponIndex].WeaponName + " Equip");
        return currentWeaponIndex;
    }

    public int PrevWeaponIndex()
    {
        if (currentWeaponIndex == 0)
        {
            currentWeaponIndex = weapons.Count - 1;
        }
        else
        {
            currentWeaponIndex--;
        }
        Debug.Log("The Player now has: " + weapons[currentWeaponIndex].WeaponName + " Equip");
        return currentWeaponIndex;
    }
}
