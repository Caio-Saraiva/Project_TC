using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public JsonLoader jsonLoader; // O carregador do JSON
    public DynamicPanel dynamicPanel; // O painel dinâmico que será atualizado

    public void OnButtonClick()
    {
        // Obter o componente PrefabItemData para acessar o itemId
        PrefabItemData itemData = GetComponent<PrefabItemData>();

        if (itemData != null)
        {
            int itemId = itemData.itemId; // Pega o ID armazenado no PrefabItemData

            // Busca o elemento pelo ID no JSON
            ItemsShop item = jsonLoader.GetElementById(itemId);
            if (item != null)
            {
                // Atualiza o painel dinâmico com o elemento correspondente
                dynamicPanel.UpdatePanel(item);
            }
            else
            {
                Debug.LogError("Element with ID " + itemId + " not found.");
            }
        }
        else
        {
            Debug.LogError("PrefabItemData not found on this GameObject.");
        }
    }
}
