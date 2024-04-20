using Unity.VisualScripting;
using UnityEngine;

public class EquippedWeapon : MonoBehaviour
{
    private GameObject attacker;
    public PickableObject weapon;

    [Header("Weapon Specifics")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform barrelTransform;

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

        if (Physics.Raycast(ray, out hit, 100f)) // Adjust range as needed
        {
            GameObject bulletObject = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.LookRotation(barrelTransform.forward));
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            // Assign weapon stats to the bullet
            bullet.damage = weapon.damage; // Set bullet damage from weapon
            bullet.speed = weapon.attackSpeed; // Set bullet speed from weapon
            bullet.attacker = attacker;// set the attacker so the AI know who shot it

            bullet.SetTarget(hit.point); // Pass the hit point to the bullet
        }
        else
        {
            GameObject bulletObject = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.LookRotation(barrelTransform.forward));
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            // Assign weapon stats to the bullet
            bullet.damage = this.damage; // Set bullet damage from weapon
            bullet.speed = this.speed; // Set bullet speed from weapon
            bullet.attacker = attacker; // Set the attacker
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
