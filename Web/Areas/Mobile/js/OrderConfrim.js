

/**
* 
* @for 所有的商品统一下单js
*
**
******************************/

function orderConfrim() {
 
    var orders = {
        //商品名字
        sGoodName: "",
        //商品编号(唯一)
        sGoodNo: "",
        //商品图片
        sGoodsPicture: "",          
        //商品规格
        sGoodsInfo: '',              
        //购买数量
        iGoodsCount: 0,
        //商品单价
        dSinglePrices:0,
        //总价
        dTotalPrices: 0,           
        //实际支付价格
        dPrices: 0,
        //商品单价
        dSinglePrices: 0,
        //活动
        sActivity: '',
        //订单备注
        sOrderRemark: '',
        //收货地址
        sReceiver: '',
        //手机号
        sAddress: '',
        //手机号
        sPhone: '',                 
    }

    function checkParams() {
        //反馈给用户的提示
        if (client.string.isEmpty(orders.sReceiver))
            return dialog.tip("亲,请输入收件人姓名!");
        if (client.string.isEmpty(orders.sPhone))
            return dialog.tip("亲,请输入收件人手机号码!");
        if (!client.regex.isPhone(orders.sPhone))
            return dialog.tip("亲,手机号码格式不对!");
        if (client.string.isEmpty(orders.sAddress))
            return dialog.tip("亲,收货地址还没填写呢!");

        //系统验证
        if (orders.iGoodsCount <= 0)
            return dialog.tip("参数错误!");
        if (orders.dTotalPrices <= 0)
            return dialog.tip("参数错误!");
        if (orders.dPrices <= 0)
            return dialog.tip("参数错误!");
        if (client.string.isEmpty(orders.sGoodName))
            return dialog.tip("参数错误!");
        if (client.string.isEmpty(orders.sGoodNo))
            return dialog.tip("参数错误!");
        if (client.string.isEmpty(orders.sGoodsPicture))
            return dialog.tip("参数错误!");
        if (client.string.isEmpty(orders.sGoodsInfo))
            return dialog.tip("参数错误!");

        return true;
    }
    
    //订单提交
    function orderCommit() {
        if (checkParams()) {
            dialog.loading("亲,订单生成中...");
            client.ajax.ajaxRequest(
                "/Mobile/Orders/BookOrder",
                orders,
                function (r) {
                  //window.location.hostname = "mall.ltgirl.com";
                    location.href =r.data;
                });
        }
    }

    return {
        orders: orders,
        orderCommit: orderCommit
    }


}