# ProvaCSharpFront
ADICIONAR o CORS no Program.cs
(as vzs o gitignore, tira do arquivo Program.cs)

app.UseCors(
    cors => cors.AllowAnyOrigin().
        AllowAnyMethod().
        AllowAnyHeader()
);
