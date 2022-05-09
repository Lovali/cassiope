using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public umi3d.edk.UMI3DModel model;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        umi3d.edk.Transaction transaction = new umi3d.edk.Transaction();
        transaction.reliable = true;
        transaction.AddIfNotNull(model.objectRotation.SetValue(Quaternion.Euler(0, 45, 0)));
        transaction.AddIfNotNull(model.objectPosition.SetValue(model.transform.localPosition + (model.transform.forward * 0.2f)));
        transaction.Dispatch();
        Debug.Log("Open");
    }
}
