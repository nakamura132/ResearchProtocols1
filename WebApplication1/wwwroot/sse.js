// Server-Sent Eventsの初期化
const eventSource = new EventSource('/api/sse/stream');

// メッセージ受信時の処理
eventSource.onmessage = function (event) {
    const data = JSON.parse(event.data);
    const messageWithTimestamp = `【${data.Timestamp}】 ${data.Message}`;
    document.getElementById('message').innerText = messageWithTimestamp;
};

// エラー時の処理
eventSource.onerror = function () {
    document.getElementById('message').innerText = 'サーバーからの接続が切断されました';
    eventSource.close();
};
