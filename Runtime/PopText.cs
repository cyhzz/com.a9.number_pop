using System.Collections;
using System.Collections.Generic;
using Com.A9.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace Com.A9.DmgPop
{
    public class PopText : Singleton<PopText>
    {
        public const int max_count = 600;
        public GameObject num_text;

        public Dictionary<GameObject, GameObject> active_dic = new Dictionary<GameObject, GameObject>();
        public List<GameObject> released_list = new List<GameObject>();

        void Start()
        {
            Vector3 hide = Vector3.one;
            for (int i = 0; i < max_count; i++)
            {
                Aquire(hide, null);
            }
        }

        public void ClearAll()
        {
            active_dic.Clear();
            released_list.Clear();
        }

        GameObject Aquire(Vector3 pos, Transform parent)
        {
            GameObject to_alloc = null;
            if (released_list.Count != 0)
            {
                to_alloc = released_list[released_list.Count - 1];
                released_list.RemoveAt(released_list.Count - 1);
                to_alloc.transform.position = pos;
                active_dic[to_alloc] = to_alloc;
                to_alloc.GetComponentInChildren<Text>().enabled = true;
            }
            else if (active_dic.Count < max_count)
            {
                to_alloc = GameObject.Instantiate(num_text, pos, Quaternion.identity, parent);
                active_dic.Add(to_alloc, to_alloc);
                to_alloc.GetComponentInChildren<Text>().enabled = true;
            }

            return to_alloc;
        }

        public void Release(TextDestroy des)
        {
            GameObject to_release = active_dic[des.gameObject];
            to_release.GetComponentInChildren<Text>().enabled = false;
            to_release.transform.Find("crit").gameObject.SetActive(false);
            released_list.Add(to_release);
        }

        public void Text(string text, int text_size, Vector3 pos, Color col, Transform parent = null, bool is_crit = false)
        {
            if (text == "")
                return;

            GameObject go = Aquire(pos, parent);
            if (go == null)
            {
                return;
            }
            go.GetComponent<Animator>().Play("pop", 0, 0);

            if (is_crit)
            {
                go.transform.Find("crit").gameObject.SetActive(true);
            }

            var tmesh = go.GetComponentInChildren<Text>();
            tmesh.text = text;
            tmesh.fontSize = text_size;

            tmesh.color = col;
        }
    }
}

