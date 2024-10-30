using UnityEngine;

public class TriggerNoise : MonoBehaviour
{
    [SerializeField] GameObject soundSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundSource.SetActive(true);
            Destroy(gameObject);
        }
    }
}
