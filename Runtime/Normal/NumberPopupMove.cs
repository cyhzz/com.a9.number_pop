using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Com.A9.UIExt
{
    public class NumberPopupMove : MonoBehaviour
    {
        Vector2 end;
        Vector2 start;
        public float radius;
        bool ok;
        public UnityEvent OnEnd;
        public Text text;

        public Gradient myGradient;
        public float strobeDuration = 2f;

        public void Init(string txt)
        {
            text.text = txt;
        }

        void Start()
        {
            start = transform.position;
            end = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * radius;
        }

        float pg;
        public float duration = 1.0f;
        public float fade_duration = 1.0f;

        void Update()
        {
            float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
            var c = myGradient.Evaluate(t);
            text.color = new Color(c.r, c.g, c.b, text.color.a);

            if (ok)
            {
                return;
            }

            pg += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, CurveManager.instance.Evaluate(CurveType.EASE_OUT_SINE,
            pg / duration));

            if (pg >= duration)
            {
                ok = true;
                OnEnd?.Invoke();
            }
        }

        public void Fade()
        {
            StartCoroutine(Fade_());
        }

        IEnumerator Fade_()
        {
            var c = text.color;
            for (float i = 0; i < fade_duration; i += Time.deltaTime)
            {
                text.color = new Color(c.r, c.g, c.b, 1 - i / fade_duration);
                yield return null;
            }
            Destroy(gameObject);
        }
    }

}
