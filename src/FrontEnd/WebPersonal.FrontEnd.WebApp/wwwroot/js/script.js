window.MostrarAlerta = function(message) {
    alert(message);
}


window.LlamarCSharp = function () {
    var result = window.DotNet.invokeMethod("WebPersonal.FrontEnd.WebApp",
        "GetNombreCompleto", "NetMentor");
    alert(result); 
}