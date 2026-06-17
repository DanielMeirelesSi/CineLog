import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { ToastProvider } from './contexts/ToastContext'
import Header from './components/Header'
import ToastContainer from './components/ui/ToastContainer'
import Catalogo from './pages/Catalogo'
import DetalheFilme from './pages/DetalheFilme'
import DetalheSerie from './pages/DetalheSerie'
import Favoritos from './pages/Favoritos'
import Admin from './pages/Admin'

export default function App() {
  return (
    <ToastProvider>
      <BrowserRouter>
        <div className="min-h-screen bg-cinema-base text-cinema-primary">
          <Header />
          <main>
            <Routes>
              <Route path="/" element={<Catalogo />} />
              <Route path="/filmes/:id" element={<DetalheFilme />} />
              <Route path="/series/:id" element={<DetalheSerie />} />
              <Route path="/favoritos" element={<Favoritos />} />
              <Route path="/admin" element={<Admin />} />
            </Routes>
          </main>
          <footer className="border-t border-cinema-border mt-16 py-6 text-center text-cinema-muted text-xs">
            © 2025 CineLog · PUC · Programação Orientada a Objetos
          </footer>
        </div>
        <ToastContainer />
      </BrowserRouter>
    </ToastProvider>
  )
}
