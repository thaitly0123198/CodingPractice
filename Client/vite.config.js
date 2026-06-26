import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 8080,
    open: true,
    proxy: {
      // Shortens fetch('/api/users') to http://localhost:5000/api/users
      '/nav': {
        target: 'http://localhost:8090', // Your backend server port
        changeOrigin: false,
        secure: true,
      }
    }
  }
})