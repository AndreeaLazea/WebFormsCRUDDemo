<%@ Page Language="VB" AutoEventWireup="true" CodeFile="DemoItems.aspx.vb" Inherits="DemoItems" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Demo Items CRUD</title>
    
    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" rel="stylesheet" />
    
    <style>
        body {
            background-color: #f8f9fa;
            padding-top: 20px;
        }
        .main-container {
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            padding: 30px;
            margin-bottom: 30px;
        }
        .page-header {
            border-bottom: 3px solid #0d6efd;
            padding-bottom: 15px;
            margin-bottom: 25px;
        }
        .gridview-container {
            margin-top: 20px;
        }
        .btn-action {
            margin-right: 5px;
        }
        .alert-container {
            margin-bottom: 20px;
        }
        .form-section {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 5px;
            margin-bottom: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Page Header -->
            <div class="main-container">
                <div class="page-header">
                    <h1><i class="bi bi-list-check"></i> Demo Items Management</h1>
                    <p class="text-muted">A simple CRUD demonstration with proper DAL/BLL architecture</p>
                </div>

                <!-- Alert Messages -->
                <div class="alert-container">
                    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
                        <i class="bi bi-check-circle-fill"></i>
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
                        <i class="bi bi-exclamation-triangle-fill"></i>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>
                </div>

                <!-- Insert/Edit Form -->
                <div class="form-section">
                    <h4 class="mb-3">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Add New Item"></asp:Label>
                    </h4>
                    
                    <asp:HiddenField ID="hdnEditItemID" runat="server" Value="0" />
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtTitle" class="form-label">Title <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="200" placeholder="Enter item title"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                                ControlToValidate="txtTitle" 
                                ErrorMessage="Title is required" 
                                CssClass="text-danger" 
                                Display="Dynamic"
                                ValidationGroup="InsertUpdate">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvTitle" runat="server" 
                                ControlToValidate="txtTitle" 
                                ErrorMessage="Title must be at least 3 characters" 
                                CssClass="text-danger" 
                                Display="Dynamic"
                                ClientValidationFunction="validateTitleLength"
                                OnServerValidate="ValidateTitleLength"
                                ValidationGroup="InsertUpdate">
                            </asp:CustomValidator>
                        </div>
                        
                        <div class="col-md-6 mb-3">
                            <label for="txtDescription" class="form-label">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="1000" placeholder="Enter item description" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <asp:Button ID="btnSave" runat="server" Text="Save Item" CssClass="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="InsertUpdate" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" Visible="false" />
                        </div>
                    </div>
                </div>

                <!-- GridView -->
                <div class="gridview-container">
                    <h4 class="mb-3">Items List</h4>
                    
                    <asp:GridView ID="gvItems" runat="server" 
                        AutoGenerateColumns="False" 
                        CssClass="table table-striped table-bordered table-hover"
                        DataKeyNames="ItemID"
                        OnRowEditing="gvItems_RowEditing"
                        OnRowDeleting="gvItems_RowDeleting"
                        OnRowCancelingEdit="gvItems_RowCancelingEdit"
                        OnRowUpdating="gvItems_RowUpdating"
                        EmptyDataText="No items found. Add your first item above!"
                        GridLines="None">
                        
                        <Columns>
                            <asp:BoundField DataField="ItemID" HeaderText="ID" ReadOnly="True" ItemStyle-Width="60px" ItemStyle-CssClass="align-middle" />
                            
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <strong><%# Eval("Title") %></strong>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditTitle" runat="server" Text='<%# Bind("Title") %>' CssClass="form-control" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditTitle" runat="server" 
                                        ControlToValidate="txtEditTitle" 
                                        ErrorMessage="Title is required" 
                                        CssClass="text-danger" 
                                        Display="Dynamic"
                                        ValidationGroup="EditRow">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle CssClass="align-middle" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <%# Eval("Description") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control" MaxLength="1000" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="align-middle" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:MMM dd, yyyy}" ReadOnly="True" ItemStyle-Width="130px" ItemStyle-CssClass="align-middle" />
                            
                            <asp:TemplateField HeaderText="Actions" ItemStyle-Width="180px" ItemStyle-CssClass="text-center align-middle">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning btn-action" ToolTip="Edit">
                                        <i class="bi bi-pencil-square"></i> Edit
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger btn-action" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');">
                                        <i class="bi bi-trash"></i> Delete
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success btn-action" ValidationGroup="EditRow">
                                        <i class="bi bi-check-lg"></i> Update
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelEdit" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary btn-action" CausesValidation="false">
                                        <i class="bi bi-x-lg"></i> Cancel
                                    </asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                        <HeaderStyle CssClass="table-primary" />
                        <EmptyDataRowStyle CssClass="text-center text-muted" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap 5 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    
    <!-- Client-side validation -->
    <script type="text/javascript">
        function validateTitleLength(sender, args) {
            var value = args.Value.trim();
            args.IsValid = (value.length >= 3);
        }
    </script>
</body>
</html>

