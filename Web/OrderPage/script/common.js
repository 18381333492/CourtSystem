
var webFont = (function () {
    //判断设备显示字体
    if (judge.platform() == "ios") {
        var str = "<style> body{ font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif!important;}</style>";
        $('head').append(str);
    }
    if (judge.platform() == "android") {
        var str = "<style>body{ font-family: 'RobotoRegular', 'Droid Sans', sans-serif!important;}</style>";
        $('head').append(str);
    }
})();
