let socket;
const statusLabel = document.getElementById('statusLabel');
const urlBox = document.getElementById('url');
const connectButton = document.getElementById('connectButton');
const disconnectButton = document.getElementById('disconnectButton');
const messageBox = document.getElementById('messageBox');
const sendButton = document.getElementById('sendButton');
const clearButton = document.getElementById('clearButton');
const logTableBody = document.getElementById('logTableBody');

/// <summary>
/// WebSocketの接続状態に合わせてUIを更新する関数
/// </summary>
function updateState() {
    switch (socket.readyState) {
        case WebSocket.CLOSING:
            statusLabel.innerText = '切断中';
            setUIState(false, false, false, false, false);
            break;
        case WebSocket.CLOSED:
            statusLabel.innerText = '未接続';
            setUIState(false, false, false, true, true);
            break;
        case WebSocket.CONNECTING:
            statusLabel.innerText = '接続試行中';
            setUIState(false, false, false, false, false);
            break;
        case WebSocket.OPEN:
            statusLabel.innerText = '接続確立中';
            setUIState(true, true, true, false, false);
            break;
        default:
            statusLabel.innerText = `不明なWebSocketステータス：${socket.readyState}`;
            break;
    }
}

/// <summary>
/// UIの各要素の有効/無効状態を設定する関数
/// </summary>
function setUIState(messageBoxEnabled, sendButtonEnabled, disconnectButtonEnabled, urlBoxEnabled, connectButtonEnabled) {
    messageBox.disabled = !messageBoxEnabled;
    sendButton.disabled = !sendButtonEnabled;
    disconnectButton.disabled = !disconnectButtonEnabled;
    urlBox.disabled = !urlBoxEnabled;
    connectButton.disabled = !connectButtonEnabled;
}

/// <summary>
/// WebSocket接続を開始する関数
/// </summary>
function connectWebSocket() {
    statusLabel.innerText = '接続試行中';
    socket = new WebSocket(urlBox.value);

    socket.onopen = function () {
        updateState();
        addLogEntry('接続開始', true);
    };

    socket.onclose = function () {
        updateState();
        addLogEntry('接続終了', true);
    };

    socket.onmessage = function (event) {
        addLogEntry(event.data, false, true);
    };

    socket.onerror = function (event) {
        updateState();
        addLogEntry(`エラー: ${event.message}`, true);
    };
}

/// <summary>
/// WebSocket接続を終了する関数
/// </summary>
function disconnectWebSocket() {
    if (socket.readyState !== WebSocket.OPEN) {
        alert("ソケットは接続状態ではありません");
    } else {
        socket.close();
    }
}

/// <summary>
/// メッセージを送信する関数
/// </summary>
function sendMessage() {
    if (socket.readyState !== WebSocket.OPEN) {
        alert("ソケットは接続状態ではありません");
    } else {
        const message = messageBox.value;
        socket.send(message);
        addLogEntry(message, true, false);
    }
}

/// <summary>
/// メッセージボックスの内容をクリアする関数
/// </summary>
function clearMessage() {
    messageBox.value = '';
}

/// <summary>
/// 通信ログテーブルに新しい行を追加する関数
/// </summary>
function addLogEntry(data, isClient = false, isServer = false) {
    const row = document.createElement('tr');
    const clientCell = document.createElement('td');
    const serverCell = document.createElement('td');
    const dataCell = document.createElement('td');

    clientCell.innerText = isClient ? '〇' : '';
    serverCell.innerText = isServer ? '〇' : '';
    dataCell.innerText = data;

    row.appendChild(clientCell);
    row.appendChild(serverCell);
    row.appendChild(dataCell);

    logTableBody.appendChild(row);
}

/// <summary>
/// ページ読み込み時にURLを初期化する関数
/// </summary>
function initializeUrl() {
    const scheme = window.location.protocol === 'https:' ? 'wss' : 'ws';
    const port = window.location.port ? `:${window.location.port}` : '';
    const url = `${scheme}://${window.location.hostname}${port}/api/websocket/connect`;
    urlBox.value = url;
}

// ページ読み込み時にURLを初期化
window.onload = initializeUrl;