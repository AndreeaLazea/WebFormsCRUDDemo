<%@ Page Language="VB" AutoEventWireup="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Error - Demo CRUD App</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            padding-top: 50px;
        }
        .error-container {
            max-width: 600px;
            margin: 0 auto;
            text-align: center;
            background: white;
            padding: 50px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .error-icon {
            font-size: 80px;
            color: #dc3545;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="error-container">
            <div class="error-icon">!</div>
            <h1 class="text-danger">Error</h1>
            <p class="lead text-muted my-4">
                An error has occurred. Please try again.
            </p>
            <a href="Default.aspx" class="btn btn-primary">Go to Home</a>
            <a href="Pages/DemoItems.aspx" class="btn btn-outline-secondary">Go to Demo Items</a>
        </div>
    </form>
</body>
</html>

