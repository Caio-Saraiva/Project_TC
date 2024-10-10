using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    // GameObject que ser� ativado/desativado
    public GameObject targetObject;

    // M�todo para alternar o estado do GameObject
    public void ToggleActiveState()
    {
        if (targetObject != null)
        {
            // Inverte o estado ativo/inativo do GameObject
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
