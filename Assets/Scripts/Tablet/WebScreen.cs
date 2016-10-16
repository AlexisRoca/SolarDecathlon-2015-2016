using UnityEngine;

public class WebScreen : ITabletScreen
{
    private readonly GameObject _tablet;
    private VRWebView _webView;
    public string Url { get; set; }

    public WebScreen(GameObject tablet)
    {
        _tablet = tablet;
    }

    public void Show()
    {
        var prefab = Resources.Load<GameObject>("VRWebSample3D");

        GameObject clone = Object.Instantiate(prefab);
        _webView = clone.GetComponent<VRWebView>();
        _webView.m_URL = Url;
        _webView.gameObject.transform.SetParent(_tablet.transform, false);
    }

    public void Hide()
    {
        if (_webView != null)
            Object.Destroy(_webView.gameObject);
        _webView = null;
    }
}