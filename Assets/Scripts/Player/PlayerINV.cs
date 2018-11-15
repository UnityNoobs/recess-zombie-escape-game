using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerINV : MonoBehaviour {
    public List<Weapon> weapons = new List<Weapon>();
    public int playerNumber = 1;
    private int currentWeaponIndex = 0;
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("P"+playerNumber+"WeaponScroll") > 0)
        {
            currentWeaponIndex = NextWeaponIndex();
        }
        if (Input.GetAxis("P" + playerNumber + "WeaponScroll") < 0)
        {
            currentWeaponIndex = PrevWeaponIndex();
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
        if (weapons.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
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
