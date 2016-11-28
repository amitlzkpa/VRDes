


    //---------------------------------------------------------------


    private List<AppListener> listeners = new List<AppListener>();


    public void registerListener(AppListener l)
    {
        listeners.Add(l);
    }


    public void deRegisterListener(AppListener l)
    {
        listeners.Remove(l);
    }


    public void notifyPosChange()
    {
        foreach(AppListener l in listeners)
        {
            l.onPositionChange(transform.position);
        }
    }


    public void notifyRotChange()
    {
        foreach (AppListener l in listeners)
        {
            l.onRotationChange(transform.rotation);
        }
    }


    public void notifyScaleChange()
    {
        foreach (AppListener l in listeners)
        {
            l.onScaleChange(transform.localScale);
        }
    }


    public void notifyTransformChange()
    {
        notifyPosChange();
        notifyRotChange();
        notifyScaleChange();
    }