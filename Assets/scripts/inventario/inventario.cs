using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Equipo : MonoBehaviour
{
    public static Equipo Instance { get; private set; }

    [Header("Referencias")]
    [SerializeField] private Jugador jugador;
    [SerializeField] private TextMeshProUGUI textoDinero;
    [SerializeField] private GameObject objetoDeEquipo;
    [SerializeField] private Transform contenedorEquipo;

    [Header("Parámetros de inventario")]
    [SerializeField] private int limiteObjetos = 12;
    [SerializeField] private List<Sprite> catalogoSprites;

    private int numeroMaximoObjetos = 0;
    private Dictionary<Sprite, (GameObject instancia, int cantidad)> objetosComprados = new();
    private Dictionary<Sprite, int> efectosCurativos = new();

    public int dinero
    {
        get
        {
            if (jugador == null)
            {
                Debug.LogWarning("Jugador no asignado en Equipo, devolviendo 0 de dinero.");
                return 0;
            }
            return jugador.dinero;
        }
        set
        {
            if (jugador == null)
            {
                Debug.LogWarning("Jugador no asignado en Equipo, no se puede asignar dinero.");
                return;
            }
            jugador.dinero = value;
            ActualizarTextoDinero();
        }
    }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        ActualizarInventarioVisual();
    }

    private void Start()
    {
        ActualizarTextoDinero();
    }

    private void ReasignarReferencias()
    {
        if (jugador == null)
            jugador = Object.FindFirstObjectByType<Jugador>();

        if (textoDinero == null)
            textoDinero = GameObject.Find("TextoDinero")?.GetComponent<TextMeshProUGUI>();

        if (contenedorEquipo == null)
            contenedorEquipo = GameObject.Find("ContenedorEquipo")?.transform;

        if (objetoDeEquipo == null)
            objetoDeEquipo = Resources.Load<GameObject>("ObjetoDeEquipo"); // Si lo guardaste en Resources

        if (jugador == null)
            Debug.LogWarning("⚠ No se encontró el jugador en la nueva escena.");

        if (textoDinero == null)
            Debug.LogWarning("⚠ No se encontró el texto del dinero en la nueva escena.");

        if (contenedorEquipo == null)
            Debug.LogWarning("⚠ No se encontró el contenedor de equipo en la nueva escena.");
    }

    public void AsignarJugador(Jugador nuevoJugador)
    {
        jugador = nuevoJugador;
        ActualizarTextoDinero();
    }

    public IReadOnlyDictionary<Sprite, (GameObject instancia, int cantidad)> GetObjetosComprados()
    {
        return objetosComprados;
    }

    private void ActualizarTextoDinero()
    {
        if (textoDinero != null)
            textoDinero.text = dinero.ToString();
    }

    public void IncluirEquipo(int costo, Image imagenEquipo, bool esCurativo = false, int cantidadCura = 0)
    {
        if (imagenEquipo == null || imagenEquipo.sprite == null) return;

        if (jugador == null)
        {
            Debug.LogWarning("Jugador no asignado en Equipo, no se puede incluir equipo.");
            return;
        }

        if (dinero < costo)
        {
            Debug.Log("💰 Dinero insuficiente.");
            return;
        }

        if (numeroMaximoObjetos >= limiteObjetos)
        {
            Debug.Log("📦 Límite de objetos alcanzado.");
            return;
        }

        Sprite sprite = imagenEquipo.sprite;
        dinero -= costo;
        numeroMaximoObjetos++;

        if (esCurativo && cantidadCura > 0 && !efectosCurativos.ContainsKey(sprite))
        {
            efectosCurativos.Add(sprite, cantidadCura);
        }

        if (objetosComprados.TryGetValue(sprite, out var data))
        {
            data.cantidad++;
            objetosComprados[sprite] = data;
            ActualizarContador(data.instancia, data.cantidad);
        }
        else
        {
            GameObject instancia = CrearObjetoVisual(sprite, 1);
            objetosComprados.Add(sprite, (instancia, 1));
        }
    }

    public void UsarItem(Sprite sprite)
    {
        if (jugador == null)
        {
            Debug.LogWarning("Jugador no asignado en Equipo, no se puede usar ítem.");
            return;
        }

        if (!objetosComprados.TryGetValue(sprite, out var data))
        {
            Debug.Log("❌ El ítem no está en el inventario.");
            return;
        }

        if (efectosCurativos.TryGetValue(sprite, out int cura))
        {
            jugador?.UsarObjetoCurativo(sprite, cura);
        }
        else
        {
            Debug.Log("ℹ Este ítem no tiene efecto curativo.");
        }

        data.cantidad--;

        if (data.cantidad <= 0)
        {
            Destroy(data.instancia);
            objetosComprados.Remove(sprite);
            numeroMaximoObjetos--;
        }
        else
        {
            objetosComprados[sprite] = data;
            ActualizarContador(data.instancia, data.cantidad);
        }
    }

    public void ActualizarInventarioVisual()
    {
        ActualizarTextoDinero();

        foreach (var kvp in objetosComprados)
        {
            ActualizarContador(kvp.Value.instancia, kvp.Value.cantidad);
        }
    }

    private void ActualizarContador(GameObject objeto, int cantidad)
    {
        if (objeto == null) return;
        var contador = objeto.transform.Find("ContadorTexto")?.GetComponent<TextMeshProUGUI>();
        if (contador != null)
        {
            contador.text = $"x{cantidad}";
        }
    }

    private GameObject CrearObjetoVisual(Sprite sprite, int cantidad)
    {
        if (objetoDeEquipo == null)
        {
            Debug.LogError("❌ Objeto de equipo no asignado.");
            return null;
        }

        GameObject equipo = Instantiate(objetoDeEquipo, Vector2.zero, Quaternion.identity, contenedorEquipo);

        Image imagen = equipo.GetComponent<Image>();
        if (imagen != null)
        {
            imagen.sprite = sprite;
        }

        ActualizarContador(equipo, cantidad);

        Button btn = equipo.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => UsarItem(sprite));
        }

        return equipo;
    }

    public Sprite BuscarSpritePorNombre(string nombre)
    {
        return catalogoSprites.Find(spr => spr.name == nombre);
    }

    public void AgregarObjetoCargado(Sprite sprite, int cantidad)
    {
        if (sprite == null || cantidad <= 0) return;

        if (objetosComprados.TryGetValue(sprite, out var data))
        {
            data.cantidad += cantidad;
            objetosComprados[sprite] = data;
            ActualizarContador(data.instancia, data.cantidad);
        }
        else
        {
            GameObject instancia = CrearObjetoVisual(sprite, cantidad);
            objetosComprados.Add(sprite, (instancia, cantidad));
        }

        numeroMaximoObjetos += cantidad;
    }

    public void LimpiarInventario()
    {
        foreach (var kvp in objetosComprados)
        {
            Destroy(kvp.Value.instancia);
        }

        objetosComprados.Clear();
        numeroMaximoObjetos = 0;
        ActualizarTextoDinero();
    }

    public int TomarCuraDeSprite(Sprite sprite)
    {
        if (efectosCurativos.TryGetValue(sprite, out int cura))
            return cura;
        return 0;
    }
}
