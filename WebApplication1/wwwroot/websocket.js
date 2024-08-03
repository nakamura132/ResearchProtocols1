let socket;
const messagesDiv = document.getElementById('messages');
const statusDiv = document.getElementById('status');
const startButton = document.getElementById('startButton');
const stopButton = document.getElementById('stopButton');

// WebSocket接続のURLを動的に生成する関数
function getWebSocketUrl() {
    // 現在のプロトコルに基づいてWebSocketのプロトコルを決定
    const protocolPrefix = (window.location.protocol === 'https:') ? 'wss://' : 'ws://';

    // ホストとAPIエンドポイントを組み合わせ
    return protocolPrefix + window.location.host + '/api/websocket/connect';
}

// WebSocket接続を開始する関数
function startWebSocket() {
    socket = new WebSocket(getWebSocketUrl());

    socket.onopen = function () {
        statusDiv.innerText = 'ステータス: 接続中';
        startButton.disabled = true;
        stopButton.disabled = false;
    };

    socket.onmessage = function (event) {
        const messageElement = document.createElement('div');
        messageElement.innerText = event.data;
        messagesDiv.appendChild(messageElement);
        messagesDiv.scrollTop = messagesDiv.scrollHeight; // 受信するたびにスクロールを最下部に
    };

    socket.onclose = function () {
        updateUI('切断', false, true);
    };
}

// WebSocket接続を終了する関数
function stopWebSocket() {
    if (socket) {
        socket.close();
        //updateUI('切断', false, true);  // 直接UIを更新
    }
}

// UIの状態を更新する共通関数
function updateUI(statusText, startDisabled, stopDisabled) {
    statusDiv.innerText = `ステータス: ${statusText}`;
    startButton.disabled = startDisabled;
    stopButton.disabled = stopDisabled;
}
