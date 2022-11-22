using UnityEngine.Video;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public static class Extensions
{

    public static List<Item> Union(List<Item> list1, List<Item> list2)
    {
        List<Item> result = new List<Item>();
        foreach (Item item in list1)
        {
            if (list2.Contains(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public static bool isEmpty(this UnityEvent value)
    {
        for (int i = 0; i < value.GetPersistentEventCount(); i++)
        {
            if (value.GetPersistentTarget(i) != null) return false;
        }
        return true;
    }

    public static string GetStrBetweenTag(this string value, string tag)
    {
        if (value.Contains(tag))
        {
            int index = value.IndexOf(tag) + tag.Length;
            return value.Substring(index, value.IndexOf(tag, index) - index);
        }
        else return "";
    }

    public static List<string> GetAllStrBetweenTag(this string value, string tag)
    {
        List<string> returnValue = new List<string>();
        int tagLength = tag.Length;
        while (value.Contains(tag))
        {
            int startIndex = value.IndexOf(tag) + tagLength;
            int endIndex = value.IndexOf(tag, startIndex);
            returnValue.Add(value.Substring(startIndex, endIndex - startIndex));
            value = value.Substring(endIndex + tagLength);
        }
        return returnValue;
    }

    public static string GetStrBetweenTags(this string value, string startTag, string endTag)
    {
        if (value.Contains(startTag) && value.Contains(endTag))
        {
            int index = value.IndexOf(startTag) + startTag.Length;
            return value.Substring(index, value.IndexOf(endTag) - index);
        }
        else return null;
    }

    public static VideoPlayer MakeCopy(this VideoPlayer original, bool copyURL = false)
    {
        var copy = original.gameObject.AddComponent<VideoPlayer>();
        PropertyInfo[] p = original.GetType().GetProperties();

        foreach (PropertyInfo prop in p)
        {
            if (!copyURL && prop.Name.Equals("url"))
                continue;
            try
            {
                prop.SetValue(copy, prop.GetValue(original));
            }
            catch
            { }
        }

        GameObject.Destroy(original);

        return copy;
    }
}
