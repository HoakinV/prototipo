using UnityEngine;

public class MascotaManager : MonoBehaviour
{
    public static MascotaManager Instance;

    // ====== Datos del jugador ======
    [Header("Datos de la Mascota")]
    public string personajeSeleccionado = "Zorro";
    public string nombrePersonaje = "MiMascota";
    public int monedasTotales = 0;
    public float vida = 100f;
    public float felicidad = 100f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ... (métodos se mantienen igual) ...
}