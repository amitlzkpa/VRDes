using UnityEngine;
using System.Collections;

public abstract class AbstractCreator : MonoBehaviour, Creator {

    public GameObject actionManagerPrefab;
    public GameObject menuManagerPrefab;


    public ActionManager getActionManager()
    {
        return actionManagerPrefab.GetComponent<ActionManager>();
    }

    public GameObject getActionObject()
    {
        return actionManagerPrefab;
    }

    public ContextMenuManager getMenuManager()
    {
        return menuManagerPrefab.GetComponent<ContextMenuManager>();
    }

    public GameObject getMenuObject()
    {
        return menuManagerPrefab;
    }


    // to be overridden by each creator
    public virtual void setupLaser(LaserPicker laser)
    {
    }
}
