import { useMemo, useState } from 'react'
import { Navigate, useLocation, useNavigate } from 'react-router-dom'
import { Button } from '../components/common/Button'
import { ErrorMessage } from '../components/common/Feedback'
import { Input } from '../components/common/Input'
import { useAuth } from '../hooks/useAuth'

interface LoginFormState {
  email: string
  password: string
}

export function LoginPage() {
  const navigate = useNavigate()
  const location = useLocation()
  const { isAuthenticated, login } = useAuth()

  const [form, setForm] = useState<LoginFormState>({
    email: 'hadson.sampaio@gmail.com',
    password: '123456',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const redirectPath = useMemo(() => {
    const state = location.state as { from?: { pathname?: string } } | null
    return state?.from?.pathname ?? '/parking'
  }, [location.state])

  if (isAuthenticated) {
    return <Navigate to="/parking" replace />
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setError(null)

    if (!form.email || !form.password) {
      setError('Informe email e senha para continuar.')
      return
    }

    setLoading(true)

    try {
      await login({ email: form.email, password: form.password })
      navigate(redirectPath, { replace: true })
    } catch (submitError) {
      if (submitError instanceof Error) {
        setError(submitError.message)
      } else {
        setError('Nao foi possivel realizar login.')
      }
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="login-page">
      <section className="login-card">
        <p className="eyebrow">Impacta GarageTrack</p>
        <h1>Entrar no sistema</h1>
        <p className="subtitle">Controle entradas, saidas e vagas do estacionamento.</p>

        <form className="stack" onSubmit={handleSubmit}>
          <Input
            id="email"
            label="Email"
            type="email"
            value={form.email}
            onChange={(event) => setForm((prev) => ({ ...prev, email: event.target.value }))}
            placeholder="seu@email.com"
            autoComplete="email"
          />

          <Input
            id="password"
            label="Senha"
            type="password"
            value={form.password}
            onChange={(event) => setForm((prev) => ({ ...prev, password: event.target.value }))}
            placeholder="********"
            autoComplete="current-password"
          />

          {error ? <ErrorMessage message={error} /> : null}

          <Button type="submit" fullWidth disabled={loading}>
            {loading ? 'Entrando...' : 'Entrar'}
          </Button>
        </form>
      </section>
    </div>
  )
}
