using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Item
{
    public string nombreItem;
    public Sprite icono;

    public Item(string nombre)
    {
        this.nombreItem = nombre;
    }
}

public class HotbarController : MonoBehaviour
{
    [Header("Hotbar State")]
    [SerializeField] private Item[] hotbar = new Item[5];
    [SerializeField] private Item itemSostenido = null;

    private void Start()
    {
        MostrarInstrucciones();
    }

    private void MostrarInstrucciones()
    {
        Debug.Log(
            "CONTROLES De accesorios\n" +
            "1. Seleccionar item\n" +
            "   - Tecla [E]: Espada de Hierro\n" +
            "   - Tecla [V]: Pocion de Vida\n" +
            "   - Tecla [A]: Antorcha\n" +
            "   - Tecla [M]: Manzana\n" +
            "   - Tecla [D]: Pocion de Daño\n\n" +
            "2. Acciones de Slor (1 al 5):\n" +
            "   - [Número]: Meter o Cambiar item del slot por el item en mano.\n" +
            "   - [Shift + Número]: Tirar / Descartar item del slot.\n" +
            "   - [Alt + Número]: Usar ítem del slot.\n" +
            ""
        );
    }

    private void Update()
    {
        if (Keyboard.current == null) return;


        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SeleccionarItemParaMano("Espada de Hierro");
        }
        else if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            SeleccionarItemParaMano("Poción de Vida");
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            SeleccionarItemParaMano("Antorcha");
        }
        else if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SeleccionarItemParaMano("Manzana");
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SeleccionarItemParaMano("Poción de Daño");
        }


        int slotIndex = ObtenerSlotPresionado();

        if (slotIndex != -1)
        {
            bool holdingShift = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
            bool holdingAlt = Keyboard.current.leftAltKey.isPressed || Keyboard.current.rightAltKey.isPressed;

            if (holdingShift)
            {
                TirarItem(slotIndex);
            }
            else if (holdingAlt)
            {
                UsarItem(slotIndex);
            }
            else
            {
                MeterOCambiarItem(slotIndex);
            }
        }
    }

    private void SeleccionarItemParaMano(string nombre)
    {
        itemSostenido = new Item(nombre);
        Debug.Log($"[Mano] Has seleccionado '{itemSostenido.nombreItem}'. Presiona del 1 al 5 para guardarlo en un slot.");
    }

    private int ObtenerSlotPresionado()
    {
        var kb = Keyboard.current;
        if (kb.digit1Key.wasPressedThisFrame || kb.numpad1Key.wasPressedThisFrame) return 0;
        if (kb.digit2Key.wasPressedThisFrame || kb.numpad2Key.wasPressedThisFrame) return 1;
        if (kb.digit3Key.wasPressedThisFrame || kb.numpad3Key.wasPressedThisFrame) return 2;
        if (kb.digit4Key.wasPressedThisFrame || kb.numpad4Key.wasPressedThisFrame) return 3;
        if (kb.digit5Key.wasPressedThisFrame || kb.numpad5Key.wasPressedThisFrame) return 4;
        return -1;
    }


    public void MeterOCambiarItem(int indice)
    {
        if (!EsIndiceValido(indice)) return;

        if (itemSostenido == null || string.IsNullOrEmpty(itemSostenido.nombreItem))
        {
            Debug.LogWarning("[Hotbar] No tienes ningún ítem en mano. Selecciona uno primero con las teclas (E, P, A, M, C).");
            return;
        }

        Item actual = hotbar[indice];

        if (actual != null && !string.IsNullOrEmpty(actual.nombreItem))
        {
            Debug.Log($"[Hotbar] Reemplazando '{actual.nombreItem}' por '{itemSostenido.nombreItem}' en el slot {indice + 1}.");
        }
        else
        {
            Debug.Log($"[Hotbar] Guardando '{itemSostenido.nombreItem}' en el slot {indice + 1}.");
        }

        hotbar[indice] = itemSostenido;
        itemSostenido = null;
    }

    public void TirarItem(int indice)
    {
        if (!EsIndiceValido(indice)) return;

        if (hotbar[indice] != null && !string.IsNullOrEmpty(hotbar[indice].nombreItem))
        {
            Debug.Log($"[Hotbar] Se tiró el ítem '{hotbar[indice].nombreItem}' del slot {indice + 1}.");
            hotbar[indice] = null;
        }
        else
        {
            Debug.Log($"[Hotbar] El slot {indice + 1} está vacío, no hay nada que tirar.");
        }
    }

    public void UsarItem(int indice)
    {
        if (!EsIndiceValido(indice)) return;

        Item item = hotbar[indice];
        if (item != null && !string.IsNullOrEmpty(item.nombreItem))
        {
            Debug.Log($"[Hotbar] Usando el ítem '{item.nombreItem}' del slot {indice + 1}!");
        }
        else
        {
            Debug.Log($"[Hotbar] El slot {indice + 1} está vacío, nada que usar.");
        }
    }

    private bool EsIndiceValido(int indice)
    {
        if (indice >= 0 && indice < hotbar.Length) return true;
        Debug.LogWarning($"[Hotbar] Índice fuera de rango: {indice}.");
        return false;
    }
}