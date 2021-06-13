
export function open(option, content, dotnetHelper) {

    if (!option.hasOwnProperty("type") || option.type == 0 || option.type == 3) {
        option.content = content.innerHTML;
    }
    else if (option.type == 1) {
        content.display = "inline";
        option.content = $(content);
    }
    else if (option.type == 4) {
        option.content = [content.innerHTML, option.content]
    }
    else {

    }

    for (let p in option) {
        if (option[p] == null || option[p] == "")
            continue;

        if (p === "yes" || p === "success" || p === "cancel" ||
            p === "min" || p === "full" || p === "restore" || (p !== "btn" && p !== "btnAlign" && p.indexOf("btn") != -1)) {
            option[p] = function (index, layero) {
                dotnetHelper.invokeMethodAsync('CallEvent', p, index.toString());
                if (p === "cancel" || p.indexOf("btn") != -1) {
                    return false;
                }
            }
        }
        else if (p === "end") {
            option[p] = function () {
                dotnetHelper.invokeMethodAsync('CallEvent', p, "");
            }
        }
        else if (p === "resizing" || p === "moveEnd") {
            option[p] = function (layero) {
                //$(layero).html()
                dotnetHelper.invokeMethodAsync('CallEvent', p, "");
            }
        }
        else if (p === "title" || p === "btn" || p === "area" || p === "offset" || p === "closeBtn" || p === "shade"
            || p === "move" || p === "tips") {
            if (p === "shade" || p === "tips") {
                var tmp = Number.parseFloat(option[p]);
                if (!Number.isNaN(tmp)) {
                    option[p] = tmp;
                }
                else {
                    option[p] = formatOption(option[p]);
                }
            }
            else {
                option[p] = formatOption(option[p]);
            }
        }
    }

    if (option.hasOwnProperty("debugMode")) {
        let debugMode = option.debugMode;
        delete option["debugMode"];
        if (debugMode) {
            console.log(option);
        }
    }

    let index = layer.open(option);

    return index;
}

export function alert(content, options, eventYes, dotnetHelper) {

    let lcoption = formatOption2(options);

    if (eventYes != null && eventYes != "") {
        let index = layer.alert(content, lcoption, function (index) {
            dotnetHelper.invokeMethodAsync('CallEvent', eventYes, index.toString());
        });
        return index;
    }
    else {
        let index = layer.alert(content, lcoption);
        return index;
    }
}

export function confirm(content, options, eventYes, eventCancel, dotnetHelper) {

    let lcoption = formatOption2(options);

    let yes = null;
    if (eventYes != null && eventYes != "") {
        yes = (index) => {
            dotnetHelper.invokeMethodAsync('CallEvent', eventYes, index.toString());
        }
    }

    let cancel = null;
    if (eventCancel != null && eventCancel != "") {
        cancel = (index) => {
            dotnetHelper.invokeMethodAsync('CallEvent', eventCancel, index.toString());
        }
    }

    let index = layer.confirm(content, lcoption, yes, cancel);
    return index;
}

export function msg(content, options, eventEnd, dotnetHelper) {
    let lcoption = formatOption2(options);
    let end = null;
    if (eventEnd != null && eventEnd != "") {
        end = () => {
            dotnetHelper.invokeMethodAsync('CallEvent', eventEnd, "");
        }
    }
    let index = layer.msg(content, lcoption, end);
    return index;
}

export function tips(content, follow, options) {
    let lcoption = formatOption2(options);
    return layer.tips(content, follow, lcoption);
}

export function load(icon, options) {
    let lcoption = formatOption2(options);
    return layer.load(icon, lcoption);
}

export function close(index, eventClose, dotnetHelper) {

    let close = null;
    if (eventClose != null && eventClose != "") {
        close = () => {
            dotnetHelper.invokeMethodAsync('CallEvent', eventClose, "");
        }
    }

    layer.close(index);
}

export function closeAll(type, eventClose, dotnetHelper) {

    let close = null;
    if (eventClose != null && eventClose != "") {
        close = () => {
            dotnetHelper.invokeMethodAsync('CallEvent', eventClose, "");
        }
    }

    layer.closeAll(type, close);
}

export function prompt(options, eventYes, dotnetHelper) {
    let lcoption = formatOption2(options);
    let yes = null;
    if (eventYes != null && eventYes != "") {
        yes = (value, index, elem) => {
            let pm = { value: value, index: index };
            dotnetHelper.invokeMethodAsync('CallEvent', eventYes, JSON.stringify(pm));
        }
    }
    let index = layer.prompt(lcoption, yes);
    return index;
}

export function title(title, index) {
    layer.title(title, index);
}

export function style(index, cssStyle) {
    layer.style(index, cssStyle);
}

export function action(type, index) {
    if (type == "full") {
        layer.full(index);
    }
    else if (type == "min") {
        layer.min(index);
    }
    else if (type == "restore") {
        layer.restore(index);
    }
}

export function iframeAuto(index) {
    layer.iframeAuto(index);
}

export function iframeSrc(index, url) {
    layer.iframeSrc(index, url);
}

function formatOption(str) {
    if (str != null && str != "") {
        if (str.indexOf("[") != -1 || str == "true" || str == "false") {
            return eval('(' + str + ')');
        }
        else {
            return str;
        }
    }
    else {
        return str;
    }
}

function formatOption2(options) {
    let lcoption = {};

    for (let p in options) {
        if (options[p] == null || options[p] == "")
            continue;

        if (p === "title" || p === "btn" || p === "area" || p === "offset" || p === "closeBtn" || p === "shade"
            || p === "move" || p === "tips") {
            if (p === "shade" || p === "tips") {
                var tmp = Number.parseFloat(options[p]);
                if (!Number.isNaN(tmp)) {
                    lcoption[p] = tmp;
                }
                else {
                    lcoption[p] = formatOption(options[p]);
                }
            }
            else {
                lcoption[p] = formatOption(options[p]);
            }
        }
        else {
            lcoption[p] = options[p];
        }
    }

    return lcoption;
}
