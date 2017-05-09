$(function(){
	// 详情数量减少
	$('.details_con .minus,.cart_count .minus').click(function(){
		var _index=$(this).parent().parent().index()-1;
		var _count=$(this).parent().find('.count');
		var _val=_count.val();
		if(_val>1){
			_count.val(_val-1);
			$('.details_con .selected span').eq(_index).text(_val-1);
			
		}
		if(_val<=2){
			$(this).addClass('disabled');
		}
		
	});

	// 详情数量添加
	$('.details_con .add,.cart_count .add').click(function(){
		var _index=$(this).parent().parent().index()-1;
		var _count=$(this).parent().find('.count');
		var _val=_count.val();
		$(this).parent().find('.minus').removeClass('disabled');
		_count.val(_val-0+1);
		$('.details_con .selected span').eq(_index).text(_val-0+1);
		
	});

    //商品详情和品论的切换
	$('.hd_fav ul li').on("click", function () {
	    $(this).addClass("on").siblings().remove("on");
	    var value = parseInt($(this).attr("data-value"));
	    if (value == 0) {
	        $('.prop-area').show();
	        $('.appraise').hide();
	    }
	    else {
	        $('.prop-area').hide();
	        $('.appraise').show();
	    }
	});

    //参数
	var defaults = {
	    page: 1,//页面
	    rows:8,//每页的数量
	    state: true,//是否请求的标识
	    sGoodNo: $('#sGoodNo').val(),//商品的编号
	};
	loadData();
    //加载商品评论数据
	function loadData() {
	    client.ajax.ajaxRequest("/Mobile/GoodsComment/GetList", defaults, function (res) {
	        $('#CommentCount').text('('+res.total+')');
	        if (res.rows.length > 0) {
	            var html = [];
	            $(res.rows).each(function () {
	                html.push('<li class="">');
	                html.push('<div style="height:50px">');
	                html.push('<img src="../../Areas/Mobile/Content/Shoes/img/theme.png" style="height:50px;width:50px;display:block;margin-left:16px;display:inline-block" />');
	                html.push('<span style="text-align:left;margin:auto;margin-left:5px;top:3px;position:relative;color:orangered">' + this.sCommentPerson.substring(0,3) +'****'+this.sCommentPerson.substr(7)+ '</span>');
	                html.push('<div style="display:inline-block;margin-left:10px;top:1px;position:relative">');
	                for (var i = 1; i <= this.iScore; i++) {
	                    html.push('<img src="../../Scripts/plug-in/starsScore/images/star-on.png" />');
	                }
	                html.push('</div> ');
	                html.push('</div>');
	                html.push('<p style="padding-left:20px;font-size: 12px">' + this.dCommentTime.substring(0, this.dCommentTime.length-3) + '</p>');
	                html.push('<p style="padding:0 20px;">' + this.sCommentContent + '</p>');
	                html.push('</li>');
	            });
	            $(html.join('')).insertBefore('.last');
	            defaults.state = res.total <= defaults.rows * defaults.page ? false : true;
	            if (defaults.state) {
	                $('.last').show();
	                $('.last a').unbind("click", loadMore);
	                $('.last a').on("click",loadMore);
	            }
	            else
	                $('.last').hide();
	        }
	    },null,true);
	}

    //加载更多数据
	function loadMore() {
	    defaults.page = defaults.page + 1;
	    loadData();
	}
});

