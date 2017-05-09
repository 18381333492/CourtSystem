/**
 * Created by Lenovo on 2016/5/19.
 */
$(function(){
    /*全选*/
    $("#all").on("click",function(){
        if ($("#all").prop("checked")) {
            $(".subCheck").prop("checked", true);
        } else {
            $(".subCheck").prop("checked", false);
        }
    });
    /*复选*/
    $(".subCheck").on("click",function(){
        if($(this).prop("checked")==false){
            $("#all").prop("checked",false)
        }
        var num=$(".subCheck").length;
        var subNum = $(".subCheck:checked").length;
        if(subNum==num){
            $("#all").prop("checked",true)
        }
    })
});