using UnityEngine;

public class PersistenciaGlobal : MonoBehaviour
{
    public static PersistenciaGlobal Instancia { get; private set; }

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false); // Oculto por defecto
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    public void Mostrar() => gameObject.SetActive(true);
    public void Ocultar() => gameObject.SetActive(false);
}
