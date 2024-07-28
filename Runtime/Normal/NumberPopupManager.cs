using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
namespace Com.A9.UIExt
{
    public class NumberPopupManager : Singleton<NumberPopupManager>
    {
        public Transform grid;
        public GameObject prefab;

        public void Pop(Vector2 pos, string txt, float scale = 1.0f)
        {
            var g = Instantiate(prefab, pos, Quaternion.identity, grid);
            g.transform.localScale = Vector3.one * scale;
            g.GetComponentInChildren<NumberPopupMove>().Init(txt);
        }
    }

}

