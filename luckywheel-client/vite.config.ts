import { fileURLToPath, URL } from 'node:url'
import { unlinkSync } from 'node:fs'
import { resolve } from 'node:path'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

const port = parseInt(process.env.PORT ?? "5173");

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
    {
      name: 'exclude-msw',
      closeBundle() {
        try {
          unlinkSync(resolve(__dirname, 'dist', 'mockServiceWorker.js'))
        } catch {}
      }
    },
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  server: {
    host: true,
    port,
    proxy: {
      '/api': {
        target: process.env.services__luckyrest__https__0 || process.env.services__luckyrest__http__0 || `http://localhost:${port}`,
        changeOrigin: true,
        rewrite: path => path.replace(/^\/api/, ""),
        secure: false
      }
    }
  }
})
