$(document).ready(function () {
    SelecionarUsuarios();
    $('#txtCPF').mask('000.000.000-00');
})
function LimparCampos() {
    $('#txtNome').val('');
    $('#txtEmail').val('');
    $('#txtCPF').val('');
    $('#txtSenha').val('');
}

function CadastrarUsuario() {
    var Nome = $('#txtNome').val();
    var Email = $('#txtEmail').val();
    var CPF = $('#txtCPF').val();
    var Senha = $('#txtSenha').val();

    if (Nome == "" || Email == "" || CPF == "" || Senha == "") {
        modalMessage('Aviso!!', 'Preencha todos os campos!!');
    }
    else {
        var jDados = { Nome: Nome, Email: Email, CPF: CPF, Senha: Senha };
        $.ajax({
            url: "Default.aspx/CadastrarUsuario",
            data: JSON.stringify(jDados),
            dataType: "JSON",
            type: "POST",
            contentType: "Application/JSON; charset=utf-8",
            success: function () {
                modalMessage('Aviso!!', 'Cadastro Realizado com sucesso');
                LimparCampos();
                SelecionarUsuarios();
            },
            error: function () {
                modalMessage('Aviso!', 'Houve um erro');
                LimparCampos();
            }
        })
    }
}

function SelecionarUsuarios() {
    $.ajax({
        url: "Default.aspx/SelecionarUsuarios",
        dataType: "JSON",
        type: "POST",
        contentType: "Application/JSON; charset=utf-8",
        success: function (data) {
            var DataOption = data.d;
            var option = '';
            var tr = "";
            var arr = []


            $.each(DataOption, function (key, value) {
                var aux = [];

                aux = ["" + value.Id + "", "" + value.Nome + "", "" + value.Email + "", "" + value.CPF + "", "<button data-Id=" + value.Id + " type='button' onclick='Editar(this)' class='btn btn-info form-control btnEdit'>Editar <i class='glyphicon glyphicon-pencil'/></button>", "<button data-Id=" + value.Id + " type='button' onclick='Remover(this)' class='btn btn-danger form-control btnRemov'>Remover <i class='glyphicon glyphicon-trash'/></button>"]
                arr.push(aux);
            });

            $(document).ready(function () {

                if ($.fn.DataTable.isDataTable("#tblUsuarios")) {
                    $('#tblUsuarios').DataTable().clear().destroy();
                }



                $('#tblUsuarios').DataTable({
                    "language": {
                        "sEmptyTable": "Nenhum registro encontrado",
                        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sInfoThousands": ".",
                        "sLengthMenu": "_MENU_ Resultados por página",
                        "sLoadingRecords": "Carregando...",
                        "sProcessing": "Processando...",
                        "sZeroRecords": "Nenhum registro encontrado",
                        "sSearch": "Pesquisar",
                        "oPaginate": {
                            "sNext": "Próximo",
                            "sPrevious": "Anterior",
                            "sFirst": "Primeiro",
                            "sLast": "Último"
                        }

                    },
                    data: arr,
                    ordering: false,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'excel',
                            text: 'Excel',
                        },
                        {
                            extend: 'copy',
                            text: 'Copiar',
                        },
                        {
                            extend: 'print',
                            text: 'Imprimir',
                        },
                        {
                            extend: 'pdfHtml5',
                            text: 'PDF',
                            customize: function (doc) {
                                doc.styles.tableHeader.fontSize = 15;
                            },
                            exportOptions: {
                                columns: [0,1,2]

                            },
                        }
                    ],
                    columns: [
                        { title: "Id", "visible": false },
                        { title: "Nome" },
                        { title: "Email" },
                        { title: "CPF" },
                        { title: "Editar" },
                        { title: "Remover" }
                    ],

                });
            });
            console.log(arr);
        },
        error: function () {
            alert('Erro');
        }
    })
}

function Editar(obj) {
    var id = $(obj).data('id');
    var Jid = { Id: id }
    $.ajax({
        url: "Default.aspx/SelecionarPeloId",
        data: JSON.stringify(Jid),
        dataType: "JSON",
        type: "POST",
        contentType: "Application/JSON;charset=utf-8",
        success: function (data) {
            modalEdit(data.d.Id, data.d.Nome, data.d.Email, data.d.CPF);
        },
        error: function () {
            modalMessage('Atenção', 'Houve um erro');
        }

    })

}
function Remover(obj) {
    if (confirm("Deseja deletar esse usuário?")) {
        var id = $(obj).data('id');
        var jId = { Id: id }
        $.ajax({
            url: "Default.aspx/DeletarUsuario",
            type: "POST",
            dataType: "JSON",
            contentType: "Application/JSON; charset=utf-8",
            data: JSON.stringify(jId),
            success: function () {
                modalMessage('Sucesso', 'Usuário deletado com sucesso!!!');
                SelecionarUsuarios();
            },
            error: function () {
                modalMessage('Aviso', 'Ocorreu um erro');
            }

        })
    }
}


