using UnityEngine;

public class Pipe : MonoBehaviour
{
    private static float pipeSpeed = 2f; // Speed at which pipes move to the left
    [SerializeField] private Transform imagePipe;

    private Vector3 spawnPosition;
    private float deSpawnDistance = 25f; // Distance from the spawn point to despawn the pipe


    private void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        Move();
        CheckDespawn();
    }

    public static void SetPipeSpeed(float pipeSpeed)
    {
        Pipe.pipeSpeed = pipeSpeed;
    }

    private void Move()
    {
        // Move the pipe to the left
        transform.Translate(Vector3.left * pipeSpeed * Time.deltaTime);
    }

    private void CheckDespawn()
    {
        // Calculate the distance from the spawn point
        float distanceFromSpawn = Vector3.Distance(transform.position, spawnPosition);

        // Despawn the pipe if it exceeds the despawn distance
        if (distanceFromSpawn >= deSpawnDistance)
        {
            Destroy(gameObject);
        }
    }

    public void RotateImage()
    {
        // Rotate the imagePipe by 180 degrees around the Z-axis
        imagePipe.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
}