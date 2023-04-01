/// <reference path="EmpleadoCRUD.js" />

$(document).ready(function () { //click
    GetAll();
    CatEntidadFederativaGetAll()
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5271/api/Empleado/GetAll',
        success: function (result) { //200 OK 
            $('#tblEmpleados tbody').empty();
            $.each(result.objects, function (i, empleado) {
                var filas =
                    '<tr>'

                    + '<td class="text-center"> '
                    + '<a href="#" onclick="GetById(' + empleado.idEmpleado + ')">'
                    + '<i class="bi bi-pencil-fill" style="color: #f57809"></i>'
                    + '</a> '
                    + '</td>'
                    + "<td class='text-center'>" + empleado.idEmpleado + "</td>"
                    + "<td class='text-center'>" + empleado.numeroNomina + "</td>"
                    + "<td class='text-center'>" + empleado.nombre + "</td>"
                    + "<td class='text-center'>" + empleado.apellidoPaterno + "</td>"
                    + "<td class='text-center'>" + empleado.apellidoMaterno + "</td>"
                    + "<td class='text-center'>" + empleado.catEntidadFederativa.idEstado + "</td>"
                    + "<td class='text-center'>" + empleado.catEntidadFederativa.estado + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Delete(' + empleado.idEmpleado + ')"><i class="bi bi-eraser-fill" style="color: #FFFFFF"></i></span></button></td>'

                    + "</tr>";
                $("#tblEmpleados tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};


function CatEntidadFederativaGetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5271/api/CatEntidadFederativa/GetAll',
        success: function (result) {
            $("#ddlCatEntidadFederativa").append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, catentidadfederativa) {
                $("#ddlCatEntidadFederativa").append('<option value="'
                    + catentidadfederativa.idEstado + '">'
                    + catentidadfederativa.estado + '</option>');
            });
        }
    });
}
function Modal() {
    $('#myModal').modal('show');
    IniciarEmpleado();
}

function ModalClose() {
    $('#myModal').modal('hide');
}

function IniciarEmpleado() {
    var empleado = {
        IdEmpleado: $('#txtIdEmpleado').val(''),
        NumeroNomina: $('#txtNumeroNomina').val(''),
        Nombre: $('#txtNombre').val(''),
        ApellidoPaterno: $('#txtApellidoPaterno').val(''),
        ApellidoMaterno: $('#txtApellidoMaterno').val(''),
        catEntidadFederativa: {
            idEstado: $('#ddlCatEntidadFederativa').val(0)
        }

    }
}

function Add(empleado) {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5271/api/Empleado/Add',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({
            NumeroNomina: empleado.numeroNomina,
            Nombre: empleado.nombre,
            ApellidoPaterno: empleado.apellidoPaterno,
            ApellidoMaterno: empleado.apellidoMaterno,
            catEntidadFederativa: {
                idEstado: empleado.catEntidadFederativa.idEstado
            }
        }),
        success: function (result) {
            alert('Registro exitoso');
            $('#ModalUpdate').modal('show');
            $('#myModal').modal('hide');
            GetAll();
        },
        error: function (result) {
            alert('Error al agregar el registro');
        }
    });
};

function Validar() {
    var empleado = {
        idEmpleado: $('#txtIdEmpleado').val(),
        numeroNomina: $('#txtNumeroNomina').val(),
        nombre: $('#txtNombre').val(),
        apellidoPaterno: $('#txtApellidoPaterno').val(),
        apellidoMaterno: $('#txtApellidoMaterno').val(),
        catEntidadFederativa: {
            idEstado: $('#ddlCatEntidadFederativa').val()
        }
    }

    if (empleado.idEmpleado == '') {
        Add(empleado);
    }
    else {
        Update(empleado);
    }
}


function GetById(IdEmpleado) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5271/api/Empleado/GetById/' + IdEmpleado,
        success: function (result) {
            $('#txtIdEmpleado').val(result.object.idEmpleado);
            $('#txtNumeroNomina').val(result.object.numeroNomina);
            $('#txtNombre').val(result.object.nombre);
            $('#txtApellidoPaterno').val(result.object.apellidoPaterno);
            $('#txtApellidoMaterno').val(result.object.apellidoMaterno);
            $('#ddlCatEntidadFederativa').val(result.object.catEntidadFederativa.idEstado);
            $('#myModal').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}

function Update(empleado) {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5271/api/Empleado/Update',
        dataType: 'json',
        data: JSON.stringify(empleado),
        contentType: 'application/json; charset-utf-8',
        success: function (result) {
            $('#ModalUpdate').modal('show');
            $('#myModal').modal('hide');
            GetAll();
        },
        error: function (result) {
            alert('Error al actualizar al empleado' + result.responseJSON.ErrorMessage);
        }
    });
};



function Delete(idEmpleado) {
    if (confirm("¿Esta seguro que desea eliminar el registro?")) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:5271/api/Empleado/Delete/' + idEmpleado,
            success: function (result) {
                $('#myModal').modal('show');
                GetAll();
            },
            error: function (result) {
                alert('Error al intentar eliminar el registro' + result.responseJSON.ErrorMessage);
            }
        });
    }
};


