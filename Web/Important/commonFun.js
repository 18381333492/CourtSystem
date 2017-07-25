


function commonFun() {

    //请求的服务器的链接
    var requestUrl = {
        verifyGoods: '',
        order:'',
        bookOrder:'',
    }


    //验证商品
    function verifyGoods(goodsNo,callback) {
        if (goodsNo) {
            client.ajax.ajaxRequest(requestUrl.verifyGoods, { goodsNo: goodsNo }, function (r) {
                if (callback) {//回调函数
                    callback(r);
                }
            });
        }
        else {
            dialog.tip("goodsNo参数不能为空!");
        }
    }


    //点击购买商品
    function buyGoods(goodsNo,count,activity) {
        if (!goodsNo) return dialog.tip("goodsNo参数不能为空!");
        if (!count) {
            return dialog.tip("购买数量参数不能为空!");
        }
        else {
            if (isNaN(parseInt(count))) return dialog.tip("count数据格式错误!");
            else
                client.localStorage.setStorage("googInfo", {
                    activity: activity,
                    count:count
                });
            location.href = requestUrl.order + "?goodsNo=" + goodsNo;
        }
    }


    return {
        verifyGoods: verifyGoods,
        buyGoods:buyGoods
    }




}