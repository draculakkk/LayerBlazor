# 一款基于Layer.js的blazor组件

## 简要说明：

### 1：nuget 搜索 LayerBlazor安装类库
### 2：Program注册Services.AddLayer();
### 3：到layui和jquery官网下载最新的layer和jquery的JS库
wwwroot/index.html(WebAssembly)或_Host.cshtml(Blazor Server)添加如下行（这里的引用路径填自己项目的JS及样式路径）
```html
<link href="script/layer/theme/default/layer.css" rel="stylesheet" /> <br/>
<script src="script/jquery-{version}.min.js"></script> <br/>
<script src="script/layer/layer.js"></script>
```
### 4：_Imports.razor 添加 @using LayerBlazor

## Layer标签自带的属性说明，其余属性可参考layer官网文档：

|属性|说明|
|:---|:---|
|ClassName|类名|
|debugModel|启用后浏览器控台会输出当前layer弹窗的options项|
|CloseAsync|关闭弹窗方法|

### 示例1：标准用法，捕获Layer的引用示例，调用OpenAsync弹窗
```razor
<Layer @ref="dlgtplt" title="['文本', 'font-size:18px;']" debugMode="true" skin="layui-layer-rim" shadeClose="true" shade="0.6"
       area="['750px', '50%']" btn="['确定', '取消']"
       OnYes="@(async (index)=>
                {
                    System.Console.WriteLine($"onyes:{index}");
                    await dlgtplt.CloseAsync();
                })"
       OnCancel="@(async (index)=>
                {
                    System.Console.WriteLine($"oncancel:{index}");
                    await dlgtplt.CloseAsync();
                })"
       OnMoveEnd="@(()=>
                     {
                         System.Console.WriteLine($"moveEnd");
                     })">
    <div style="background-color: red;">
        <h1>数字1+数字2结果：@result</h1>
        数字1:<input @bind="num1" />
        数字1:<input @bind="num2" />
        <Button @onclick="@(e => { result = $"{int.Parse(num1) + int.Parse(num2)}"; })">计算</Button>
    </div>
</Layer>

<button @onclick="TestOpen">测试弹窗</button>

@code{
  private Layer dlgtplt;

  private async Task TestOpen()
  {
      result = "待计算";
      await dlgtplt.OpenAsync();
  }
}
```

### 示例2：支持layer全局方法，需要注入LayerHelper，方法和layer官网文档同步（tab、photos、config、ready、getChildFrame、getFrameIndex暂时还不支持）
```razor
@inject LayerHelper layerHelper

<button @onclick="@(async ()=>
                    {
                        int i = await layerHelper.Alert("我是弹窗", new LayerOptions() { icon = 1 });
                    })">
    测试Alter
</button>

<button @onclick="@(async ()=>
                    {
                        await layerHelper.Confirm("我是确认窗", new LayerOptions() { icon = 1 }, async (index)=>
                        {
                            Console.WriteLine($"Confirm:{index}");
                            await layerHelper.Close(index);
                        });
                    })">
    测试Confirm
</button>

<button @onclick="@(async ()=>
                    {
                        await layerHelper.Msg("我是MSG", new LayerOptions() { icon = 6 });
                    })">
    测试Msg
</button>

<button @onclick="@(async ()=>
                    {
                        await layerHelper.Load(2, new LayerOptions() { time = 5 * 1000 });
                    })">
    测试Load
</button>

<button @onclick="@(async ()=>
                    {
                        await layerHelper.Tips("我是Tips", "#tip", new LayerOptions() { tips = "1" });
                    })">
    测试Tips
</button>

<button @onclick="@(async ()=>
                    {
                        await layerHelper.Prompt(new LayerOptions() { formType = 0 }, async (value, index)=> 
                        {
                            Console.WriteLine($"value:{value},index:{index}");
                            await layerHelper.Close(index);
                        });
                    })">
    测试Prompt
</button>

```
