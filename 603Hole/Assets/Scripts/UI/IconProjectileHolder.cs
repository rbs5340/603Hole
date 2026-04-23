using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class IconProjectileHolder : MonoSingleton<IconProjectileHolder>
{
    [SerializeField] private IconProjectile projectilePrefab;
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Sprite garlicIcon;
    [SerializeField] private Sprite bikeIcon;
    [SerializeField] private Sprite candyIcon;
    [SerializeField] private Sprite waluigiumIcon;
    public float projectileSpeed = 500;
    public float maxAngularSpeed = 180;
    RectTransform rectTransform => transform as RectTransform;

    private void Start()
    {
        projectilePrefab.gameObject.SetActive(false);
    }

    public static Sprite GetResourceSprite(ResourceType resourceType)
    {
        if (Instance == null || resourceType == ResourceType.None) return null;
        switch (resourceType)
        {
            case ResourceType.Coins:
                return Instance.coinIcon;
            case ResourceType.Garlic:
                return Instance.garlicIcon;
            case ResourceType.Candy:
                return Instance.candyIcon;
            case ResourceType.Bikes:
                return Instance.bikeIcon;
            case ResourceType.Waluigium:
                return Instance.waluigiumIcon;
            default:
                return null;
        }
    }

    /// <summary>
    /// Input should be in screen space
    /// </summary>
    public void Create(Vector2 startPos, Vector2 endPos, Sprite sprite, UnityAction callbackAction, float projectileSpeedOverride = float.NaN, float maxAngularSpeedOverride = float.NaN)
    {
        IconProjectile newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.gameObject.SetActive(true);
        newProjectile.Init(startPos, endPos, sprite, callbackAction);
        newProjectile.speed = float.IsNaN(projectileSpeedOverride) ? projectileSpeed : projectileSpeedOverride;
        float maxAngSpd = float.IsNaN(maxAngularSpeedOverride) ? maxAngularSpeed : maxAngularSpeedOverride;
        newProjectile.angularSpeed = Random.Range(-maxAngSpd, maxAngSpd);
    }
}
