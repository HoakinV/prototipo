using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscena : MonoBehaviour
{
    private void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;

        if (PersistenciaGlobal.Instancia != null)
        {
            if (nombreEscena == "EscenaJuego" || nombreEscena == "EscenaInventario")
                PersistenciaGlobal.Instancia.Mostrar();
            else
                PersistenciaGlobal.Instancia.Ocultar();
        }
    }
}
