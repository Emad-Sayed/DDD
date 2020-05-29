 // repeating details
 var b = document.getElementById("hour");
 var c = document.getElementById("day");
 var d = document.getElementById("week");
 var e = document.getElementById("month");
 var f = document.getElementById("year");
 function oneTime() {
     b.style.display = "none";
     c.style.display = "none";
     d.style.display = "none";
     e.style.display = "none";
     f.style.display = "none";
 }
 function perHour() {
     b.style.display = "block"
     c.style.display = "none"
     d.style.display = "none"
     e.style.display = "none"
     f.style.display = "none"
 }
 function perDay() {
     b.style.display = "none"
     c.style.display = "block"
     d.style.display = "none"
     e.style.display = "none"
     f.style.display = "none"
 }
 function perWeek() {
     b.style.display = "none"
     c.style.display = "none"
     d.style.display = "block"
     e.style.display = "none"
     f.style.display = "none"
 }
 function perMonth() {
     b.style.display = "none"
     c.style.display = "none"
     d.style.display = "none"
     e.style.display = "block"
     f.style.display = "none"
 }
 function perYear() {
     b.style.display = "none"
     c.style.display = "none"
     d.style.display = "none"
     e.style.display = "none"
     f.style.display = "block"
 }

 // to active and deactive inputs
 var a = document.getElementById("activity");
 function enable() {
     a.disabled = false;
 }
 function disable() {
     a.disabled = true;
 }
 function enabl1Disable2() {
     document.getElementById("row1Input1").disabled = false;
     document.getElementById("row1Input2").disabled = false;
     document.getElementById("row2Input1").disabled = true;
     document.getElementById("row2Input2").disabled = true;
     document.getElementById("row2Input3").disabled = true;
 }
 function enabl2Disable1() {
     document.getElementById("row1Input1").disabled = true;
     document.getElementById("row1Input2").disabled = true;
     document.getElementById("row2Input1").disabled = false;
     document.getElementById("row2Input2").disabled = false;
     document.getElementById("row2Input3").disabled = false;
 }
 function yearEnable1Disable2() {
     document.getElementById("yearInput1").disabled = false;
     document.getElementById("yearInput2").disabled = false;
     document.getElementById("yearInput3").disabled = true;
     document.getElementById("yearInput4").disabled = true;
     document.getElementById("yearInput5").disabled = true;
 }
 function yearEnable2Disable1() {
     document.getElementById("yearInput1").disabled = true;
     document.getElementById("yearInput2").disabled = true;
     document.getElementById("yearInput3").disabled = false;
     document.getElementById("yearInput4").disabled = false;
     document.getElementById("yearInput5").disabled = false;
 }


 $('.card-add').click(function (event) {
     var show = $('.hide').removeClass('hide');
     event.stopPropagation();

 });

 $(".card-remove").click(function(event2){
    $(this).parents('.card').remove();
    event2.stopPropagation();

  });
