using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class IconProjectileHolder : MonoSingleton<IconProjectileHolder>
{
    [SerializeField] private IconProjectile projectilePrefab;
    public float projectileSpeed = 500;
    public float maxAngularSpeed = 180;
    RectTransform rectTransform => transform as RectTransform;

    private void Start()
    {
        projectilePrefab.gameObject.SetActive(false);
    }

    /// <summary>
    /// Input should be in screen space
    /// </summary>
    public void Create(Vector2 startPos, Vector2 endPos, Sprite sprite, UnityAction callbackAction, float projectileSpeedOverride = float.NaN, float maxAngularSpeedOverride = float.NaN)
    {
        IconProjectile newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.gameObject.SetActive(true);
        newProjectile.Init(startPos, endPos, sprite, callbackAction);
        newProjectile.speed = float.IsNaN(projectileSpeedOverride)? projectileSpeed: projectileSpeedOverride;
        float maxAngSpd = float.IsNaN(maxAngularSpeedOverride) ? maxAngularSpeed : maxAngularSpeedOverride;
        newProjectile.angularSpeed = Random.Range(-maxAngSpd, maxAngSpd);
    }
}
