var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// �f�t�H���g�t�@�C�� (index.html) ���g�p
app.UseDefaultFiles();
// �ÓI�t�@�C�����
app.UseStaticFiles();

app.Run();
