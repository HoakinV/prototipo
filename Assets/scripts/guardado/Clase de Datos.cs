using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int saludActual;
    public int dineroTotal;

    public float posX;
    public float posY;
    public float posZ;

    public List<string> itemsComprados = new List<string>();  // Nombres de sprites u objetos
    public List<int> cantidades = new List<int>();            // Cantidades de cada item
    public List<int> curaciones = new List<int>();            // Cantidad de cura (si aplica)
}
