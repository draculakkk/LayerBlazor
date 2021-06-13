using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public class LayerHelper : IDisposable, IAsyncDisposable
    {
        private LayerJsInterop layerJsInterop;
        private EventHandel eventHandel;
        private DotNetObjectReference<EventHandel> objRef;

        public LayerHelper(IJSRuntime jsRuntime)
        {
            layerJsInterop = new LayerJsInterop(jsRuntime);
            eventHandel = new EventHandel();
            objRef = DotNetObjectReference.Create(eventHandel);
        }

        public async Task<int> Alert(string content, LayerOptions? options = null, Action<int>? yes = null)
        {
            string? eventYes = null;
            if (yes != null)
            {
                eventYes = $"LH{yes.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventYes, yes);
            }
            int index = await layerJsInterop.Alert(content, options, eventYes, objRef);
            return index;
        }

        public async Task<int> Confirm(string content, LayerOptions? options = null, Action<int>? yes = null, Action<int>? cancel = null)
        {
            string? eventYes = null;
            string? eventCancel = null;
            if (yes != null)
            {
                eventYes = $"LH{yes.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventYes, yes);
            }
            if (cancel != null)
            {
                eventCancel = $"LH{cancel.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventCancel, eventCancel);
            }

            int index = await layerJsInterop.Confirm(content, options, eventYes, eventCancel, objRef);
            return index;
        }

        public async Task<int> Msg(string content, LayerOptions? options = null, Action? end = null)
        {
            string? eventEnd = null;
            if (end != null)
            {
                eventEnd = $"LH{end.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventEnd, end);
            }
            int index = await layerJsInterop.Msg(content, options, eventEnd, objRef);
            return index;
        }

        public async Task<int> Load(int? icon, LayerOptions? options = null)
        {
            int index = await layerJsInterop.Load(icon, options);
            return index;
        }

        public async Task<int> Tips(string content, string follow, LayerOptions? options)
        {
            int index = await layerJsInterop.Tips(content, follow, options);
            return index;
        }

        public async Task Close(int index, Action? close = null)
        {
            string? eventClose = null;
            if (close != null)
            {
                eventClose = $"LH{close.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventClose, close);
            }
            await layerJsInterop.Close(index, eventClose, objRef);
        }

        public async Task CloseAll(string? type = null, Action? close = null)
        {
            string? eventClose = null;
            if (close != null)
            {
                eventClose = $"LH{close.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventClose, close);
            }
            await layerJsInterop.CloseAll(type, eventClose, objRef);
        }

        public async Task<int> Prompt(LayerOptions? options = null, Action<string, int>? yes = null)
        {
            string? eventYes = null;
            if (yes != null)
            {
                eventYes = $"LH{yes.GetHashCode()}";
                eventHandel.AddEventDuplicate(eventYes, yes);
            }
            int index = await layerJsInterop.Prompt(options, eventYes, objRef);
            return index;
        }

        public async Task Title(string title, int index)
        {
            await layerJsInterop.Title(title, index);
        }

        public async Task Style(int index, object cssStyle)
        {
            await layerJsInterop.Style(index, cssStyle);
        }

        public async Task Full(int index)
        {
            await layerJsInterop.Action("full", index);
        }

        public async Task Min(int index)
        {
            await layerJsInterop.Action("min", index);
        }

        public async Task Restore(int index)
        {
            await layerJsInterop.Action("restore", index);
        }

        public async Task IframeAuto(int index)
        {
            await layerJsInterop.IframeAuto(index);
        }

        public async Task IframeSrc(int index, string url)
        {
            await layerJsInterop.IframeSrc(index, url);
        }

        public void Dispose()
        {
            objRef?.Dispose();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (layerJsInterop != null)
            {
                await layerJsInterop.DisposeAsync();
            }
        }
    }
}
