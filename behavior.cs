using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Behavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f;
    private float duration = 30f;

    public float growInterval = 0.5f; // Grow more frequently
    public float growthFactor = 1.05f; // Smaller size increase each time
    public float growDuration = 0.3f; // Time it takes to complete one growth step

    private void Start()
    {
        StartCoroutine(MoveTowardsPlayer());
        StartCoroutine(GrowOverTime());
    }

    private IEnumerator MoveTowardsPlayer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for next frame
        }
    }

    private IEnumerator GrowOverTime()
    {
        while (true)
        {
            Vector3 targetScale = transform.localScale * growthFactor; // Calculate new target scale
            float t = 0f;

            while (t < 1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t);
                t += Time.deltaTime / growDuration; // Smooth transition over time
                yield return null; // Wait for next frame
            }

            yield return new WaitForSeconds(growInterval); // Wait before next growth
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //finds the gameover script and triggers the fade
            FindFirstObjectByType<GameOverScreen>().TriggerGameOver();
        }
    }
}