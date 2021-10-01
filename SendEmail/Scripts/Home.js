$("#Bt_Enviar").click(function () {

    var arquivo = new FormData();
    arquivo.append($("#resultado")[0].files[0].name, $("#resultado")[0].files[0])
    SendFile(arquivo);
});


function SendFile(arquivo) {
    $.ajax({
        url: '/Home/Reader',
        data: arquivo,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            if (data.success) {
                alert("Os emails foram enviados a seus destinatários")
            }
            else {
                alert("Falha no Upload")
            }
        }
    });
}

$("Btn_EnviarManual").click(function () {

    var Nome = $("Input_Nome").text();
    var Email = $("#Input_Email").text();

    var data2 = {
        Nome,
    }
    var data3 = {
        Email,
    }

    function Sender(data2, data) {
        $.ajax({
            url: '/Home/Sender',

            type: 'POST',
            success: function (data) {
                if (data.success) {
                    alert("Os emails foram enviados a seus destinatários")
                }
                else {
                    alert("Falha no Upload")
                }
            }
        });
    }
});

// action click in button
$("#Btn_EnviarManual").click(function () {
    //var Resultado = document.getElementById("input_nome");
    var Nome = $("#input_nome").val();
    //var Resultado = document.getElementById("input_email");
    var Email = $("#input_email").val();

    // object parameter
    //var data1 = {
    //    Resultado,
    //};
    //var data2 = {
    //    Resultado2,
    //}

    // request ajax
    Sender(Nome, Email){

        success: function (data) {
            if (data.success) {
                alert("Os emails foram enviados a seus destinatários");
            }
            else {
                alert("Falha no Upload");
            }
        });
        
    }   
    //$.ajax({
    //    url: '/Home/Sender',
    //    data: arquivo,
    //    processData: false,
    //    contentType: false,
    //    type: 'POST',
    //    success: function (data) {
    //        if (data.success) {
    //            alert("Os emails foram enviados a seus destinatários")
    //        }
    //        else {
    //            alert("Falha no Upload")
    //        }
    //    }
    //});
        
//});

// request
function Sender(Nome, Email) {
    post(url, parameter)
    return jQuery.post("/Home/Sender", Nome, Email);
}

<<<<<<< HEAD


=======
>>>>>>> ee3c9f3762f47eb5d0c861f969d5df2e6ee58019
