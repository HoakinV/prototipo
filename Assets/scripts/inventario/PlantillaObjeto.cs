using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Objeto", menuName = "Objetos/Plantilla Objeto")]
public class PlantillaObjeto : ScriptableObject
{
    [Header("Datos visuales")]
    public Sprite imagenObjeto;
    public string textoObjeto;

    [Header("Datos de tienda")]
    [Min(0)] public int precioObjeto;

    [Header("Propiedades especiales")]
    public bool esCurativo;
    [Min(0)] public int cantidadCura;

    // Puedes añadir más tipos de efectos en el futuro, como daño, velocidad, etc.
}
