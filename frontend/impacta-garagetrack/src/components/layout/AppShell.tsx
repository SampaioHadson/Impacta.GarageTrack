import { NavLink, Outlet, useNavigate } from 'react-router-dom'
import { Button } from '../common/Button'
import { useAuth } from '../../hooks/useAuth'

export function AppShell() {
  const navigate = useNavigate()
  const { user, logout } = useAuth()

  function handleLogout() {
    logout()
    navigate('/login', { replace: true })
  }

  return (
    <div className="app-shell">
      <header className="app-header">
        <div>
          <p className="eyebrow">Impacta GarageTrack</p>
          <h1>Controle de Estacionamento</h1>
        </div>

        <div className="header-user">
          <span>
            {user?.name} | {user?.companyName}
          </span>
          <Button onClick={handleLogout} variant="secondary">
            Sair
          </Button>
        </div>
      </header>

      <nav className="app-nav">
        <NavLink to="/parking">Estacionamentos</NavLink>
        <NavLink to="/parking/new">Cadastrar Entrada</NavLink>
        <NavLink to="/parkingtax">Tarifas</NavLink>
      </nav>

      <main className="app-main">
        <Outlet />
      </main>
    </div>
  )
}
