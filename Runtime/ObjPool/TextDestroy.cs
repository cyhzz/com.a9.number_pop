using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.A9.DmgPop
{
    public class TextDestroy : MonoBehaviour
    {
        public int id;
        public void Destroy()
        {
            PopText.instance.Release(this);
        }
    }
}
