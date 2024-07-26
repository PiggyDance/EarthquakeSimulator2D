using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeController : MonoBehaviour
{
  public float earthquakeDuration = 5.0f;
  public float horizontalMagnitude = 0.5f;
  public float verticalMagnitude = 0.3f;
  private bool isShaking = false;
  private float shakeTimer = 0.0f;

  void Update()
  {
    if (isShaking)
    {
      if (shakeTimer > 0)
      {
        Shake();
        shakeTimer -= Time.deltaTime;
      }
      else
      {
        isShaking = false;
      }
    }
  }

  public void StartEarthquake()
  {
    isShaking = true;
    shakeTimer = earthquakeDuration;
  }

  void Shake()
  {
    Rigidbody2D[] rigidbodies = FindObjectsOfType<Rigidbody2D>();
    foreach (Rigidbody2D rb in rigidbodies)
    {
      if (rb.gameObject.tag == "Ground")
      {
        Vector2 horizontalShake = new Vector2(Random.Range(-1f, 1f) * horizontalMagnitude, 0);
        rb.AddForce(horizontalShake, ForceMode2D.Impulse);
      }
      else
      {
        Vector2 topLevelShake = new Vector2(Random.Range(-0.1f, 0.1f) * horizontalMagnitude, 0);
        rb.AddForce(topLevelShake, ForceMode2D.Impulse);
      }
    }
  }
}