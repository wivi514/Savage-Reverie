using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // Damage the bullet does
    public int speed; // Speed the bullet goes 
    private Vector3 startPosition;
    public float maxDistance = 100f; // Maximum travel distance before destruction

    private Vector3 targetPosition;
    private bool targetSet = false;

    void Start()
    {
        startPosition = transform.position;
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Update()
    {
        if (targetSet)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                Destroy(gameObject); // Destroy the bullet upon reaching the target or else it'll be stuck in the air
            }
        }
        float distance = Vector3.Distance(startPosition, transform.position);
        if (distance > maxDistance)
        {
            Destroy(gameObject); // Destroy the bullet if it exceeds the maximum distance
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object we hit has a component that links to a CharacterSheet
        var character = other.GetComponent<CharacterHealth>();
        if (character != null)
        {
            character.ApplyDamage(damage);
        }
        Destroy(gameObject); // Destroy bullet on hit
    }
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        targetSet = true;
    }
}
