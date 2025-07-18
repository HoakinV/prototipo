using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryShopManager : MonoBehaviour
{
    public static InventoryShopManager Instancia { get; private set; }

    [Header("Paneles UI")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Botones Guardar / Cargar")]
    [SerializeField] private GameObject guardarBoton;
    [SerializeField] private GameObject cargarBoton;

    private void Awake()
    {
        // Singleton y persistencia entre escenas
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReasignarReferencias();
    }

    private void ReasignarReferencias()
    {
        if (inventoryPanel == null)
            inventoryPanel = BuscarPorNombre("InventoryPanel");

        if (shopPanel == null)
            shopPanel = BuscarPorNombre("ShopPanel");

        if (guardarBoton == null)
            guardarBoton = BuscarPorNombre("GuardarBoton");

        if (cargarBoton == null)
            cargarBoton = BuscarPorNombre("CargarBoton");

        if (inventoryPanel == null || shopPanel == null)
            Debug.LogWarning("⚠ Algunos paneles no fueron encontrados.");

        if (guardarBoton == null || cargarBoton == null)
            Debug.LogWarning("⚠ Los botones de Guardar o Cargar no fueron encontrados.");
    }

    private GameObject BuscarPorNombre(string nombre)
    {
        GameObject encontrado = GameObject.Find(nombre);
        if (encontrado != null) return encontrado;

        // Si no se encuentra directamente, busca dentro del Canvas
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Transform hijo = canvas.transform.Find(nombre);
            if (hijo != null) return hijo.gameObject;
        }

        return null;
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null || shopPanel == null)
        {
            Debug.LogWarning("⚠ Faltan paneles por asignar.");
            return;
        }

        bool abrirInventario = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(abrirInventario);

        if (abrirInventario)
        {
            shopPanel.SetActive(false);
            OcultarBotonesGuardarCargar();
        }
    }

    public void ToggleShop()
    {
        if (inventoryPanel == null || shopPanel == null)
        {
            Debug.LogWarning("⚠ Faltan paneles por asignar.");
            return;
        }

        bool abrirTienda = !shopPanel.activeSelf;
        shopPanel.SetActive(abrirTienda);

        if (abrirTienda)
        {
            inventoryPanel.SetActive(false);
            OcultarBotonesGuardarCargar();
        }
    }

    public void ToggleSaveLoad()
    {
        if (guardarBoton == null || cargarBoton == null)
        {
            Debug.LogWarning("⚠ No se pueden alternar botones porque están en null.");
            return;
        }

        bool mostrar = !guardarBoton.activeSelf;

        guardarBoton.SetActive(mostrar);
        cargarBoton.SetActive(mostrar);

        if (mostrar)
        {
            if (inventoryPanel != null) inventoryPanel.SetActive(false);
            if (shopPanel != null) shopPanel.SetActive(false);
        }
    }

    private void OcultarBotonesGuardarCargar()
    {
        if (guardarBoton != null) guardarBoton.SetActive(false);
        if (cargarBoton != null) cargarBoton.SetActive(false);
    }
}
