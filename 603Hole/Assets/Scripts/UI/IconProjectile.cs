using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IconProjectile : MonoBehaviour
{
    [SerializeField] Image image;

    RectTransform rectTransform => transform as RectTransform;

    public float speed;
    public float angularSpeed;
    float t;
    Vector2 startPos;
    Vector2 endPos;
    Vector2 controlPos;
    float scaleFactor;
    UnityEvent callback = new();

    public static Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        float u = 1f - t;
        return u * u * p0 + 2f * u * t * p1 + t * t * p2;
    }

    private void Update()
    {
        t += Time.deltaTime * speed / scaleFactor;
        (transform as RectTransform).anchoredPosition = QuadraticBezier(startPos, controlPos, endPos, t);
        transform.rotation = Quaternion.AngleAxis(Time.deltaTime * angularSpeed, Vector3.forward) * transform.rotation;
        if (t >= 1)
        {
            callback?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 startPos, Vector2 endPos, Sprite sprite, UnityAction callbackAction)
    {
        float screenModifier = (float)Screen.width / 1920;
        startPos /= screenModifier;
        endPos /= screenModifier;
        var maxHeight = 1080; //fixed canvas space

        this.startPos = startPos;
        this.endPos = endPos;
        controlPos = new Vector2(Mathf.Lerp(startPos.x, endPos.x, Random.Range(0.4f, 0.6f)),
            Mathf.LerpUnclamped(startPos.y, maxHeight, Random.Range(-0.1f, 1.0f)));

        (transform as RectTransform).anchoredPosition = startPos;

        image.sprite = sprite;
        callback.AddListener(callbackAction);
        scaleFactor = Mathf.Abs(endPos.x - startPos.x);
        t = 0;
    }
}
