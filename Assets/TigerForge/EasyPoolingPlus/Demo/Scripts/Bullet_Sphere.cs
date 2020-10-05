using UnityEngine;
using TigerForge;

public class Bullet_Sphere : MonoBehaviour
{    
    void OnEnable()
    {
        // When a bullet is active in the game, I destroy it after 2 seconds.
        Invoke("Destroy", 2f);
    }

    void Update()
    {
        // I move the bullets.
        transform.Translate(-10 * Time.deltaTime, 0, 0);
    }

    private void Destroy()
    {
        // After 2 seconds, the bullet is destroyed.
        // The destroy procedure is left to the Destroy() method of EasyPoolingPlusSystem class.
        // In this way, the Destroy() method will remove this bullet according to the Pooler settings.
        EasyPoolingPlusSystem.Destroy(gameObject);
    }
}
