using UnityEngine;

public class ShadowDamage : MonoBehaviour
{
    public float damageAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        HopeSystem playerHope =
            other.GetComponentInParent<HopeSystem>();

        if (playerHope != null)
        {
            playerHope.LoseHope(damageAmount);
        }
    }
}
