using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TabletView : MonoBehaviour
{
    public GameObject Tablet;
    public MapScreen MapScreen;
    //public MaterialScreen MaterialScreen;
    private WebScreen _materialScreen;
    private IList<ITabletScreen> _screens;

    void Start()
    {
        _materialScreen = new WebScreen(Tablet);

        _screens = new List<ITabletScreen>
        {
            MapScreen,
            _materialScreen
        };
    }

    public void ShowMaterialInfo(string url)//Material material, MaterialInfo info)
    {
        //MaterialScreen.Material = material;
        //MaterialScreen.MaterialInfo = info;

        _materialScreen.Url = url;

        ChangeScreen(_materialScreen);
    }

    public void BackToDefault()
    {
        ChangeScreen(MapScreen);
    }

    private void ChangeScreen(ITabletScreen screen)
    {
        foreach (ITabletScreen tabletScreen in _screens)
            tabletScreen.Hide();

        screen.Show();
    }
}
