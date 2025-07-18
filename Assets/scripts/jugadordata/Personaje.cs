using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Juego/Personaje")]
public class Personaje : ScriptableObject
{
    [Header("Datos del Personaje")]
    public Sprite retrato;
    public int vidaBase = 100;
    public int felicidadInicial = 100;

    [Header("Prefab jugable")]
    [Tooltip("Prefab del personaje que se instancia en la escena")]
    public GameObject prefab;
}
