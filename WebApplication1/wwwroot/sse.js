let eventSource;
const messagesDiv = document.getElementById('messages');
const statusDiv = document.getElementById('status');
const startButton = document.getElementById('startButton');
const stopButton = document.getElementById('stopButton');

// SSE接続を開始する関数
function startSSE() {
    // 既存の接続が閉じている場合のみ新しい接続を開始
    if (!eventSource || eventSource.readyState === EventSource.CLOSED) {
        eventSource = new EventSource('/api/sse/stream');

        // メッセージ受信時の処理
        eventSource.onmessage = function (event) {
            const data = JSON.parse(event.data);
            const messageWithTimestamp = `【${data.Timestamp}】 ${data.Message}`;
            const messageElement = document.createElement('div');
            messageElement.classList.add('message');
            messageElement.innerText = messageWithTimestamp;
            messagesDiv.appendChild(messageElement);
            messagesDiv.scrollTop = messagesDiv.scrollHeight; // スクロールを最下部に
        };

        // 接続が開かれたときの処理
        eventSource.onopen = function () {
            statusDiv.innerText = 'ステータス: 接続中';
            startButton.disabled = true;
            stopButton.disabled = false;
        };

        // エラーまたは接続が閉じられたときの処理
        eventSource.onerror = function () {
            if (eventSource.readyState === EventSource.CLOSED) {
                statusDiv.innerText = 'ステータス: 切断';
                startButton.disabled = false;
                stopButton.disabled = true;
            }
        };
    }
}

// SSE接続を停止する関数
function stopSSE() {
    if (eventSource) {
        eventSource.close();
        statusDiv.innerText = 'ステータス: 切断';
        startButton.disabled = false;
        stopButton.disabled = true;
    }
}

// 初期メッセージのクリア
messagesDiv.innerHTML = '';
