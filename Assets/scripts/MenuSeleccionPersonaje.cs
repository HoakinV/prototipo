using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSelectionPersonaje : MonoBehaviour
{
    [Header("Componentes UI")]
    [SerializeField] private Image imagenPersonaje;
    [SerializeField] private TextMeshProUGUI nombrePersonaje;

    private int indexPersonaje;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null || gameManager.Personajes.Count == 0)
        {
            Debug.LogError("GameManager o personajes no disponibles.");
            return;
        }

        indexPersonaje = PlayerPrefs.GetInt("JugadorIndex", 0);
        ActualizarPantalla();
    }

    public void Siguiente()
    {
        indexPersonaje = (indexPersonaje + 1) % gameManager.Personajes.Count;
        ActualizarPantalla();
    }

    public void Anterior()
    {
        indexPersonaje = (indexPersonaje - 1 + gameManager.Personajes.Count) % gameManager.Personajes.Count;
        ActualizarPantalla();
    }

    private void ActualizarPantalla()
    {
        var personaje = gameManager.Personajes[indexPersonaje];
        imagenPersonaje.sprite = personaje.retrato;
        nombrePersonaje.text = personaje.name;

        PlayerPrefs.SetInt("JugadorIndex", indexPersonaje);
        PlayerPrefs.Save();
    }
}
