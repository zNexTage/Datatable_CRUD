<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Datatable.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <h1 style="text-align: center;">CRUD
        </h1>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <label>Nome: </label>
                <input type="text" class="form-control" id="txtNome">
            </div>
            <div class="col-md-3">
                <label>Email: </label>
                <input type="text" class="form-control" id="txtEmail">
            </div>
            <div class="col-md-3">
                <label>CPF: </label>
                <input type="text" class="form-control" id="txtCPF">
            </div>
            <div class="col-md-3">
                <label>Senha: </label>
                <input type="password" class="form-control" id="txtSenha">
            </div>
            <div class="col-md-1">
                <label>Confirmar: </label>
                <button class="btn btn-success" id="btCadastrar" onclick="CadastrarUsuario();return false;">Cadastrar</button>
            </div>
        </div>
        <h1 style="text-align:center;">Lista de usuários</h1>
        <hr />
        <br />
        <table id="tblUsuarios" class="table">
        </table>
    </div>
    <!--MODAL PARA EDITAR AS INFO--->
    <div class="modal fade" id="ModalEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-8">
                                <label>Nome: </label>
                                <input type="text" id="txtEditNome" class="form-control">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <label>Email: </label>
                                <input type="text" id="txtEditEmail" class="form-control">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <label>CPF</label>
                                <input type="text" id="txtEditCPF" class="form-control">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="btAtualizar" onclick="Atualizar()" data-dismiss="modal">Atualizar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="Scripts/JS/Default.js"></script>

    <script>
        $(document).ready(function () {
            $('#txtEditCPF').mask('000.000.000-00');
        })
        $id = 0;
        function modalEdit(Id, Nome, Email, CPF) {
            $id = Id;
            console.log($id);
            document.getElementById("txtEditNome").value = Nome;
            document.getElementById("txtEditEmail").value = Email;
            document.getElementById("txtEditCPF").value = CPF;

            $('#ModalEdit').modal('show');
        }
        function LimparModal() {
            document.getElementById("txtEditNome").value = "";
            document.getElementById("txtEditEmail").value = "";
            document.getElementById("txtEditCPF").value = "";
        }

        function Atualizar() {
            var Nome = $('#txtEditNome').val();
            var Email = $('#txtEditEmail').val();
            var CPF = $('#txtEditCPF').val();
            var Id = $id;


            if (Nome == "" || Email == "" || CPF == "") {

            }
            else {
                var jDados = { Id: Id, Nome: Nome, Email: Email, CPF: CPF }
                $.ajax({
                    url: "Default.aspx/AtualizarUsuario",
                    data: JSON.stringify(jDados),
                    dataType: "JSON",
                    type: "POST",
                    contentType: "Application/JSON; charset=utf-8",
                    success: function () {
                        LimparModal();
                        SelecionarUsuarios();
                        modalMessage('Aviso!!', 'Atualização Realizada com sucesso');
                    },
                    error: function () {
                        LimparModal();
                        SelecionarUsuarios();
                        modalMessage('Aviso!', 'Houve um erro');
                    }
                })
            }
        }
    </script>
</asp:Content>
