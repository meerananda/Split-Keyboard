using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvt;
    public UnityEvent contactEvt;
    public UnityEvent actionEvt;
    public UnityEvent defaultEvt;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ProximityState)
            proximityEvt.Invoke();
        else if (state.NewInteractableState == InteractableState.ContactState)
            contactEvt.Invoke();
        else if (state.NewInteractableState == InteractableState.ActionState)
            actionEvt.Invoke();
        else
            defaultEvt.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
