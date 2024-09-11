using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private int buttonId;                  // ID identificador do bot�o
    [SerializeField] private GameObject prefabToInstantiate; // Prefab que ser� instanciado
    [SerializeField] private Transform contentParent;        // Content do ScrollView (pai dos prefabs)
    [SerializeField] private ButtonDataManager dataManager;  // Refer�ncia ao script que gerencia os dados

    // M�todo chamado ao clicar no bot�o
    public void OnButtonClick()
    {
        if (prefabToInstantiate == null || contentParent == null || dataManager == null)
        {
            Debug.LogError("Prefab, Content Parent ou Data Manager n�o configurados no ButtonController.");
            return;
        }

        // Busca os dados baseados no ID
        ButtonData buttonData = dataManager.GetButtonDataById(buttonId);

        if (buttonData == null)
        {
            Debug.LogError("Nenhum dado encontrado para o ID: " + buttonId);
            return;
        }

        // Instancia o prefab dentro do contentParent
        GameObject newItem = Instantiate(prefabToInstantiate, contentParent);

        // Tenta obter o script controlador no prefab instanciado
        PrefabItemController controller = newItem.GetComponent<PrefabItemController>();

        if (controller != null)
        {
            // Configura os campos do prefab instanciado
            controller.Setup(buttonData.title, buttonData.description, buttonData.image);
        }
        else
        {
            Debug.LogError("Prefab instanciado n�o possui o componente PrefabItemController.");
        }
    }
}
