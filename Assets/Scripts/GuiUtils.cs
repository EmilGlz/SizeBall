using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class GuiUtils
{
    public static GameObject FindGameObject(string name, GameObject parentOrSelf)
    {
        if (parentOrSelf == null)
            return null;
        if (parentOrSelf.name == name)
            return parentOrSelf;
        var components = parentOrSelf.GetComponentsInChildren<Transform>(true);
        foreach (Transform component in components)
        {
            if (component.gameObject.name == name)
                return component.gameObject;
        }
        return null;
    }
    public static GameObject FindGameObjectBySubstr(string str, GameObject parentOrSelf)
    {
        if (parentOrSelf == null)
            return null;
        if (parentOrSelf.name.IndexOf(str, StringComparison.Ordinal) != -1)
            return parentOrSelf;
        var components = parentOrSelf.GetComponentsInChildren<Transform>(true);
        foreach (Transform component in components)
        {
            if (component.gameObject.name.IndexOf(str, StringComparison.Ordinal) != -1)
                return component.gameObject;
        }

        return null;
    }
    public static T FindGameObject<T>(string name, Transform parentOrSelf)
        where T : MonoBehaviour
    {
        if (parentOrSelf == null)
            return null;
        var go = FindGameObject(name, parentOrSelf.gameObject);
        if (go == null) return null;
        return go.GetComponent<T>();
    }

    public static void ForceUpdateLayout(RectTransform rectTransform)
    {
        if (rectTransform == null)
            return;

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
    public static void DestroyAllChildren<T>(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<T>() != null)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    public static void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public static void DestroyAllChildrenByLayer(Transform parent, int ignoreLayer)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.layer != ignoreLayer)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    public static void DoExtendAnimation(GameObject panel, float animTIme = 0.5f)
    {
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(1, animTIme);
    }
    public static void DoCloseAnimation(GameObject panel, float animTIme = 0.5f)
    {
        panel.transform.localScale = Vector3.one;
        panel.transform.DOScale(0, animTIme);
    }
}