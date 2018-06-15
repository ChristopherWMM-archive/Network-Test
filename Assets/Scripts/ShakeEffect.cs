using UnityEngine;
using System.Collections;

public class ShakeEffect : MonoBehaviour {

    public bool isShaking = false;
    private Vector3 backupStartPosition = Vector3.zero;
    private Vector3 startPosition = Vector3.zero;

    void Start()
    {
        backupStartPosition = gameObject.transform.position;
    }

    public void StartShaking(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
            isShaking = true;
            float elapsed = 0.0f;

            startPosition = gameObject.transform.position;

            while (elapsed < duration)
            {

                elapsed += Time.deltaTime;

                float percentComplete = elapsed / duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper;
                y *= magnitude * damper;

                gameObject.transform.position = new Vector3(x, y, startPosition.z);

                yield return null;
            }

            isShaking = false;
            if (startPosition == backupStartPosition)
            {
                gameObject.transform.position = startPosition;
            }
            else
            {
                gameObject.transform.position = backupStartPosition;
            }
        

    }
}
