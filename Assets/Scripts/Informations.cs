using System.Collections;
using System.Collections.Generic;
using umi3d.edk;
using umi3d.edk.interaction;
using UnityEngine;
using static umi3d.edk.interaction.UMI3DInteractable;

public class Informations : MonoBehaviour
{

    [SerializeField] UMI3DInteractable interactable;
    [SerializeField] umi3d.edk.UMI3DNode node;
    // Start is called before the first frame update
    void Start()
    {
        interactable.onHoverEnter.AddListener(displayText);
        interactable.onHoverExit.AddListener(hideText);
    }

    void displayText(HoverEventContent eventContent)
    {
        Transaction transaction = new Transaction();
        transaction.reliable = true;
        transaction.Add(node.objectActive.SetValue(true));
        transaction.Dispatch();
    }

    void hideText(HoverEventContent eventContent)
    {
        Transaction transaction = new Transaction();
        transaction.reliable = true;
        transaction.Add(node.objectActive.SetValue(false));
        transaction.Dispatch();
    }
}
