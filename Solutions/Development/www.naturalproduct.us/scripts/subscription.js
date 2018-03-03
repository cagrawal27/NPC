// JavaScript File
function setFormValues(rbInstance)
{
    //alert(rbInstance.value);
    var str = rbInstance.value;
    var arr = str.split(",");
    
    //for (var i=0; i<arr.length;i++)
    //    alert(arr[i]);
    
    var item_name = document.getElementById("item_name");
    var amount = document.getElementById("amount");
    item_name.value = arr[0];
    amount.value = arr[1];      
}

function validateForm(frm)
{
    var sel = false;
    
    // Loop from zero to the one minus the number of radio button selections
    // If a radio button has been selected it will return true
    // (If not it will return false)
    for (j = 0; j < frm.rbRates.length; j++)
        if (frm.rbRates[j].checked)
            sel = true; 

    // If there were no selections made display an alert box 
    if (!sel)
    {
        alert("Please select a subscription.")
        return false;
    }
    
    return true; 
}    

