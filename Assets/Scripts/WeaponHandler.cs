using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponColliderObject;
    
    public void EnableWeapon()
    {
        weaponColliderObject.SetActive(true);
    } 

    public void DisableWeapon()
    {
        weaponColliderObject.SetActive(false);
    }

    

}
