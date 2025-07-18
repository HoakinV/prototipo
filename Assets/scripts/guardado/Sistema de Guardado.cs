using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string fileName = "savefile.json";
    private static readonly string path = Path.Combine(Application.persistentDataPath, fileName);

    public static void GuardarJuego(Jugador jugador, Equipo inventario)
    {
        if (jugador == null || inventario == null)
        {
            Debug.LogWarning("No se pudo guardar: Jugador o inventario es null.");
            return;
        }

        SaveData data = new SaveData
        {
            saludActual = jugador.saludActual,
            dineroTotal = inventario.dinero,
            itemsComprados = new List<string>()
        };

        foreach (var kvp in inventario.GetObjetosComprados())
        {
            string nombre = kvp.Key != null ? kvp.Key.name : "NULL";
            data.itemsComprados.Add(nombre + "|" + kvp.Value.cantidad);
        }

        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log("✅ Juego guardado correctamente en: " + path);
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Error al guardar el juego: " + ex.Message);
        }
    }

    public static void CargarJuego(Jugador jugador, Equipo inventario)
    {
        if (jugador == null || inventario == null)
        {
            Debug.LogWarning("No se pudo cargar: Jugador o inventario es null.");
            return;
        }

        if (!File.Exists(path))
        {
            Debug.LogWarning("⚠ No se encontró archivo de guardado.");
            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            jugador.saludActual = data.saludActual;
            inventario.dinero = data.dineroTotal;

            inventario.LimpiarInventario();

            foreach (string itemData in data.itemsComprados)
            {
                string[] partes = itemData.Split('|');
                if (partes.Length == 2 && int.TryParse(partes[1], out int cantidad))
                {
                    string nombreSprite = partes[0];
                    Sprite sprite = inventario.BuscarSpritePorNombre(nombreSprite);
                    if (sprite != null)
                    {
                        inventario.AgregarObjetoCargado(sprite, cantidad);
                    }
                    else
                    {
                        Debug.LogWarning($"⚠ Sprite no encontrado para: {nombreSprite}");
                    }
                }
                else
                {
                    Debug.LogWarning($"⚠ Datos inválidos de ítem: {itemData}");
                }
            }

            inventario.ActualizarInventarioVisual();
            Debug.Log("✅ Juego cargado correctamente.");
        }
        catch (Exception ex)
        {
            Debug.LogError("❌ Error al cargar el juego: " + ex.Message);
        }
    }
}
