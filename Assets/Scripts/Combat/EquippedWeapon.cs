using Unity.VisualScripting;
using UnityEngine;

public class EquippedWeapon : MonoBehaviour
{
    private GameObject attacker;
    public PickableObject weapon;

    [Header("Weapon Specifics")]
    [SerializeField] GameObject realBulletPrefab;
    [SerializeField] Transform realBarrelTransform;
    [SerializeField] GameObject fakebulletPrefab;
    [SerializeField] Transform fakebarrelTransform;

    [Header("Weapon Stats")]
    public int damage;
    public int speed;

    [Header("Faction")]
    [SerializeField] CharacterSheet characterSheet;

    private void Start()
    {
        //Need to add modifier depending on stats in character sheets
        attacker = this.gameObject;
    }

    public void shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        bool hasHit = Physics.Raycast(ray, out hit, 100f); // Single raycast

        if (weapon.damage > 0)
        {
            // Instantiate real bullet
            GameObject realBulletObject = Instantiate(realBulletPrefab, realBarrelTransform.position, Quaternion.LookRotation(realBarrelTransform.forward));
            Bullet realBullet = realBulletObject.GetComponent<Bullet>();
            realBullet.damage = weapon.damage * (1 + (int)characterSheet.GetSkillLevel("Guns"));
            realBullet.speed = weapon.attackSpeed;
            realBullet.attacker = attacker;
            if (hasHit) realBullet.SetTarget(hit.point);

            // Instantiate fake bullet if prefab is assigned
            if (fakebulletPrefab != null)
            {
                GameObject fakeBulletObject = Instantiate(fakebulletPrefab, fakebarrelTransform.position, Quaternion.LookRotation(fakebarrelTransform.forward));
                Bullet fakeBullet = fakeBulletObject.GetComponent<Bullet>();
                fakeBullet.speed = this.speed;
                if (hasHit) fakeBullet.SetTarget(hit.point);
            }
        }
    }


    public void EquipWeapon(PickableObject newWeapon)
    {
        // Set the new weapon
        weapon = newWeapon;

        // Update the weapon specifics if needed
        damage = newWeapon.damage;
        Debug.Log("Damage = " + damage);
        speed = newWeapon.attackSpeed;
        Debug.Log("Speed = " + speed);

        // You might want to update the visual of the weapon or any other weapon-specific logic here
        // For example, changing the weapon model that the player is holding
    }
}
