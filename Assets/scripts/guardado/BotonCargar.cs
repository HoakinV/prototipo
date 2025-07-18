using UnityEngine;
using UnityEngine.UI;

public class BotonCargar : MonoBehaviour
{
    [Header("Referencias necesarias")]
    public Jugador jugador;
    public Equipo inventario;

    [SerializeField] private Button botonCargar;

    void Start()
    {
        if (botonCargar == null)
        {
            botonCargar = GetComponent<Button>();
        }

        if (botonCargar != null)
        {
            botonCargar.onClick.AddListener(Cargar);
        }
        else
        {
            Debug.LogError("❌ Botón de carga no asignado ni encontrado automáticamente.");
        }
    }

    void Cargar()
    {
        if (jugador == null || inventario == null)
        {
            Debug.LogError("❌ No se puede cargar: referencias faltantes.");
            return;
        }

        jugador.CargarDatos(); // Llama al método de Jugador para cargar todo (salud, dinero, posición, inventario)
        Debug.Log("✅ Partida cargada desde botón.");
    }
}
