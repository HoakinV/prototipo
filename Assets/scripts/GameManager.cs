using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Datos de juego")]
    [SerializeField] private List<Personaje> personajes = new List<Personaje>();
    public List<Personaje> Personajes => personajes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public Personaje ObtenerPersonajeSeleccionado()
    {
        int index = PlayerPrefs.GetInt("JugadorIndex", 0);
        if (index < 0 || index >= personajes.Count)
        {
            Debug.LogWarning("Índice de personaje inválido. Usando índice 0.");
            index = 0;
        }
        return personajes[index];
    }
}
