/*
* JavaScript库 V1.0
* 版权所有：深圳百新谷网络科技有限公司 www.singoo.com.cn 
* Auther: vincent
*/

(function () {
    window['SingGooJS'] = {}                      // 注册命名空间

    /*
    功能: 获取对象
    id:   对象名称
    */
    function $O(id) {
        return document.getElementById(id);
    }
    window['SingGooJS']['$O'] = $O;               // 注册方法到命名空间下

    /*
    功能: 获取对象value值
    id:   对象名称
    */
    function $V(id) {
        if (document.getElementById(id) == null)
            return "";
        else
            return document.getElementById(id).value;
    }
    window['SingGooJS']['$V'] = $V;

    /*
    功能: 获取地址栏参数
    name: 参数名
    return: 返回参数值
    */
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);

        return "";
    }
    window['SingGooJS']['GetQueryString'] = GetQueryString;

    /*
    功能: 获取select中的当前选中值
    selectName: select名称     
    */
    function GetSelectValue(selectName) {
        var objSelect = document.getElementById(selectName);
        return objSelect.value;
    }
    window['SingGooJS']['GetSelectValue'] = GetSelectValue;

    /*
    功能：添加select选项
    selectName: select名称 
    */
    function AddSelectOption(selectName, text, value) {
        var objSelect = document.getElementById(selectName);
        objSelect.options.add(new Option(text, value));
    }
    window['SingGooJS']['AddSelectOption'] = AddSelectOption;

    /*
    功能: 获取select中的当前选中text值
    selectName: select名称     
    */
    function GetSelectText(selectName) {
        var objSelect = document.getElementById(selectName);
        return objSelect.options[objSelect.selectedIndex].text;
    }
    window['SingGooJS']['GetSelectText'] = GetSelectText;

    /*
    功能: 添加一条数据到listbox中
    control: 控件对象( var control = document.getElementById('name'); )
    objValue: 要添加的值
    */
    function ListBoxAdd(control, objValue, objText) {
        var node = document.createElement("OPTION");
        control.options.add(node);
        node.text = objText;
        node.value = objValue;

        control.appendChild(node);
    }
    window['SingGooJS']['ListBoxAdd'] = ListBoxAdd;

    /*
    功能: 检索listbox中是否有该数据
    controlName: 控件名称
    objValue: 要检索的值
    返回 true 为存在，false 为不存在
    */
    function ListBoxSearch(controlName, objValue) {
        var obj = document.getElementById(controlName);
        var length = obj.length;

        for (var i = 0; i < length; i++) {
            var value = obj.options[i].value;
            var text = obj.options[i].text;

            if (text == objValue) return true;
        }

        return false;
    }
    window['SingGooJS']['ListBoxSearch'] = ListBoxSearch;

    /*
    功能: 将listboxA中的单条数据转移到listboxB中
    lsbSource: listboxA
    ltbDestination: listboxB    
    */
    function ListBoxAddOne(lsbSource, ltbDestination) {
        var lst1 = window.document.getElementById(lsbSource);
        var lstindex = lst1.selectedIndex;
        if (lstindex < 0)
            return;
        var v = lst1.options[lstindex].value;
        var t = lst1.options[lstindex].text;
        var lst2 = window.document.getElementById(ltbDestination);
        var length = lst2.options.length;
        lst2.options[length] = new Option(t, v, true, true);
        lst1.options[lstindex].parentNode.removeChild(lst1.options[lstindex]);
    }
    window['SingGooJS']['ListBoxAddOne'] = ListBoxAddOne;

    /*
    功能: 将listboxA中的所有数据转移到listboxB中
    lsbSource: listboxA
    ltbDestination: listboxB    
    */
    function ListBoxAddAll(lsbSource, ltbDestination) {
        var lst1 = window.document.getElementById(lsbSource);
        var length = lst1.options.length;
        if (length <= 0)
            return;
        var opNum = 0;
        var lst2 = window.document.getElementById(ltbDestination);
        for (var i = length; i > 0; i--) {
            var v = lst1.options[i - 1].value;
            var t = lst1.options[i - 1].text;
            lst2.options[opNum] = new Option(t, v, true, true);
            lst1.options[i - 1].parentNode.removeChild(lst1.options[i - 1]);
            opNum++;
        }
    }
    window['SingGooJS']['ListBoxAddAll'] = ListBoxAddAll;

    /*
    功能: 将listName中的数据上移
    listName: 控件名称
    */
    function ListBoxUp(listName) {
        var obj = document.getElementById(listName);

        for (var i = 1; i < obj.length; i++) {
            //最上面的一个不需要移动，所以直接从i=1开始  .　　　　　　　　
            if (obj.options[i].selected) {
                if (!obj.options.item(i - 1).selected) {
                    var selText = obj.options[i].text;
                    var selValue = obj.options[i].value;
                    obj.options[i].text = obj.options[i - 1].text;
                    obj.options[i].value = obj.options[i - 1].value;
                    obj.options[i].selected = false;
                    obj.options[i - 1].text = selText;
                    obj.options[i - 1].value = selValue;
                    obj.options[i - 1].selected = true;
                }
            }
        }
    }
    window['SingGooJS']['ListBoxUp'] = ListBoxUp;

    /*
    功能: 将listName中的数据下移
    listName: 控件名称
    */
    function ListBoxDown(listName) {
        var obj = document.getElementById(listName);

        for (var i = obj.length - 2; i >= 0; i--) {
            //向下移动，最后一个不需要处理，所以直接从倒数第二个开始  
            if (obj.options[i].selected) {
                if (!obj.options[i + 1].selected) {
                    var selText = obj.options[i].text;
                    var selValue = obj.options[i].value;
                    obj.options[i].text = obj.options[i + 1].text;
                    obj.options[i].value = obj.options[i + 1].value;
                    obj.options[i].selected = false;
                    obj.options[i + 1].text = selText;
                    obj.options[i + 1].value = selValue;
                    obj.options[i + 1].selected = true;
                }
            }
        }
    }
    window['SingGooJS']['ListBoxDown'] = ListBoxDown;

    /*
    功能: 获取文件名带有扩展名 
    obj: file元素对象
    返回文件名如(01.jpg)
    */
    function getFileName(obj) {
        if (obj.value == "") return "";

        obj.value.replace("\\", "\\\\");
        var pos = obj.value.lastIndexOf("\\") * 1;
        return obj.value.substring(pos + 1);
    }
    window['SingGooJS']['getFileName'] = getFileName;

    /*
    功能: 获取文件扩展名 
    obj: file元素对象
    返回文件扩展名如(jpg)
    */
    function getFileExt(obj) {
        var index = obj.value.lastIndexOf(".");
        var end = obj.value.length;

        return obj.value.substring(index, end);
    }
    window['SingGooJS']['getFileExt'] = getFileExt;

    /*
    功能: 获取url地址扩展名 
    obj: file元素对象
    返回文件扩展名如(jpg)
    */
    function getUrlExt(url) {
        var index = url.lastIndexOf(".");
        var end = url.length;

        return url.substring(index + 1, end);
    }
    window['SingGooJS']['getUrlExt'] = getUrlExt;

    /*
    功能: 匹配文件扩展名 
    strRegex: 用于验证图片扩展名的正则表达式(如: "(.jpg|.JPG|.gif|.GIF)$")
    fileName: 要匹配的文件名
    返回 true 为格式匹配，false 为格式不匹配
    */
    function checkFileType(strRegex, fileName) {
        var reg = new RegExp(strRegex);

        if (reg.test(fileName)) {
            return true;
        } else {
            return false;
        }
    }
    window['SingGooJS']['checkFileType'] = checkFileType;

    /*
    功能: 向Select里添加Option
    obj: select对象
    sName: option的text值
    sValue: option的value值
    */
    function selectAddOption(obj, sName, sValue) {
        var oOption = document.createElement("option");
        oOption.appendChild(document.createTextNode(sName));

        if (arguments.length == 3) {
            oOption.setAttribute("value", sValue);
        }

        obj.appendChild(oOption);
    }
    window['SingGooJS']['selectAddOption'] = selectAddOption;


    /*
    功能: 操作控件中的内容
    type: 0 减数据 1 加数据
    controlName: 控件名称
    objValue: 要操作的值
    */
    function addOrCutData(type, controlName, objValue) {
        if (type == 0) {
            var str = document.getElementById(controlName).value;
            str = str.replace(objValue + "|", "")
            document.getElementById(controlName).value = str;
        }
        else if (type == 1) {
            document.getElementById(controlName).value = document.getElementById(controlName).value + objValue + "|";
        }
    }
    window['SingGooJS']['addOrCutData'] = addOrCutData;

    /*
    功能: 操作控件中的内容
    type: 0 减数据 1 加数据
    controlName: 控件名称
    objValue: 要操作的值
    schar: 用什么符号分隔 (如 , | $ ...)
    */
    function addOrCutDataByChar(type, controlName, objValue, schar) {
        if (type == 0) {
            var str = document.getElementById(controlName).value;
            str = str.replace(objValue + schar, "")
            document.getElementById(controlName).value = str;
        }
        else if (type == 1) {
            document.getElementById(controlName).value = document.getElementById(controlName).value + objValue + schar;
        }
    }
    window['SingGooJS']['addOrCutDataByChar'] = addOrCutDataByChar;

    /*
    功能: 数据列表进行分页
    currentpage: 当前页
    pageall: 总页数
    fclick: 查询数据的方法名
    */
    function showPage(currentpage, pageall, fclick) {
        currentpage = parseInt(currentpage);
        pageall = parseInt(pageall);
        var begin, end;
        if (currentpage - 3 < 1) {
            begin = 1;
            end = (7 > pageall ? pageall : 7);
        }
        else {
            if (currentpage + 3 < pageall) {
                begin = currentpage - 3;
                end = currentpage + 3;
            }
            else {
                begin = (pageall - 7 > 1 ? pageall - 7 : 1);
                end = pageall;
            }
        }

        if (fclick == null) fclick = "showList";

        var pagehtml = '';
        if (currentpage > 1) pagehtml = '<a href="javascript:void(0)" style="cursor:pointer;" onclick="' + fclick + '(' + (currentpage - 1) + ');">上一页</a>';
        if (begin > 1) pagehtml += ' <a  href="javascript:void(0)" onclick="' + fclick + '(1);">' + 1 + '</a> ';
        if (begin > 2) pagehtml += '...';
        for (var i = begin; i <= end; i++) {
            if (i == currentpage) pagehtml += ' <b><font color=\'red\'>' + i + '</font></b> ';
            else pagehtml += ' <a href="javascript:void(0)" onclick="' + fclick + '(' + i + ');">' + i + '</a> ';
        }
        if (pageall - end > 2) pagehtml += '...';
        if (pageall - end > 0) pagehtml += ' <a  href="javascript:void(0)" onclick="' + fclick + '(' + pageall + ');">' + pageall + '</a> ';
        if (currentpage < pageall) pagehtml += '<a href="javascript:void(0)" style="cursor:pointer;" onclick="' + fclick + '(' + (currentpage + 1) + ');" >下一页</a>';
        if (currentpage < pageall) pagehtml += "&nbsp;<input id=\"iGo\" name=\"iGo\" type=\"text\" style=\"width:20px;\"/><a href=\"javascript:SingGooJS.goToPage('" + fclick + "');\">GO</a>";
        //pagehtml += "&nbsp;总数:<strong><font color='red'>" + total + "</font></strong>";

        return pagehtml;
    }
    window['SingGooJS']['showPage'] = showPage;

    /*
    功能: 数据列表进行分页
    currentpage: 当前页
    pageall: 总页数
    fclick: 查询数据的方法名
    */
    function showPage2(currentpage, pageall, fclick) {
        currentpage = parseInt(currentpage);
        pageall = parseInt(pageall);
        var begin, end;
        if (currentpage - 3 < 1) {
            begin = 1;
            end = (7 > pageall ? pageall : 7);
        }
        else {
            if (currentpage + 3 < pageall) {
                begin = currentpage - 3;
                end = currentpage + 3;
            }
            else {
                begin = (pageall - 7 > 1 ? pageall - 7 : 1);
                end = pageall;
            }
        }

        if (fclick == null) fclick = "showList";

        var pagehtml = '';
        if (currentpage > 1) pagehtml = '<a style="cursor: pointer;" onclick="' + fclick + '(' + (currentpage - 1) + ');">上一页</a>';
        if (begin > 1) pagehtml += ' <a style="cursor: pointer;"  onclick="' + fclick + '(1);">' + 1 + '</a> ';
        if (begin > 2) pagehtml += '...';
        for (var i = begin; i <= end; i++) {
            if (i == currentpage) pagehtml += '<a><b><font color=\'red\'>' + i + '</font></b></a> ';
            else pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + i + ');">' + i + '</a> ';
        }
        if (pageall - end > 2) pagehtml += '...';
        if (pageall - end > 0) pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + pageall + ');">' + pageall + '</a> ';
        if (currentpage < pageall) pagehtml += '<a href="javascript:void(0)" style="cursor:pointer;" onclick="' + fclick + '(' + (currentpage + 1) + ');" >下一页</a>';

        if (currentpage < pageall) pagehtml += "&nbsp;<a><input id=\"iGo\" name=\"iGo\" type=\"text\" style=\"width:20px;\"/></a><a href=\"javascript:SingGooJS.goToPage('" + fclick + "');\">GO</a>";
        //pagehtml += "&nbsp;总数:<strong><font color='red'>" + total + "</font></strong>";

        return pagehtml;
    }
    window['SingGooJS']['showPage2'] = showPage2;


    function showPageIndex(currentpage, pageall, fclick) {
        currentpage = parseInt(currentpage);
        pageall = parseInt(pageall);
        var begin, end;
        if (currentpage - 3 < 1) {
            begin = 1;
            end = (7 > pageall ? pageall : 7);
        }
        else {
            if (currentpage + 3 < pageall) {
                begin = currentpage - 3;
                end = currentpage + 3;
            }
            else {
                begin = (pageall - 7 > 1 ? pageall - 7 : 1);
                end = pageall;
            }
        }

        if (fclick == null) fclick = "Index.prototype.showList";

        var pagehtml = '';
        if (currentpage > 1) pagehtml = '<a style="cursor: pointer;" onclick="' + fclick + '(' + (currentpage - 1) + ');">上一页</a>';
        if (begin > 1) pagehtml += ' <a style="cursor: pointer;"  onclick="' + fclick + '(1);">' + 1 + '</a> ';
        if (begin > 2) pagehtml += '...';
        for (var i = begin; i <= end; i++) {
            if (i == currentpage) pagehtml += '<a><b><font color=\'red\'>' + i + '</font></b></a> ';
            else pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + i + ');">' + i + '</a> ';
        }
        if (pageall - end > 2) pagehtml += '...';
        if (pageall - end > 0) pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + pageall + ');">' + pageall + '</a> ';
        if (currentpage < pageall) pagehtml += '<a href="javascript:void(0)" style="cursor:pointer;" onclick="' + fclick + '(' + (currentpage + 1) + ');" >下一页</a>';

        if (currentpage < pageall) pagehtml += "&nbsp;<a><input id=\"iGo\" name=\"iGo\" type=\"text\" style=\"width:20px;\"/></a><a href=\"javascript:SingGooJS.goToPage('" + fclick + "');\">GO</a>";
        //pagehtml += "&nbsp;总数:<strong><font color='red'>" + total + "</font></strong>";

        return pagehtml;
    }
    window['SingGooJS']['showPageIndex'] = showPageIndex;

    /*
    功能: 数据列表进行分页
    currentpage: 当前页
    pageall: 总页数
    fclick: 查询数据的方法名
    */
    function showPage3(currentpage, pageall, fclick, total) {
        currentpage = parseInt(currentpage);
        pageall = parseInt(pageall);
        var begin, end;
        if (currentpage - 3 < 1) {
            begin = 1;
            end = (7 > pageall ? pageall : 7);
        }
        else {
            if (currentpage + 3 < pageall) {
                begin = currentpage - 3;
                end = currentpage + 3;
            }
            else {
                begin = (pageall - 7 > 1 ? pageall - 7 : 1);
                end = pageall;
            }
        }

        if (fclick == null) fclick = "showList";

        var pagehtml = '';
        if (currentpage > 1) pagehtml = '<a style="cursor: pointer;" onclick="' + fclick + '(' + (currentpage - 1) + ');">上一页</a>';
        if (begin > 1) pagehtml += ' <a style="cursor: pointer;"  onclick="' + fclick + '(1);">' + 1 + '</a> ';
        if (begin > 2) pagehtml += '...';
        for (var i = begin; i <= end; i++) {
            if (i == currentpage) pagehtml += '<a><b>' + i + '</b></a> ';
            else pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + i + ');">' + i + '</a> ';
        }
        if (pageall - end > 2) pagehtml += '...';
        if (pageall - end > 0) pagehtml += ' <a  style="cursor: pointer;" onclick="' + fclick + '(' + pageall + ');">' + pageall + '</a> ';
        if (currentpage < pageall) pagehtml += '<a style="cursor: pointer;" onclick="' + fclick + '(' + (currentpage + 1) + ');">下一页</a>';
        if (currentpage < pageall) pagehtml += "&nbsp;<a><input id=\"iGo\" name=\"iGo\" type=\"text\" style=\"width:20px;\"/></a><a href=\"javascript:SingGooJS.goToPage('" + fclick + "');\">GO</a>";
        pagehtml += "&nbsp;总数:<strong><font color='red'>" + total + "</font></strong>";

        return pagehtml;
    }
    window['SingGooJS']['showPage3'] = showPage3;
    /*
    功能:分页跳转  
    fclick: 查询数据的方法名  
    */
    function goToPage(fclick) {
        var currentpage = document.getElementById("iGo").value;

        currentpage = parseInt(currentpage);

        if (currentpage <= 0)
            currentpage == 1;

        var str = fclick + "(currentpage);";

        eval(str);
    }
    window['SingGooJS']['goToPage'] = goToPage;

    /*
    功能: 将特殊字符转换为 字符串
    str: 需要转换的字符
    return: 返回转换后的字符串
    */
    function codeToStr(str) {
        var s = "";
        if (str.length == 0) return "";
        for (var i = 0; i < str.length; i++) {
            switch (str.substr(i, 1)) {
                case "<":
                    s += "&lt;";
                    break;
                case ">":
                    s += "&gt;";
                    break;
                case "&":
                    s += "&amp;";
                    break;
                case "   ":
                    s += "&nbsp;";
                    break;
                case "\"":
                    s += "&quot;";
                    break;
                case "\n":
                    s += "<br>";
                    break;
                default:
                    s += str.substr(i, 1);
                    break;
            }
        }
        return s;
    }
    window['SingGooJS']['codeToStr'] = codeToStr;

    /*
    功能: 将字符串转换为 特殊字符 
    str: 需要转换的字符
    return: 返回转换后的字符串
    */
    function strToCode(str) {
        var s = "";
        if (str.length == 0) return "";

        s = str.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&amp;/g, "&");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&quot;/g, "\"");
        s = s.replace(/&<br>;/g, "\n");
        s = s.replace(/\/\/\//g, "\r\n");

        return s;
    }
    window['SingGooJS']['strToCode'] = strToCode;

    /*
    功能: 获取本地文件大小(适用于IE6)
    fileName: 本地文件路径,即file控件中的value值
    return: 返回文件大小
    */
    function getFileSizeByIE6() {
        var file = new Image(); //把附件当做图片处理放在缓冲区预加载   
        file.dynsrc = document.getElementById("fudPolicy").value; //设置附件的url    
        var filesize = file.fileSize; //获取上传的文件的大小

        return filesize;
    }
    window['SingGooJS']['getFileSizeByIE6'] = getFileSizeByIE6;

    /*
    功能: 获取本地文件大小(适用于IE7)
    fileName: 本地文件路径,即file控件中的value值
    return: 返回文件大小
    */
    function getFileSizeByIE7(fileName) {
        if (document.layers) {
            if (navigator.javaEnabled()) {
                var file = new java.io.File(fileName);
                if (location.protocol.toLowerCase() != 'file:') netscape.security.PrivilegeManager.enablePrivilege('UniversalFileRead');
                return file.length();
            }
            else return -1;
        }
        else if (document.all) {
            window.oldOnError = window.onerror;
            window.onerror = function (err) {
                if (err.indexOf('utomation') != -1) {
                    alert('file access not possible'); // 文件无法访问
                    return true;
                }
                else return false;
            };
            var fso = new ActiveXObject('Scripting.FileSystemObject');
            var file = fso.GetFile(fileName);
            window.onerror = window.oldOnError;
            return file.Size;
        }
    }
    window['SingGooJS']['getFileSizeByIE7'] = getFileSizeByIE7;

    /*
    功能: 获取本地图片分辩率(只适用于IE7)
    fileName: 本地文件路径,即file控件中的value值
    return: 返回文件大小
    */
    function checkImageDimensions(fileName) {
        var imgURL = 'file:///' + fileName;

        var img = new Image();
        img.OnInit = loadHandler;
        if (document.layers && location.protocol.toLowerCase() != 'file:' && navigator.javaEnabled()) netscape.security.PrivilegeManager.enablePrivilege('UniversalFileRead');
        img.src = imgURL;

        alert(this.width + 'x' + this.height);
    }
    window['SingGooJS']['checkImageDimensions'] = checkImageDimensions;

    /*
    功能：等分一个字符串
    str：要拆分的字符串
    num：等分为几段
    */
    function splitAverage(str, num) {
        var length = str.length;   // 总长度
        var unit = length / num;     // 单位长度

        var arrayStr = new Array(num);

        for (var i = 0; i < num; i++) {
            var text = str.substring(i * unit, (i + 1) * unit);

            arrayStr[i] = text;
        }

        return arrayStr;
    }
    window['SingGooJS']['splitAverage'] = splitAverage;

    /*
    功能：拆分一个字符串,多少个字符为一段
    str：要拆分的字符串
    len：取多长   
    */
    function substringStr(str, len) {
        var length = str.length;   // 总长度
        var num = parseInt(length / len); // 可分为几段        
        num = (length % len == 0) ? num : (num + 1);

        var arrayStr = new Array(num);

        for (var i = 0; i < num; i++) {
            var text = str.substring(i * len, (i + 1) * len);

            arrayStr[i] = text;
        }

        return arrayStr;
    }
    window['SingGooJS']['substringStr'] = substringStr;

    /*
    功能：字符串添加换行符
    str：要自动换行的字符串
    len：单位长度，多长开始换行   
    */
    function strBr(str, unitlen) {
        var returnStr = "";
        var length = str.length;        // 总长度
        if (length <= unitlen)
            return str;

        var num = length / unitlen;    // 可以分多少段

        for (var i = 0; i < num; i++) {
            var text = str.substring(i * unitlen, (i + 1) * unitlen);

            returnStr += text + "<br \>";
        }

        return returnStr;
    }
    window['SingGooJS']['strBr'] = strBr;


    // Function Name: isValidPositiveInteger
    // Function Description: 
    /*
    功能：判断输入是否是一个正整数
    str：要判断的字符串
    */
    function isValidPositiveInteger(str) {
        var result = str.match(/^\d+$/);
        if (result == null) return false;
        if (parseInt(str) >= 0) return true;
        return false;
    }
    window['SingGooJS']['isValidPositiveInteger'] = isValidPositiveInteger;

    /*
    功能：判断输入是否是一个float型
    str：要判断的字符串
    */
    function isValidPositiveFloat(str) {
        if (isValidPositiveInteger(str))
            return true;
        var result = str.match(/^[1-9]d*.d*|0.d*[1-9]d*$/);
        if (result == null) return false;
        if (result > 0) return true;
        return false;
    }
    window['SingGooJS']['isValidPositiveFloat'] = isValidPositiveFloat;

    /*
    功能：检查输入框内是否为数字型数据(包括小数)
    str：要判断的字符串
    */
    function checkNum(str) {
        //var result = str.match(/^\d+\.{0,1}\d+$/);    
        //var result = str.match(/^(?!0)\d+(\.\d{1,4})?$/);  0.9不行 >1的实数    
        var result = str.match(/^(0\.[1-9]\d*|[1-9]\d*(\.\d+)?)$/);

        if (result == null) return false;
        if (parseInt(str) >= 0) return true;
        return false;
    }
    window['SingGooJS']['checkNum'] = checkNum;

    /*
    功能：检查输入框内是否为合格的日期
    str：要判断的字符串
    */
    function checkDate(obj) {
        var sValue = obj.value;

        if (sValue != "") {
            var patrn = /\d{4}-\d{2}-\d{2}/;
            if (!patrn.exec(sValue)) {
                obj.value = "";
                obj.focus();
                alert("非法输入,输入格式应该为2001-01-02！");
                return false;
            }
            else
                return true;
        }
    }
    window['SingGooJS']['checkDate'] = checkDate;

    /*
    功能：最大化浏览器窗口所需参数
    str：返回字符串
    */
    function openParams() {
        var str = "left=0,screenX=0,top=0,screenY=0,resizable=yes,scrollbars=yes"; //fullscreen=yes  只对IE有效！ 

        if (window.screen) {
            var ah = screen.availHeight - 30;
            var aw = screen.availWidth - 10;
            str += ",height=" + ah;
            str += ",innerHeight=" + ah;
            str += ",width=" + aw;
            str += ",innerWidth=" + aw;
        } else {
            str += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
        }

        return str;
    }
    window['SingGooJS']['openParams'] = openParams;

    //去右边的空格
    function RTrim(str) {
        var whitespace = new String(" \t\n\r");
        var s = new String(str);

        if (whitespace.indexOf(s.charAt(s.length - 1)) != -1) {
            var i = s.length - 1;
            while (i >= 0 && whitespace.indexOf(s.charAt(i)) != -1) {
                i--;
            }
            s = s.substring(0, i + 1);
        }
        return s;
    }
    window['SingGooJS']['RTrim'] = RTrim;

    //去左边的空格
    function LTrim(str) {
        var whitespace = new String(" \t\n\r");
        var s = new String(str);

        if (whitespace.indexOf(s.charAt(0)) != -1) {
            var j = 0, i = s.length;
            while (j < i && whitespace.indexOf(s.charAt(j)) != -1) {
                j++;
            }

            s = s.substring(j, i);
        }
        return s;
    }
    window['SingGooJS']['LTrim'] = LTrim;

    //去前后空格
    function Trim(str) {
        return RTrim(LTrim(str));
    }
    window['SingGooJS']['Trim'] = Trim;


    /*
    功能：选中某行，背景色变化，不支持多选
    index: 定义的checkbox序号
    objvalue:选中行的ID号(数据库ID)
    注意:第一列必须定义为<input type='checkbox' name='ckbSelect' id='ckbSelect_" + i + "' onclick=\"SingGooJS.ckbChecked(" + i + ")\" />
    */
    function ckbChecked(index, objvalue) {
        $("tr").removeClass("selected");
        $("#tr_" + index).addClass("selected");

        var obj = $("input[name='ckbSelect']");
        for (i = 0; i < obj.length; i++) {
            if (i != index) {
                obj[i].checked = false;
            }
            else {
                obj[i].checked = true;
                document.getElementById("hID").value = objvalue; // 添加ID到控件中
            }
        }
    }
    window['SingGooJS']['ckbChecked'] = ckbChecked;

    /*
    功能：加载Xml文件到相应的控件中
    xmlUrl：xml文件的地址
    nodeName：要查找的节点的名称
    obj: select对象
    */
    function getXml(xmlUrl, nodeName, obj) {
        $.ajax({
            url: xmlUrl,
            dataType: 'xml',
            type: 'GET',
            timeout: 2000,
            error: function (xml) {
                //alert("加载XML 文件出错！");
            },
            success: function (xml) {
                $(xml).find(nodeName).each(function (i) {
                    var id = $(this).attr("ID");      // 取属性值 
                    var name = $(this).attr("ProvinceName");
                    //var lower = $(this).children("ID").text(); //取子节点值

                    selectAddOption(obj, name, id);

                });
            }
        });
    }
    window['SingGooJS']['getXml'] = getXml;

    /*************** 验证 *****************/
    /*
    功能：各类验证（邮箱、手机、区号、电话...）
    strValue：需要验证的字符串
    typeId：1邮箱 2手机 3区号 4电话...
    return：验证通过返回true 反之false
    */
    function regexStr(strValue, typeId) {
        if (typeId <= 0)
            return false;

        var myReg = "";
        switch (typeId) {
            case 1:
                myReg = /^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\.(?:com|cn)$/;
                break;
            case 2:
                myReg = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
                break;
            case 3:
                myReg = /^\d{3,4}$/;
                break;
            case 4:
                myReg = /^\d{7,8}$/;
                break;
            case 5:
                myReg = /^[\w~!@#$%^&*()_+{}:"<>?\-=[\];\',.\/]{6,20}$/;
                break;
        }

        if (!myReg.test(strValue))
            return false;
        else
            return true;
    }
    window['SingGooJS']['regexStr'] = regexStr;


    /*********** 常量数据有关 ***********/

    /*根据省份ID，查询出该省份的名称
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetProviceNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Provinces.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Provinces>Province").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性    
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetProviceNameById'] = GetProviceNameById;

    /*根据城市ID，查询出该城市的名称
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetCityNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Cities.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Cities>City").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性  
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetCityNameById'] = GetCityNameById;

    /*根据区域ID，查询出该区域的名称
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetDistrictNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Districts.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Districts>District").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性  
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetDistrictNameById'] = GetDistrictNameById;

    /* 根据学历ID，查询名称
    id：要查询的ID
    return：返回名称
    */
    function GetDegreeNameById(id) {
        var strName = "";
        if (id == 0)
            strName = "不限";
        else if (id == 1)
            strName = "中专或相当学历";
        else if (id == 2)
            strName = "大专";
        else if (id == 3)
            strName = "本科";
        else if (id == 4)
            strName = "双学士";
        else if (id == 5)
            strName = "硕士";
        else if (id == 6)
            strName = "博士";
        else if (id == 76)
            strName = "博士后";

        return strName;
    }
    window['SingGooJS']['GetDegreeNameById'] = GetDegreeNameById;

    /*根据收入ID，查询出该收入的名称
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetIncomeNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Incomes.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Incomes>Income").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetIncomeNameById'] = GetIncomeNameById;

    /*民族
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetNationNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Nations.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Nations>Nation").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetNationNameById'] = GetNationNameById;

    /*职业
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetIndustryNameById(id, objname) {
        var strName = "未填";
        if (id == null) {
            $("#" + objname).text(strName);
            return;
        }

        $.ajax({ url: "Xml/Industrys.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Industrys>Industry").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetIndustryNameById'] = GetIndustryNameById;

    /*居住情况
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetHouseNameById(id, objname) {
        var strName = "";
        $.ajax({ url: "Xml/Houses.xml", dataType: "xml", success: function (xml) {
            $(xml).find("Houses>House").each(function () {
                var pro = $(this);
                var pId = pro.attr("ID");
                if (pId == id) {
                    strName = pro.text(); //读取节点属性
                    $("#" + objname).text(strName);
                }
            });
        }
        });
    }
    window['SingGooJS']['GetHouseNameById'] = GetHouseNameById;

    /*获取xml节点内容的通用方法,且将值添加到select中
    path：xml文件路径
    nodename：读取的节点 如：Nations>Nation
    attrId：xml文件中的属性ID
    id：要查询的ID
    return：返回名称
    */
    function GetNameById_Xml_InSelect(path, nodename, attrId, id, objname) {
        if (id == null || id == "" || id == 0) {
            SingGooJS.AddSelectOption(objname, "未填", id);
            return;
        }

        $.ajax({ url: path, dataType: "xml", success: function (xml) {
            var result = "";
            $(xml).find(nodename).each(function () {
                var getId = $(this).attr(attrId);
                var txt = $(this).text();
                var tag = $(this)[0].tagName;

                if (tag == "checkbox") {
                    var arrayId = id.split(",");
                    $.each(arrayId, function (arr_key, arr_val) {
                        if (arr_val == getId) {
                            result += txt + "、";
                        }
                    });
                } else {
                    if (getId == id) {
                        result = txt;
                    }
                }
            });

            SingGooJS.AddSelectOption(objname, result, id);
        }
        });
    }
    window['SingGooJS']['GetNameById_Xml_InSelect'] = GetNameById_Xml_InSelect;

    /*获取xml节点内容的通用方法
    path：xml文件路径
    nodename：读取的节点 如：Nations>Nation
    attrId：xml文件中的属性ID
    id：要查询的ID
    objname: 要填充到的元素ID
    return：返回名称
    */
    function GetNameById_Xml(path, nodename, attrId, id, objname) {
        if (id == null || id == "" || id == 0) {
            $("#" + objname).text("未填");
            return;
        }

        $.ajax({ url: path, dataType: "xml", success: function (xml) {
            var result = "";
            $(xml).find(nodename).each(function () {
                var getId = $(this).attr(attrId);
                var txt = $(this).text();
                var tag = $(this)[0].tagName;

                if (tag == "checkbox") {
                    var arrayId = id.split(",");
                    $.each(arrayId, function (arr_key, arr_val) {
                        if (arr_val == getId) {
                            result += txt + "、";
                        }
                    });
                } else {
                    if (getId == id) {
                        result = txt;
                    }
                }
            });

            $("#" + objname).text(result.replace(/、$/gi, "")); //读取节点属性         
        }
        });
    }
    window['SingGooJS']['GetNameById_Xml'] = GetNameById_Xml;
    /*********************************/

})();




