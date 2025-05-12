using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float deleteInSeconds = 5f;

    private void Start()
    {
        Destroy(gameObject, deleteInSeconds);
    }
}
