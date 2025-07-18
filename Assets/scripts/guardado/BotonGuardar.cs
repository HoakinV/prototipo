using UnityEngine;
using UnityEngine.UI;

public class BotonGuardar : MonoBehaviour
{
    public Jugador jugador;
    public Equipo inventario;

    [SerializeField] private Button botonGuardar;

    void Start()
    {
        if (botonGuardar == null)
        {
            botonGuardar = GetComponent<Button>();
        }

        if (botonGuardar != null)
        {
            botonGuardar.onClick.AddListener(Guardar);
        }
        else
        {
            Debug.LogError("❌ No se encontró ni se asignó un botón para BotonGuardar.");
        }
    }

    void Guardar()
    {
        if (jugador == null || inventario == null)
        {
            Debug.LogWarning("⚠ No se puede guardar: faltan referencias a Jugador o Inventario.");
            return;
        }

        jugador.GuardarDatos(); // 👈 Llama al método que ya guarda salud, dinero, posición e inventario
        Debug.Log("✅ Juego guardado desde el botón.");
    }
}
