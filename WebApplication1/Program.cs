
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// �R���g���[���[�T�[�r�X��ǉ�
builder.Services.AddControllers();

var app = builder.Build();

// WebSocket �T�|�[�g��ǉ�
app.UseWebSockets();

// ���ۂ� WebSocket �ڑ�����������J�X�^���~�h���E�F�A��ǉ����܂�
// WebSocket �~�h���E�F�A�́A�ÓI�t�@�C���~�h���E�F�A��F�؃~�h���E�F�A�̑O�ɐݒ肷��K�v������܂��B
//app.UseMiddleware<WebSocketMiddleware>();

// �f�t�H���g�t�@�C�� (index.html) ���g�p
app.UseDefaultFiles();
// �ÓI�t�@�C�����
app.UseStaticFiles();

// �R���g���[���[���}�b�s���O
app.MapControllers();

app.Run();
