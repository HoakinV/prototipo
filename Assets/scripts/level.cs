using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaCambioEscena : MonoBehaviour
{
    public string nombreDeLaEscenaDestino; // Aquí escribes el nombre de la escena a la que quieres ir

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que quien toca sea el personaje
        {
            SceneManager.LoadScene(nombreDeLaEscenaDestino);
        }
    }
}
