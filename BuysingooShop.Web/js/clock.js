// JavaScript Document
function displayTime(){
        var elt = document.getElementById("clock");
		var elts = document.getElementsByClassName("clock_dex");
        if(leftTime<0){
            elt.innerHTML = "Over";
			elts.innerHTML = "Over";
        }
        else{
            //结束时间，需自定义 懒人建站 http://www.51xuediannao.com 
            var endTime = new Date("2015/01/20 18:45:13");
            var now = new Date();
            var leftTime = endTime.getTime() -now.getTime();
            var ms = parseInt(leftTime%1000).toString();
            leftTime = parseInt(leftTime/1000);
			var t = Math.floor(leftTime / 86400);
            var o = Math.floor(leftTime / 3600%24);
            var d = Math.floor(o/24);
            var m = Math.floor(leftTime/60%60);
            var s = leftTime%60;
            elt.innerHTML = t+ "days" + o + ":" + m + ":" + s ;
			elts.innerHTML = t+ "days" + o + ":" + m + ":" + s ;
            setTimeout(displayTime,100);
        }
    }
    displayTime();
	
	
	
function fresh() {
var time1 = document.getElementById("timeD").value;
for (var i = 1; i <= 5; i++) {
var endtime = new Date(time1);
var nowtime = new Date();
var leftsecond = parseInt((endtime.getTime() - nowtime.getTime()) / 1000);
d = parseInt(leftsecond / 3600 / 24);
h = parseInt((leftsecond / 3600) % 24);
m = parseInt((leftsecond / 60) % 60);
s = parseInt(leftsecond % 60);
// document.getElementById("times").innerHTML=__h+"小时"+__m+"分"+__s+"秒";
document.getElementById("times"+i.toString()).innerHTML = h + "小时" + m + "分" + s + "秒";
if (leftsecond <= 0) {
A
}
}
}
fresh()
var sh;
sh = setInterval(fresh, 1000);

