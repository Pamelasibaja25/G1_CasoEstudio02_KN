$(function () {

    $("#IdCasa").change(function () {
        let idCasa = $("#IdCasa").val();
        $("#PrecioCasa").val("");

        if (idCasa !== "") {
            $.ajax({
                url: "/Casas/ObtenerPrecio",
                type: "GET",
                dataType: "json",
                data: { id: idCasa },
                success: function (result) {
                    if (result && result.Precio !== undefined && result.Precio !== null) {
                        let valor = parseFloat(result.Precio);
                        if (!isNaN(valor)) {
                            $("#PrecioCasa").val(valor.toFixed(2));
                        } else {
                            $("#PrecioCasa").val(result.Precio);
                        }
                    }
                }
            });
        }
    });

    $("#FormAlquiler").validate({
        rules: {
            IdCasa: {
                required: true
            },
            UsuarioAlquiler: {
                required: true
            }
        },
        messages: {
            IdCasa: {
                required: "* Debe seleccionar una casa."
            },
            UsuarioAlquiler: {
                required: "* Debe ingresar el usuario de alquiler."
            }
        }
    });

});
