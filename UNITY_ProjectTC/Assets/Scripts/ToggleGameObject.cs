using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    // GameObject que será ativado/desativado
    public GameObject targetObject;

    // Método para alternar o estado do GameObject
    public void ToggleActiveState()
    {
        if (targetObject != null)
        {
            // Inverte o estado ativo/inativo do GameObject
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
