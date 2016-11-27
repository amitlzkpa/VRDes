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
    void moveObject(Vector3 tgtPos);
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


public interface SnapObjectManager
{
    void updateSnapObjects();
}


//---------------------------------------------------------------


public interface MeshMaker
{
    void updateMesh();
}


//---------------------------------------------------------------


public interface AppListener
{
    void onPositionChange(Vector3 pos);
    void onRotationChange(Quaternion rot);
    void onScaleChange(Vector3 scale);
}


//---------------------------------------------------------------


public enum SnapType
{
    MID, END
}


public interface SnapObject
{
    bool isSnap();
    Vector3 getSnapPt(Vector3 inPos);
}

