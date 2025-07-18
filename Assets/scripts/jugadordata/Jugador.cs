using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public static Jugador Instance { get; private set; }

    [Header("Parámetros del jugador")]
    public int saludMaxima = 100;
    public int saludActual;
    public int dinero = 100;
    public float intervaloDaño = 3f;

    private Equipo inventario;

    private void Awake()
    {
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
        StartCoroutine(EsperarYAsignarEquipo());

        // Reposicionar al jugador
        GameObject puntoInicio = GameObject.Find("PuntoInicioJugador");
        if (puntoInicio != null)
        {
            transform.position = puntoInicio.transform.position;
        }
        else
        {
            Debug.LogWarning("⚠ No se encontró el objeto 'PuntoInicioJugador'.");
        }

        // Asegurar que la cámara lo siga
        StartCoroutine(ReasignarCamara());
    }

    private IEnumerator ReasignarCamara()
    {
        yield return new WaitForEndOfFrame();
        CamaraSeguimiento camara = CamaraSeguimiento.Instance;
        if (camara != null)
        {
            camara.AsignarObjetivo(transform);
        }
    }

    private IEnumerator Start()
    {
        yield return EsperarYAsignarEquipo();
        CargarDatos();

        if (saludActual <= 0 || saludActual > saludMaxima)
            saludActual = saludMaxima;

        StartCoroutine(AplicarDañoConIntervalo());
    }

    private IEnumerator EsperarYAsignarEquipo()
    {
        while (Equipo.Instance == null)
            yield return null;

        inventario = Equipo.Instance;
        Equipo.Instance.AsignarJugador(this); // ✅ ENLAZA el jugador al inventario
    }

    private IEnumerator AplicarDañoConIntervalo()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloDaño);
            RecibirDaño(5);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        saludActual = Mathf.Max(saludActual - cantidad, 0);
        Debug.Log("Jugador recibió daño. Salud actual: " + saludActual);
    }

    public void UsarObjetoCurativo(Sprite sprite, int cantidadCura)
    {
        if (sprite != null && cantidadCura > 0)
        {
            if (saludActual >= saludMaxima)
            {
                Debug.Log("La salud ya está al máximo.");
                return;
            }

            saludActual = Mathf.Min(saludActual + cantidadCura, saludMaxima);
            Debug.Log($"Objeto curativo: {sprite.name}, curado {cantidadCura}. Salud actual: {saludActual}");
        }
        else
        {
            Debug.LogWarning("Curación inválida.");
        }
    }

    public void GuardarDatos()
    {
        if (inventario != null)
        {
            SaveSystem.GuardarJuego(this, inventario);
            Debug.Log("✅ Datos guardados desde Jugador.");
        }
    }

    public void CargarDatos()
    {
        if (inventario != null)
        {
            SaveSystem.CargarJuego(this, inventario);
            Debug.Log("✅ Datos cargados en Jugador.");
        }
    }
}
