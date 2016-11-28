using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Wall : SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Wall refObj;



    //---------------------------------------------------------------



    private void createSnapObjects()
    {
        List<List<Vector3>> ptSets = refObj.getAllPtSets();
        List<bool> setVisible = refObj.getAllVisibilityList();

        for(int j=0; j<ptSets.Count; j++)
        {
            if (!setVisible[j]) continue;
            List<Vector3> pts = ptSets[j];
            Vector3 midPt;

            foreach (Vector3 pt in pts)
            {
                base.createAndPlaceSnapObj(pt, pt, SnapType.END);
            }

            for (int i = 0; i < pts.Count - 1; i++)
            {
                base.createAndPlaceSnapObj(pts[i], pts[i + 1]);
                midPt = (pts[i] + pts[i + 1]) / 2;
                base.createAndPlaceSnapObj(midPt, midPt, SnapType.MID);
            }

            // the fourth side
            base.createAndPlaceSnapObj(pts[pts.Count-1], pts[0]);
            midPt = (pts[pts.Count - 1] + pts[0]) / 2;
            base.createAndPlaceSnapObj(midPt, midPt, SnapType.MID);
        }
    }



    //---------------------------------------------------------------


    
    public override void updateSnapObjects()
    {
        base.clearSnapObjects();
        createSnapObjects();
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start ()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Wall>();
        updateSnapObjects();
    }



}
