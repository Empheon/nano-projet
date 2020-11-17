using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour
{
    protected abstract void OnCharacterFocus();
    protected abstract void OnCharacterBlur();
    protected abstract void OnCharacterInteractPositive(GameObject character);
    protected abstract void OnCharacterInteractNegative(GameObject character);
}
