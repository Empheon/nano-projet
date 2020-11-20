using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour
{
    protected abstract void OnCharacterFocus(GameObject character);
    protected abstract void OnCharacterBlur(GameObject character);
    protected abstract void OnCharacterInteract(GameObject character);
    protected abstract void OnStopInteraction();
}
