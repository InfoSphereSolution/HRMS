    function Check_Click(objRef) {
         //Get the Row based on checkbox
         var row = objRef.parentNode.parentNode;
         if (objRef.checked) {
             //If checked change color to Gainsboro
             row.style.backgroundColor = "D5D8DC";
         }
         else {
             //If not checked change back to original color
             if (row.rowIndex % 2 == 0) {
                 //Alternating Row Color
                 row.style.backgroundColor = "White";
             }
             else {
                 row.style.backgroundColor = "white";
             }
         }

         //Get the reference of GridView
         var GridView = row.parentNode;

         //Get all input elements in Gridview
         var inputList = GridView.getElementsByTagName("input");

         for (var i = 0; i < inputList.length; i++) {
             //The First element is the Header Checkbox
             var headerCheckBox = inputList[0];

             //Based on all or none checkboxes
             //are checked check/uncheck Header Checkbox
             var checked = true;
             if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                 if (!inputList[i].checked) {
                     checked = false;
                     break;
                 }
             }
         }
         headerCheckBox.checked = checked;

     }
 
     function checkAll(objRef) {
         var GridView = objRef.parentNode.parentNode.parentNode;
         var inputList = GridView.getElementsByTagName("input");
         for (var i = 0; i < inputList.length; i++) {
             //Get the Cell To find out ColumnIndex
             var row = inputList[i].parentNode.parentNode;
             if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                 if (objRef.checked) {
                     //If the header checkbox is checked
                     //check all checkboxes
                     //and highlight all rows
                     row.style.backgroundColor = "#D5D8DC";
                     inputList[i].checked = true;
                 }
                 else {
                     //If the header checkbox is checked
                     //uncheck all checkboxes
                     //and change rowcolor back to original
                     if (row.rowIndex % 2 == 0) {
                         //Alternating Row Color
                         row.style.backgroundColor = "White";
                     }
                     else {
                         row.style.backgroundColor = "white";
                     }
                     inputList[i].checked = false;
                 }
             }
         }
     }
 
     function MouseEvents(objRef, evt) {
         var checkbox = objRef.getElementsByTagName("input")[0];
         if (evt.type == "mouseover") {
             objRef.style.backgroundColor = "#ABB2B9";
         }
         else {
             if (checkbox.checked) {
                 objRef.style.backgroundColor = "#D5D8DC";
             }
             else if (evt.type == "mouseout") {
                 if (objRef.rowIndex % 2 == 0) {
                     //Alternating Row Color
                     objRef.style.backgroundColor = "White";
                 }
                 else {
                     objRef.style.backgroundColor = "white";
                 }
             }
         }
     }
