using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public class LayerOptions
    {
        public int? type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? title { get; set; }

        /// <summary>
        /// type为2填链接地址，type为4直接填吸附元素选择器，其他type不填
        /// </summary>
        public string? content { get; set; }

        /// <summary>
        /// 样式类名
        /// </summary>
        public string? skin { get; set; }

        /// <summary>
        /// 宽高
        /// </summary>
        public string? area { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public string? offset { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public int? icon { get; set; }

        /// <summary>
        /// 是否点击遮罩关闭
        /// </summary>
        public bool? shadeClose { get; set; }

        /// <summary>
        /// 自动关闭所需毫秒
        /// </summary>
        public int? time { get; set; }

        /// <summary>
        /// 用于控制弹层唯一标识
        /// </summary>
        public string? id { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public string? btn { get; set; }

        /// <summary>
        /// 按钮排列
        /// </summary>
        public string? btnAlign { get; set; }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        public string? closeBtn { get; set; }

        /// <summary>
        /// 遮罩
        /// </summary>
        public string? shade { get; set; }

        /// <summary>
        /// 关闭动画
        /// </summary>
        public bool? isOutAnim { get; set; }

        /// <summary>
        /// 弹出动画
        /// </summary>
        public int? anim { get; set; }

        /// <summary>
        /// 最大最小化
        /// </summary>
        public bool? maxmin { get; set; }

        /// <summary>
        /// 固定
        /// </summary>
        public bool? @fixed { get; set; }

        /// <summary>
        /// 是否允许拉伸
        /// </summary>
        public bool? resize { get; set; }

        /// <summary>
        /// 是否允许浏览器出现滚动条
        /// </summary>
        public bool? scrollbar { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public int? maxWidth { get; set; }

        /// <summary>
        /// 最大高度
        /// </summary>
        public int? maxHeight { get; set; }

        /// <summary>
        /// 层叠顺序
        /// </summary>
        public int? zIndex { get; set; }

        /// <summary>
        /// 触发拖动的元素
        /// </summary>
        public string? move { get; set; }

        /// <summary>
        /// 是否允许拖拽到窗口外
        /// </summary>
        public bool? moveOut { get; set; }

        /// <summary>
        /// tips方向和颜色
        /// </summary>
        public string? tips { get; set; }

        /// <summary>
        /// 是否允许多个tips
        /// </summary>
        public bool? tipsMore { get; set; }

        /// <summary>
        /// prompt输入框类型，支持0（文本）默认1（密码）2（多行文本）
        /// </summary>
        public int? formType { get; set; }

        /// <summary>
        /// prompt初始时的值，默认空字符
        /// </summary>
        public string? value { get; set; }

        /// <summary>
        /// prompt可输入文本的最大长度，默认500
        /// </summary>
        public int? maxlength { get; set; }
    }
}
