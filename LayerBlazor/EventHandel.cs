using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public class EventHandel
    {
        private IDictionary<string, object> eventAction = new Dictionary<string, object>();

        public bool AddEvent(string name, object @event)
        {
            if (!eventAction.ContainsKey(name))
            {
                eventAction.Add(name, @event);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddEventDuplicate(string name, object @event)
        {
            if (!eventAction.ContainsKey(name))
            {
                eventAction.Add(name, @event);
            }
        }

        public bool ClearEventByName(string name)
        {
            if (eventAction.ContainsKey(name))
            {
                eventAction.Remove(name);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearEvent()
        {
            eventAction.Clear();
        }

        [JSInvokable]
        public void CallEvent(string eventName, string param)
        {
            if (eventAction.ContainsKey(eventName))
            {
                if (eventAction[eventName] is Action callEvent)
                {
                    callEvent.Invoke();
                }
                else if (eventAction[eventName] is Action<int> callEvent2)
                {
                    callEvent2.Invoke(int.Parse(param));
                }
                else if (eventAction[eventName] is Action<string, int> callEvent3)
                {
                    var json = JObject.Parse(param);
                    string? value = json.Value<string>("value");
                    int index = json.Value<int>("index");
                    callEvent3.Invoke(value ?? "", index);
                }
            }
        }
    }
}
