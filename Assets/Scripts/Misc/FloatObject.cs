using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public int randomSeed = 0; // Seed value for randomness
    public float minFloatHeight = 1f; // Minimum float height
    public float maxFloatHeight = 1f; // Maximum float height
    public float floatSpeed = 2.0f; // The speed of the floating motion

    private Vector3 startPos;
    private float randomOffset;

    public bool canMove = true;

    // Call this method to initialize the random seed
    public void InitializeRandomSeed()
    {
        // Initialize the random number generator with the specified seed
        Random.InitState(randomSeed);
    }

    private void Start()
    {
        // Store the initial position of the GameObject
        startPos = transform.position;

        // Generate a random offset for floatHeight when the object is instantiated
        randomOffset = Random.Range(minFloatHeight, maxFloatHeight);
    }

    private void Update()
    {
        if (canMove)
        {
            // Calculate the new Y position for floating
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * randomOffset;

            // Update the object's position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

}