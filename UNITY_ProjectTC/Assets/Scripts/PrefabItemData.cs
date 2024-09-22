using UnityEngine;

public class PrefabItemData : MonoBehaviour
{
    public int itemId; // Campo para armazenar o ID do item

    // Método para configurar o ID
    public void SetItemId(int id)
    {
        itemId = id;
    }
}
