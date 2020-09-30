//Execute on runtime 
$(document).ready(function () {
    let theForm = $("#theForm");
    theForm.hide();

    let buy = $("#buyButton");
    buy.on("click", function () {
        alert("Buy");
    });

    let productInfo = $("product-props li");
    productInfo.on("click", function () {

    });

    let $loginToggle = $("#loginToggle");
    let $popupForm = $(".popup-form");

    $loginToggle.on("click", function() {
        $popupForm.fadeToggle(1000);
    });
});
//let listItem = productInfo.item[0].children;

