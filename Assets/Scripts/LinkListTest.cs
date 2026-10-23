using UnityEngine;
using System.Collections.Generic;

public class LinkListTest : MonoBehaviour
{
    private LinkedList<string> sentence = new LinkedList<string>();

    void Start()
    {
        sentence.AddFirst("Buenos");

        LinkedListNode<string> currentNode = sentence.First;

        sentence.AddLast("dias");

        PrintLinkedList();

        if (currentNode != null)
        {
            currentNode.Value = "malos";
        }

        Debug.Log("Cambio de valor del primer nodo: ");
        PrintLinkedList();

        sentence.RemoveFirst();

        Debug.Log("Quitamos el primero: ");
        PrintLinkedList();
    }

    private void PrintLinkedList()
    {
        foreach (string s in sentence)
        {
            Debug.Log(s);
        }
    }
}