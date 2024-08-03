var builder = WebApplication.CreateBuilder(args);

// �R���g���[���[�T�[�r�X��ǉ�
builder.Services.AddControllers();

var app = builder.Build();

// �f�t�H���g�t�@�C�� (index.html) ���g�p
app.UseDefaultFiles();
// �ÓI�t�@�C�����
app.UseStaticFiles();
// WebSocket�~�h���E�F�A��L����
app.UseWebSockets();
// �R���g���[���[���}�b�s���O
app.MapControllers();

app.Run();
