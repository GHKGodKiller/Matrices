
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class Enemy
{
    public string nombre;

    public Enemy(string nombre)
    {
        this.nombre = nombre;
    }
}
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemigosActivos = new List<Enemy>();

    private int contadorEnemigos = 0;

    private void Start()
    {
        MostrarInstrucciones();
    }

    private void MostrarInstrucciones()
    {
        Debug.Log(
            "Controles de enemigos\n" +
            "   - Tecla [S]: Aparecer (Spawn) un nuevo enemigo -> (.Add())\n" +
            "   - Tecla [K]: Eliminar (Kill) al primer enemigo activo -> (.Remove())\n" +
            "   - Tecla [L]: Listar todos los enemigos activos en la consola\n" +
            "   - Tecla [C]: Limpiar todos los enemigos -> (.Clear())\n" +
            ""
        );
    }
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SpawnEnemigo();
        }
        else if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            EliminarPrimerEnemigo();
        }
        else if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            MostrarEnemigosActivos();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            LimpiarTodosLosEnemigos();
        }
    }
    public void SpawnEnemigo()
    {
        contadorEnemigos++;
        string[] tipos = { "Orco", "Goblin", "Esqueleto", "Dragón" };
        string tipoAleatorio = tipos[Random.Range(0, tipos.Length)];
        
        Enemy nuevoEnemigo = new Enemy($"{tipoAleatorio} #{contadorEnemigos}");

        enemigosActivos.Add(nuevoEnemigo);

        Debug.Log($"[SPAWN] ¡Ha aparecido '{nuevoEnemigo.nombre}'! " +
                  $"| Total activos en la lista: {enemigosActivos.Count}");
    }
    public void EliminarPrimerEnemigo()
    {
        if (enemigosActivos.Count == 0)
        {
            Debug.LogWarning("[GESTOR] No hay enemigos activos en la escena para eliminar.");
            return;
        }

        Enemy enemigoMuerto = enemigosActivos[0];

        enemigosActivos.Remove(enemigoMuerto);

        Debug.Log($"[MUERTE] ¡'{enemigoMuerto.nombre}' ha sido derrotado! Se eliminó de la lista. " +
                  $"| Quedan activos: {enemigosActivos.Count}");
    }
    public void MostrarEnemigosActivos()
    {
        Debug.Log($"=== Enemigos activos ({enemigosActivos.Count}) ===");

        if (enemigosActivos.Count == 0)
        {
            Debug.Log("La escena está vacía, no hay enemigos activos.");
            return;
        }

        for (int i = 0; i < enemigosActivos.Count; i++)
        {
            Debug.Log($"Índice [{i}]: {enemigosActivos[i].nombre}");
        }
    }
    public void LimpiarTodosLosEnemigos()
    {
        int cantidadAnterior = enemigosActivos.Count;

        enemigosActivos.Clear();

        Debug.Log($"[CLEAR] Se limpiaron todos los enemigos ({cantidadAnterior}). " +
                  $"Total activos ahora: {enemigosActivos.Count}");
    }
}