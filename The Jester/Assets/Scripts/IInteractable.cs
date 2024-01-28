using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnSelection();
    public void OnPressLeftClick();
    public void OnPressRightClick();

    public void OnDeSelection();

  
}
