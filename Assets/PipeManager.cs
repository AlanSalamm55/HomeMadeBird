using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private Pipe pipePrefab;
    [SerializeField] private Transform spawnLocation; // Reference to the spawner transform
    [SerializeField] private float minGap = 2f; // Minimum gap between top and bottom pipes
    [SerializeField] private float maxGap = 6f; // Maximum gap between top and bottom pipes
    [SerializeField] private float minSpawnInterval = 1.5f; // Minimum time interval between spawning pipe pairs
    [SerializeField] private float maxSpawnInterval = 3f; // Maximum time interval between spawning pipe pairs
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawning pipe pairs
    [SerializeField] private float pipeSpeed = 3f;

    private float originalPipeSpeed; // Store the original pipe speed

    [SerializeField] private Transform pipesParent; // Parent transform to hold all spawned pipes

    private float timeSinceLastSpawn;
    private bool isSpawning; // Flag to track if spawning is active

    private void Awake()
    {
        // Create a parent object to hold all spawned pipes
        pipesParent = new GameObject("PipesParent").transform;
        pipesParent.transform.SetParent(transform); // Set pipesParent as a child of the PipeManager
    }

    void Start()
    {
        timeSinceLastSpawn = spawnInterval;
        originalPipeSpeed = pipeSpeed; // Store the original pipe speed
        Pipe.SetPipeSpeed(pipeSpeed);

        // Start the spawning process
        StopSpawning();
    }

    void Update()
    {
        // Only spawn pipes if spawning is active
        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;

            // Check if it's time to spawn new pipes
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnPipes();
                // Randomize the next spawn interval within the specified range
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnPipes()
    {
        // Randomize the Y position of the center of the gap between the pipes
        float inGameYPosition = Random.Range(-2, 2);

        // Randomize the gap between top and bottom pipes
        float gap = Random.Range(minGap, maxGap);

        // Spawn the top pipe
        Vector3 topPipePosition = new Vector3(spawnLocation.position.x, inGameYPosition + (gap / 2), 0f);
        Pipe topPipe = Instantiate(pipePrefab, topPipePosition, Quaternion.identity, pipesParent);
        topPipe.RotateImage();
        Destroy(topPipe.GetComponent<BoxCollider2D>());

        // Spawn the bottom pipe
        Vector3 bottomPipePosition = new Vector3(spawnLocation.position.x, inGameYPosition - (gap / 2), 0f);
        Instantiate(pipePrefab, bottomPipePosition, Quaternion.identity, pipesParent);
    }

    public void ResetPipes()
    {
        // Destroy all the spawned pipes
        foreach (Transform pipe in pipesParent)
        {
            Destroy(pipe.gameObject);
        }
    }

    // Function to start the spawning process
    public void StartSpawning()
    {
        isSpawning = true;
    }

    // Function to stop and reset the spawning process
    public void StopSpawning()
    {
        isSpawning = false;
        timeSinceLastSpawn = spawnInterval; // Reset the time since last spawn
    }

    public void IncreaseSpeed()
    {
        pipeSpeed += 2;
        Pipe.SetPipeSpeed(pipeSpeed);
    }

    public void ResetSpeed()
    {
        pipeSpeed = originalPipeSpeed; // Reset the pipe speed to its original value
        Pipe.SetPipeSpeed(pipeSpeed);
    }
}