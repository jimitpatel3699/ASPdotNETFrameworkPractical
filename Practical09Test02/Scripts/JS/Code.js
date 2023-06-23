let LblValue = document.getElementById("lbltxt");
function BtnClick(id)
{
    LblValue.innerHTML = "Hello! World";
    if (id == "BoldText")
    {
        LblValue.style.fontWeight = "700";
    }
    else if (id == "ItalicText")
    {
        LblValue.style.fontStyle = "italic";
    } 
    else if (id == "UnderlineText")
    {
        LblValue.style.textDecoration = "underline";
    }
    else if (id =="ResetText")
    {
        LblValue.style.fontWeight = "normal";   
        LblValue.style.fontStyle = "normal";
        LblValue.style.textDecoration = "none";
    }
}