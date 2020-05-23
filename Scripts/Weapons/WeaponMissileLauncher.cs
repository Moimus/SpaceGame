using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMissileLauncher : Weapon
{
    public GameObject missileObject;
    public Transform target;
    public float fireCooldown = 2f;
    float fireCooldownRemaining = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            checkForLockOn();
            cooldownWeapon();
        }
    }

    public override void fire()
    {
        if(active)
        {
            if (fireCooldownRemaining <= 0)
            {
                Ship ship;
                ship = owner as Ship;
                if (owner != null)
                {
                    if (ship != null && ship.energyCurrent > 0)
                    {
                        GameObject p = Instantiate(projectile);
                        MissileProjectile missileComponent = p.GetComponent<MissileProjectile>();
                        missileComponent.owner = ship.gameObject.transform;
                        missileComponent.faction = owner.faction;
                        if (ship.selectedTargetPointer != -1)
                        {
                            missileComponent.target = target;
                        }
                        p.transform.position = bulletSpawner.transform.position;
                        p.transform.rotation = bulletSpawner.transform.rotation;
                        if (ship != null)
                        {
                            ship.energyCurrent -= energyConsumption;
                        }
                    }
                    else if (ship == null)
                    {
                        GameObject p = Instantiate(projectile);
                        MissileProjectile missileComponent = p.GetComponent<MissileProjectile>();
                        missileComponent.owner = owner.gameObject.transform;
                        missileComponent.faction = owner.faction;
                        missileComponent.target = target;
                        p.transform.position = bulletSpawner.transform.position;
                        p.transform.rotation = bulletSpawner.transform.rotation;
                    }
                }
                else
                {
                    GameObject p = Instantiate(projectile);
                    p.transform.position = bulletSpawner.transform.position;
                    p.transform.rotation = bulletSpawner.transform.rotation;
                }
                fireCooldownRemaining = fireCooldown;
                missileObject.SetActive(false);
            }
        }
    }

    void checkForLockOn()
    {
        Ship ownerShip;
        ownerShip = owner as Ship;
        if(ownerShip != null)
        {
            if (ownerShip.scannedShips.Count > 0 && ownerShip.selectedTargetPointer != -1)
            {
                try
                {
                    if (ownerShip.scannedShips[ownerShip.selectedTargetPointer] != null)
                    {
                        target = ownerShip.scannedShips[ownerShip.selectedTargetPointer].transform;
                    }
                }
                catch(System.Exception)
                {

                }
            }
        }
        else if(owner.GetComponent<Turret>() != null)
        {
            Turret ownerTurret;
            ownerTurret = owner as Turret;
            if(ownerTurret.target != null)
            {
                target = ownerTurret.target.transform;
            }
        }
    }

    void cooldownWeapon()
    {
        if(fireCooldownRemaining > 0)
        {
            fireCooldownRemaining -= Time.deltaTime;
        }
        else
        {
            reload();
        }
    }

    void reload()
    {
        if(!missileObject.activeInHierarchy)
        {
            missileObject.SetActive(true);
        }
    }
}
