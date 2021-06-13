using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public partial class Layer : IDisposable, IAsyncDisposable
    {
        [Inject]
        protected IJSRuntime? jsRuntime { get; set; }

        private IDictionary<string, object> option = new Dictionary<string, object>();

        private ElementReference? ercontent;

        private EventHandel? eventHandel;

        private DotNetObjectReference<EventHandel>? objRef;

        private LayerJsInterop? layerJsInterop;

        protected override void OnInitialized()
        {
            eventHandel = new EventHandel();
            objRef = DotNetObjectReference.Create(eventHandel);
            layerJsInterop = new LayerJsInterop(jsRuntime!);
        }

        public string DomId { get; } = Guid.NewGuid().ToString();

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? ClassName { get; set; }

        [Parameter]
        public bool? debugMode { get; set; }

        /// <summary>
        /// layer提供了5种层类型。可传入的值有：0（信息框）1（页面层，默认）2（iframe层）3（加载层）4（tips层）
        /// </summary>
        [Parameter]
        public int? type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Parameter]
        public string? title { get; set; }

        /// <summary>
        /// type为2填链接地址，type为4直接填吸附元素选择器，其他type不填
        /// </summary>
        [Parameter]
        public string? content { get; set; }

        /// <summary>
        /// 样式类名
        /// </summary>
        [Parameter]
        public string? skin { get; set; }

        /// <summary>
        /// 宽高
        /// </summary>
        [Parameter]
        public string? area { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        [Parameter]
        public string? offset { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Parameter]
        public int? icon { get; set; }

        /// <summary>
        /// 是否点击遮罩关闭
        /// </summary>
        [Parameter]
        public bool? shadeClose { get; set; }

        /// <summary>
        /// 自动关闭所需毫秒
        /// </summary>
        [Parameter]
        public int? time { get; set; }

        /// <summary>
        /// 用于控制弹层唯一标识
        /// </summary>
        [Parameter]
        public string? id { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        [Parameter]
        public string? btn { get; set; }

        /// <summary>
        /// 按钮排列
        /// </summary>
        [Parameter]
        public string? btnAlign { get; set; }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        [Parameter]
        public string? closeBtn { get; set; }

        /// <summary>
        /// 遮罩
        /// </summary>
        [Parameter]
        public string? shade { get; set; }

        /// <summary>
        /// 关闭动画
        /// </summary>
        [Parameter]
        public bool? isOutAnim { get; set; }

        /// <summary>
        /// 弹出动画
        /// </summary>
        [Parameter]
        public int? anim { get; set; }

        /// <summary>
        /// 最大最小化
        /// </summary>
        [Parameter]
        public bool? maxmin { get; set; }

        /// <summary>
        /// 固定
        /// </summary>
        [Parameter]
        public bool? @fixed { get; set; }

        /// <summary>
        /// 是否允许拉伸
        /// </summary>
        [Parameter]
        public bool? resize { get; set; }

        /// <summary>
        /// 是否允许浏览器出现滚动条
        /// </summary>
        [Parameter]
        public bool? scrollbar { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        [Parameter]
        public int? maxWidth { get; set; }

        /// <summary>
        /// 最大高度
        /// </summary>
        [Parameter]
        public int? maxHeight { get; set; }

        /// <summary>
        /// 层叠顺序
        /// </summary>
        [Parameter]
        public int? zIndex { get; set; }

        /// <summary>
        /// 触发拖动的元素
        /// </summary>
        [Parameter]
        public string? move { get; set; }

        /// <summary>
        /// 是否允许拖拽到窗口外
        /// </summary>
        [Parameter]
        public bool? moveOut { get; set; }

        /// <summary>
        /// tips方向和颜色
        /// </summary>
        [Parameter]
        public string? tips { get; set; }

        /// <summary>
        /// 是否允许多个tips
        /// </summary>
        [Parameter]
        public bool? tipsMore { get; set; }

        /// <summary>
        /// 右上角关闭按钮触发的回调
        /// </summary>
        [Parameter]
        public Action<int>? OnCancel { get; set; }

        /// <summary>
        /// 监听窗口拉伸动作
        /// </summary>
        [Parameter]
        public Action<int>? OnYes { get; set; }

        /// <summary>
        /// 右上角关闭按钮触发的回调
        /// </summary>
        [Parameter]
        public Action? OnResizing { get; set; }

        /// <summary>
        /// 拖动完毕后的回调方法
        /// </summary>
        [Parameter]
        public Action? OnMoveEnd { get; set; }

        /// <summary>
        /// 层弹出后的成功回调方法
        /// </summary>
        [Parameter]
        public Action<int>? OnSuccess { get; set; }

        /// <summary>
        /// 层销毁后触发的回调
        /// </summary>
        [Parameter]
        public Action? OnEnd { get; set; }

        /// <summary>
        /// 最大化回调
        /// </summary>
        [Parameter]
        public Action<int>? OnFull { get; set; }

        /// <summary>
        /// 最小化回调
        /// </summary>
        [Parameter]
        public Action<int>? OnMin { get; set; }

        /// <summary>
        /// 最大化/最小化还原后回调
        /// </summary>
        [Parameter]
        public Action<int>? OnRestore { get; set; }

        [Parameter]
        public Action<int>? OnBtn2 { get; set; }

        [Parameter]
        public Action<int>? OnBtn3 { get; set; }

        [Parameter]
        public Action<int>? OnBtn4 { get; set; }

        [Parameter]
        public Action<int>? OnBtn5 { get; set; }

        [Parameter]
        public Action<int>? OnBtn6 { get; set; }

        [Parameter]
        public Action<int>? OnBtn7 { get; set; }

        [Parameter]
        public Action<int>? OnBtn8 { get; set; }

        [Parameter]
        public Action<int>? OnBtn9 { get; set; }

        public int Index { get; private set; }

        public async Task<int> OpenAsync()
        {
            if (layerJsInterop == null || ercontent == null || objRef == null)
                return 0;

            if (debugMode.HasValue)
                FillSetting("debugMode", debugMode.Value);

            if (type.HasValue)
                FillSetting("type", type.Value);
            else
                FillSetting("type", 1);

            if (!string.IsNullOrEmpty(title))
                FillSetting("title", title);

            if (!string.IsNullOrEmpty(content))
                FillSetting("content", content);

            if (!string.IsNullOrEmpty(id))
                FillSetting("id", id);

            if (!string.IsNullOrEmpty(skin))
                FillSetting("skin", skin);

            if (!string.IsNullOrEmpty(area))
                FillSetting("area", area);

            if (!string.IsNullOrEmpty(offset))
                FillSetting("offset", offset);

            if (!string.IsNullOrEmpty(btn))
                FillSetting("btn", btn);

            if (!string.IsNullOrEmpty(btnAlign))
                FillSetting("btnAlign", btnAlign);

            if (!string.IsNullOrEmpty(closeBtn))
                FillSetting("closeBtn", closeBtn);

            if (!string.IsNullOrEmpty(shade))
                FillSetting("shade", shade);

            if (time.HasValue)
                FillSetting("time", time.Value);

            if (shadeClose.HasValue)
                FillSetting("shadeClose", shadeClose.Value);

            if (icon.HasValue)
                FillSetting("icon", icon.Value);

            if (isOutAnim.HasValue)
                FillSetting("isOutAnim", isOutAnim.Value);

            if (anim.HasValue)
                FillSetting("anim", anim.Value);

            if (maxmin.HasValue)
                FillSetting("maxmin", maxmin.Value);

            if (@fixed.HasValue)
                FillSetting("fixed", @fixed.Value);

            if (resize.HasValue)
                FillSetting("resize", resize.Value);

            if (scrollbar.HasValue)
                FillSetting("scrollbar", scrollbar.Value);

            if (maxHeight.HasValue)
                FillSetting("maxHeight", maxHeight.Value);

            if (maxWidth.HasValue)
                FillSetting("maxWidth", maxWidth.Value);

            if (zIndex.HasValue)
                FillSetting("zIndex", zIndex.Value);

            if (!string.IsNullOrEmpty(move))
                FillSetting("move", move);

            if (moveOut.HasValue)
                FillSetting("moveOut", moveOut.Value);

            if (!string.IsNullOrEmpty(tips))
                FillSetting("tips", tips);

            if (tipsMore.HasValue)
                FillSetting("tipsMore", tipsMore.Value);

            if (eventHandel != null)
            {
                if (OnYes != null && eventHandel.AddEvent("yes", OnYes))
                    FillSetting("yes", "yes");

                if (OnCancel != null && eventHandel.AddEvent("cancel", OnCancel))
                    FillSetting("cancel", "cancel");

                if (OnResizing != null && eventHandel.AddEvent("resizing", OnResizing))
                    FillSetting("resizing", "resizing");

                if (OnMoveEnd != null && eventHandel.AddEvent("moveEnd", OnMoveEnd))
                    FillSetting("moveEnd", "moveEnd");

                if (OnSuccess != null && eventHandel.AddEvent("success", OnSuccess))
                    FillSetting("success", "success");

                if (OnEnd != null && eventHandel.AddEvent("end", OnEnd))
                    FillSetting("end", "end");

                if (OnFull != null && eventHandel.AddEvent("full", OnFull))
                    FillSetting("full", "full");

                if (OnMin != null && eventHandel.AddEvent("min", OnMin))
                    FillSetting("min", "min");

                if (OnBtn2 != null && eventHandel.AddEvent("btn2", OnBtn2))
                    FillSetting("btn2", "btn2");

                if (OnBtn3 != null && eventHandel.AddEvent("btn3", OnBtn3))
                    FillSetting("btn3", "btn3");

                if (OnBtn4 != null && eventHandel.AddEvent("btn4", OnBtn4))
                    FillSetting("btn4", "btn4");

                if (OnBtn5 != null && eventHandel.AddEvent("btn5", OnBtn5))
                    FillSetting("btn5", "btn5");

                if (OnBtn6 != null && eventHandel.AddEvent("btn6", OnBtn6))
                    FillSetting("btn6", "btn6");

                if (OnBtn7 != null && eventHandel.AddEvent("btn7", OnBtn7))
                    FillSetting("btn7", "btn7");

                if (OnBtn8 != null && eventHandel.AddEvent("btn8", OnBtn8))
                    FillSetting("btn8", "btn8");

                if (OnBtn9 != null && eventHandel.AddEvent("btn9", OnBtn9))
                    FillSetting("btn9", "btn9");
            }

            Index = await layerJsInterop.Open(option, ercontent, objRef);
            return Index;
        }

        public async Task CloseAsync()
        {
            if (eventHandel != null)
            {
                eventHandel.ClearEvent();
                await jsRuntime!.InvokeVoidAsync("layer.close", Index);
            }
        }

        public void FillSetting<T>(string parameter, T value)
        {
            if (option == null || value == null)
                return;

            if (option.ContainsKey(parameter))
            {
                option[parameter] = value;
            }
            else
            {
                option.Add(parameter, value);
            }
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
