using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  // <-- Agregado

public class SelectName : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TextMeshProUGUI textoNombre; // Texto local opcional
    [SerializeField] private Image luz;
    [SerializeField] private GameObject botonAceptar;

    private void Awake()
    {
        if (luz != null) luz.color = Color.red;
        if (botonAceptar != null) botonAceptar.SetActive(false);
    }

    private void Update()
    {
        if (inputText == null || botonAceptar == null || luz == null) return;

        bool nombreValido = inputText.text.Length >= 4;
        botonAceptar.SetActive(nombreValido);
        luz.color = nombreValido ? Color.green : Color.red;
    }

    public void AceptarNombre()
    {
        if (inputText == null) return;

        string nombreJugador = inputText.text;

        if (nombreJugador.Length >= 4)
        {
            PlayerPrefs.SetString("NombreJugador", nombreJugador);
            PlayerPrefs.Save();

            if (textoNombre != null)
                textoNombre.text = nombreJugador;

            CargarNombre cargador = FindFirstObjectByType<CargarNombre>();
            if (cargador != null)
            {
                cargador.ActualizarNombre(nombreJugador);
            }
            else
            {
                Debug.LogWarning("⚠ No se encontró ningún objeto con el script CargarNombre en la escena.");
            }

            Debug.Log("✅ Nombre aceptado: " + nombreJugador);

            // Cambia a la escena deseada
            SceneManager.LoadScene("fuera");
        }
        else
        {
            Debug.LogWarning("❗ El nombre debe tener al menos 4 caracteres.");
        }
    }
}
