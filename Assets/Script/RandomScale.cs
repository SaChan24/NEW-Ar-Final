using UnityEngine;

public class SmoothScale : MonoBehaviour
{
    public float minScale = 0.04680103f;
    public float maxScale = 0.08f;
    public float speed = 2f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool growing = true;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * maxScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        if (Vector3.Distance(transform.localScale, targetScale) < 0.001f)
        {
            growing = !growing;
            targetScale = growing ? originalScale * maxScale : originalScale * minScale;
        }
    }
}