using UnityEngine;
using TMPro;

public class CargarNombre : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string claveNombre = "NombreJugador";

    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI textoTMP;  // Asignar en Inspector

    private void Start()
    {
        if (textoTMP == null)
        {
            Debug.LogError("❌ No se asignó el componente TextMeshProUGUI en el inspector.");
            return;
        }

        string nombreGuardado = PlayerPrefs.GetString(claveNombre, "Jugador");
        textoTMP.text = nombreGuardado;
        Debug.Log($"🟢 Nombre cargado correctamente: {nombreGuardado}");
    }

    public void ActualizarNombre(string nuevoNombre)
    {
        if (textoTMP == null)
        {
            Debug.LogWarning("❌ No se puede actualizar el nombre porque textoTMP es null.");
            return;
        }

        textoTMP.text = nuevoNombre;
        Debug.Log($"🔄 Nombre actualizado a: {nuevoNombre}");
    }
}
