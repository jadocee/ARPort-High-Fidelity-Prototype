using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.MixedReality.OpenXR;
using UnityEngine;

namespace Widgets
{
    public class WidgetPanel : MonoBehaviour
    {
        private static readonly LinkedList<Widget> widgets = new LinkedList<Widget>();
        private bool hidden;

        public WidgetPanel()
        {
            hidden = true;
        }

        public bool IsHidden()
        {
            return hidden;
        }

        public void SetHidden(bool hidden)
        {
            this.hidden = hidden;
        }


        private void OnEnable()
        {
            gameObject.SetActive(hidden);
        }

        public void AddWidget(Widget widget, int priority = 0)
        {
            var newWidgetGameObject = Instantiate(widget.gameObject, transform, false);
            if (priority > 0)
            {
                var newWidget = newWidgetGameObject.GetComponent<Widget>();
                newWidget.SetPriority(priority);
                var current = widgets.First;
                if (current == null) return;
                var i = 0;
                while (true)
                {
                    if (current.Next == null)
                    {
                        widgets.AddLast(newWidget);
                        newWidgetGameObject.transform.SetSiblingIndex(i);
                        break;
                    }
                    if (current.Next.Value.GetPriority() < newWidget.GetPriority())
                    {
                        widgets.AddBefore(current.Next, newWidget);
                        newWidgetGameObject.transform.SetSiblingIndex(i);
                        break;
                    }
                    current = current.Next;
                    i++;
                }
            }
            else
            {
                widgets.AddLast(widget);
            }
        }

        public void RemoveWidget(string widgetName)
        {
            Widget current;
        }
    }
}