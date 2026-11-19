using UnityEngine;

public class HopeFlower : MonoBehaviour
{
    public float hopeRestored = 20f;

    private void OnTriggerEnter(Collider other)
    {
        HopeSystem hope =
            other.GetComponentInParent<HopeSystem>();

        if (hope != null)
        {
            hope.GainHope(hopeRestored);

            Destroy(gameObject);
        }
    }
}

