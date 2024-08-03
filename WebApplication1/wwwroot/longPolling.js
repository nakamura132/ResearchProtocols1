let polling = false;
let intervalId;

// 長時間ポーリング関数
async function longPoll() {
    try {
        const response = await fetch('/api/longpolling/longpoll');
        const data = await response.json();
        const messageWithTimestamp = `【${data.timestamp}】 ${data.message}`;
        document.getElementById('message').innerText = messageWithTimestamp;
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('message').innerText = 'データの取得エラー';
    }
}

// ポーリング開始関数
function startPolling() {
    if (!polling) {
        polling = true;
        document.getElementById('status').innerText = 'ステータス: ポーリング中';
        document.getElementById('startButton').disabled = true;
        document.getElementById('stopButton').disabled = false;

        longPoll();
        intervalId = setInterval(longPoll, 11000); // 11秒間隔でポーリング
    }
}

// ポーリング停止関数
function stopPolling() {
    if (polling) {
        polling = false;
        clearInterval(intervalId);
        document.getElementById('status').innerText = 'ステータス: 待機中';
        document.getElementById('startButton').disabled = false;
        document.getElementById('stopButton').disabled = true;
    }
}
