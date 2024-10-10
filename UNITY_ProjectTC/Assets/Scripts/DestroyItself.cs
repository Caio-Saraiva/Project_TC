using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    // Tempo em segundos antes de se autodestruir
    public float destructionTime = 1.0f;
    public void SelfDestroy()
    {
        Destroy(gameObject, destructionTime);
    }

}
