using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    [SerializeField] private Transform puntoSpawn;

    private void Start()
    {
        var personaje = GameManager.Instance?.ObtenerPersonajeSeleccionado();

        if (personaje == null || personaje.prefab == null)
        {
            Debug.LogError("❌ Personaje o prefab no asignado en el ScriptableObject.");
            return;
        }

        Vector3 spawnPos = puntoSpawn != null ? puntoSpawn.position : transform.position;

        // Instanciar personaje
        GameObject jugadorGO = Instantiate(personaje.prefab, spawnPos, Quaternion.identity);
        Debug.Log($"✅ Personaje instanciado: {personaje.name}");

        // Asignar el personaje a la cámara personalizada
        CamaraSeguimiento camara = Camera.main.GetComponent<CamaraSeguimiento>();
        if (camara != null)
        {
            camara.AsignarObjetivo(jugadorGO.transform);
        }

        // Buscar el controlador de UI y asignarle el jugador instanciado
        ControlesUI controles = FindFirstObjectByType<ControlesUI>();
        if (controles != null)
        {
            PlayerController2D playerController = jugadorGO.GetComponent<PlayerController2D>();
            if (playerController != null)
            {
                controles.SetJugador(playerController);
            }
            else
            {
                Debug.LogWarning("⚠ El prefab no tiene el componente PlayerController2D.");
            }
        }
        else
        {
            Debug.LogWarning("⚠ No se encontró un objeto con el script ControlesUI.");
        }
    }
}
