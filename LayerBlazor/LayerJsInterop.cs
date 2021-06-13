using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public class LayerJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public LayerJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/LayerBlazor/common.js").AsTask());
        }

        public async ValueTask<int> Open(IDictionary<string, object> option, ElementReference? content, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("open", option, content, objRef);
        }

        public async ValueTask<int> Alert(string content, LayerOptions? options, string? eventYes, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("alert", content, options, eventYes, objRef);
        }

        public async ValueTask<int> Confirm(string content, LayerOptions? options, string? eventYes, string? eventCancel, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("confirm", content, options, eventYes, eventCancel, objRef);
        }

        public async ValueTask<int> Msg(string content, LayerOptions? options, string? eventEnd, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("msg", content, options, eventEnd, objRef);
        }

        public async ValueTask<int> Load(int? icon, LayerOptions? options)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("load", icon, options);
        }

        public async ValueTask<int> Tips(string content, string follow, LayerOptions? options)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("tips", content, follow, options);
        }

        public async ValueTask Close(int index, string? eventColse, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("close", index, eventColse, objRef);
        }

        public async ValueTask CloseAll(string? type, string? eventColse, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("closeAll", type, eventColse, objRef);
        }

        public async ValueTask<int> Prompt(LayerOptions? options, string? eventYes, DotNetObjectReference<EventHandel>? objRef)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("prompt", options, eventYes, objRef);
        }

        public async ValueTask Title(string title, int index)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("title", title, index);
        }

        public async ValueTask Style(int index, object cssStyle)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("style", index, cssStyle);
        }

        public async ValueTask Action(string type, int index)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("action", type, index);
        }

        public async ValueTask IframeAuto(int index)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("iframeAuto", index);
        }

        public async ValueTask IframeSrc(int index, string url)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("iframeSrc", index, url);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
