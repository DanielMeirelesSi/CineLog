/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        cinema: {
          base: '#08080F',
          surface: '#111118',
          elevated: '#1C1C28',
          border: '#2A2A3C',
          gold: '#E8B84B',
          red: '#C0392B',
          blue: '#2E86C1',
          primary: '#F0F0F5',
          muted: '#7A7A96',
        },
      },
      fontFamily: {
        display: ['"Bebas Neue"', 'cursive'],
        sans: ['Inter', 'sans-serif'],
      },
    },
  },
  plugins: [],
}
