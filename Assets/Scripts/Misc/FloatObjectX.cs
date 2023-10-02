using UnityEngine;

public class FloatObjectX : MonoBehaviour
{
    public int randomSeed = 0; // Seed value for randomness
    public float minFloatWidth = 1f; // Minimum float Width
    public float maxFloatWidth = 1f; // Maximum float Width
    public float floatSpeed = 1f; // The speed of the floating motion

    private Vector3 startPos;
    private float randomOffset;

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

        // Generate a random offset for floatWidth when the object is instantiated
        randomOffset = Random.Range(minFloatWidth, maxFloatWidth);
    }

    private void Update()
    {
        // Calculate the new Y position for floating
        float newX = startPos.x + Mathf.Sin(Time.time * floatSpeed) * randomOffset;

        // Update the object's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}