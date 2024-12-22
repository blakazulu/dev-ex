/// <reference lib="webworker" />

addEventListener('message', ({ data }) => {
  const response = `worker response to ${data}`;
  postMessage(response);
});

let canvas: OffscreenCanvas;
let ctx: OffscreenCanvasRenderingContext2D | null;
let width: number;
let height: number;

const snowflakes: { x: number; y: number; radius: number; speed: number; angle: number }[] = [];
const maxSnowflakes = 75;

// Initialize snowflakes
const initSnowflakes = () => {
  snowflakes.length = 0; // Clear existing snowflakes
  for (let i = 0; i < maxSnowflakes; i++) {
    snowflakes.push({
      x: Math.random() * width,
      y: Math.random() * height,
      radius: Math.random() * 10 + 5,
      speed: Math.random() * 50 + 20,
      angle: Math.random() * 360,
    });
  }
};

const drawSnowflake = (x: number, y: number, radius: number, angle: number) => {
  const arms = 6;
  ctx?.save();
  ctx?.translate(x, y);
  ctx?.rotate((angle * Math.PI) / 180);
  ctx?.beginPath();

  for (let i = 0; i < arms; i++) {
    ctx?.moveTo(0, 0);
    ctx?.lineTo(0, -radius);
    ctx?.moveTo(0, -(radius * 0.6));
    ctx?.lineTo(-radius * 0.2, -(radius * 0.8));
    ctx?.moveTo(0, -(radius * 0.6));
    ctx?.lineTo(radius * 0.2, -(radius * 0.8));
    ctx?.rotate((Math.PI * 2) / arms);
  }

  ctx!.strokeStyle = 'white';
  ctx!.lineWidth = 1.5;
  ctx?.stroke();
  ctx?.restore();
};

const updateSnowflakes = (deltaTime: number) => {
  snowflakes.forEach((snowflake) => {
    snowflake.y += (snowflake.speed * deltaTime) / 1000;
    snowflake.angle += 1;

    if (snowflake.y > height) {
      snowflake.y = -snowflake.radius;
      snowflake.x = Math.random() * width;
    }
  });
};

let lastTimestamp = 0;

const animate = (timestamp: number) => {
  const deltaTime = timestamp - lastTimestamp;
  lastTimestamp = timestamp;

  ctx?.clearRect(0, 0, width, height);
  snowflakes.forEach((snowflake) => drawSnowflake(snowflake.x, snowflake.y, snowflake.radius, snowflake.angle));
  updateSnowflakes(deltaTime);

  requestAnimationFrame(animate);
};

// Handle messages from the main thread
self.onmessage = (event: MessageEvent) => {
  if (event.data.type === 'init') {
    canvas = event.data.canvas; // Initialize canvas once
    ctx = canvas.getContext('2d');

    if (!ctx) {
      console.error('Failed to get 2D context');
      return;
    }

    width = event.data.width;
    height = event.data.height;
    canvas.width = width;
    canvas.height = height;

    initSnowflakes();
    requestAnimationFrame(animate);
  } else if (event.data.type === 'resize') {
    if (!canvas) {
      console.error('Canvas not initialized');
      return;
    }

    width = event.data.width;
    height = event.data.height;
    canvas.width = width;
    canvas.height = height;

    initSnowflakes();
  }
};
