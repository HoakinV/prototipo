using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] private GameObject prefabObjetoTienda;
    [SerializeField] private int numeroMaximoObjetosTienda = 5;
    [SerializeField] private List<PlantillaObjeto> listaTienda;

    private List<PlantillaObjeto> listaProvisionalTienda;

    private void Start()
    {
        if (prefabObjetoTienda == null)
        {
            Debug.LogError("❌ Prefab de objeto tienda no asignado.");
            return;
        }

        if (listaTienda == null || listaTienda.Count == 0)
        {
            Debug.LogError("❌ Lista de tienda vacía o no asignada.");
            return;
        }

        Transform contenedorTienda = GameObject.FindGameObjectWithTag("Tienda")?.transform;
        if (contenedorTienda == null)
        {
            Debug.LogError("❌ No se encontró un objeto con el tag 'Tienda'.");
            return;
        }

        listaProvisionalTienda = new List<PlantillaObjeto>(listaTienda);
        int cantidadObjetos = Mathf.Min(numeroMaximoObjetosTienda, listaProvisionalTienda.Count);

        for (int i = 0; i < cantidadObjetos; i++)
        {
            int indice = Random.Range(0, listaProvisionalTienda.Count);
            PlantillaObjeto plantilla = listaProvisionalTienda[indice];
            InstanciarObjetoTienda(plantilla, contenedorTienda);
            listaProvisionalTienda.RemoveAt(indice); // Evitar duplicados
        }
    }

    private void InstanciarObjetoTienda(PlantillaObjeto plantilla, Transform contenedor)
    {
        GameObject instancia = Instantiate(prefabObjetoTienda, Vector2.zero, Quaternion.identity, contenedor);

        Objeto objeto = instancia.GetComponent<Objeto>();
        if (objeto != null)
        {
            objeto.CrearObjeto(plantilla);
        }
        else
        {
            Debug.LogWarning("⚠ El prefab no tiene el componente 'Objeto'.");
        }
    }
}
