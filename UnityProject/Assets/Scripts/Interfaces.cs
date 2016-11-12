using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//---------------------------------------------------------------


public interface Creator
{
    GameObject getActionObject();
    ActionManager getActionManager();
    GameObject getMenuObject();
    ContextMenuManager getMenuManager();
    void setupLaser(LaserPicker laser);
}

public interface ContextMenuManager
{
    void cmStart(LaserPicker laser);
    void cmUpdate(LaserPicker laser);
}

public interface ActionManager
{
    void amStart(LaserPicker laser);
    void amUpdate(LaserPicker laser);
}


//---------------------------------------------------------------


public interface Highlightable
{
    void highlightObject();
}


public interface ObjectMenu
{
    void edit();
    void delete();
}


//---------------------------------------------------------------


public interface Editable
{
    void enterEditMode();
    void exitEditMode();
    void toggleEditMode();
}

public interface Infoable
{
    SortedDictionary<string, string> getInfoDict();
    string getInfoString();

    void setName(string name);
    string getName();

    List<string> getTags();
    bool hasTag(string tag);

    void addTag(string tag);
    void addTags(List<string> tagList);

    Vector3 getPosition();

    void setLayer(int layer);
    int getLayer();
    bool isOnLayer(int layer);
}


//---------------------------------------------------------------


public interface RefObjectManager
{
    void showRefObjects();
    void hideRefObjects();
    void toggleRefObjects();
    void adjustEdgeHandles();
    Vector3 getPtCenter();
}


//---------------------------------------------------------------


public interface MeshMaker
{
    void updateMesh();
}

