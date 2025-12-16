<%@ Page Language="VB" AutoEventWireup="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Demo Project - Home</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .welcome-card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 40px rgba(0,0,0,0.2);
            padding: 50px;
            text-align: center;
            max-width: 600px;
        }
        .welcome-icon {
            font-size: 80px;
            color: #667eea;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="welcome-card">
            <div class="welcome-icon">→</div>
            <h1 class="mb-4">Welcome to Demo CRUD Application</h1>
            <p class="lead text-muted mb-4">
                A clean WebForms demo with proper DAL/BLL architecture
            </p>
            <a href="Pages/DemoItems.aspx" class="btn btn-primary btn-lg">
                Go to Demo Items →
            </a>
            <hr class="my-4" />
            <div class="text-start">
                <h5>Features:</h5>
                <ul class="list-unstyled">
                    <li>• Full CRUD Operations</li>
                    <li>• DAL/BLL Separation</li>
                    <li>• Server & Client Validation</li>
                    <li>• Bootstrap 5 UI</li>
                    <li>• Parameterized SQL Queries</li>
                </ul>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

