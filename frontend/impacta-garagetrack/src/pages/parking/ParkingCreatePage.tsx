import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Button } from '../../components/common/Button'
import { ErrorMessage } from '../../components/common/Feedback'
import { Input } from '../../components/common/Input'
import { parkingService } from '../../services/parkingService'
import { normalizePlate } from '../../utils/format'

interface CreateFormState {
  plate: string
  color: string
}

export function ParkingCreatePage() {
  const navigate = useNavigate()
  const [form, setForm] = useState<CreateFormState>({ plate: '', color: '' })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setError(null)

    const normalizedPlate = normalizePlate(form.plate)

    if (!normalizedPlate || !form.color.trim()) {
      setError('Informe placa e cor para cadastrar a entrada.')
      return
    }

    setLoading(true)

    try {
      const createdRecord = await parkingService.create({
        plate: normalizedPlate,
        color: form.color.trim(),
      })

      navigate(`/parking/${createdRecord.id}`)
    } catch (submitError) {
      if (submitError instanceof Error) {
        setError(submitError.message)
      } else {
        setError('Nao foi possivel cadastrar a entrada.')
      }
    } finally {
      setLoading(false)
    }
  }

  return (
    <section className="section">
      <header className="section-header">
        <div>
          <h2>Nova entrada</h2>
          <p>Cadastre um novo veiculo no estacionamento.</p>
        </div>
        <Link className="btn btn-secondary" to="/parking">
          Voltar para lista
        </Link>
      </header>

      <article className="form-card">
        <form className="stack" onSubmit={handleSubmit}>
          <Input
            id="plate"
            label="Placa"
            value={form.plate}
            onChange={(event) =>
              setForm((prev) => ({
                ...prev,
                plate: normalizePlate(event.target.value),
              }))
            }
            placeholder="ABC1D23"
            maxLength={7}
          />

          <Input
            id="color"
            label="Cor"
            value={form.color}
            onChange={(event) => setForm((prev) => ({ ...prev, color: event.target.value }))}
            placeholder="Branco"
          />

          {error ? <ErrorMessage message={error} /> : null}

          <Button type="submit" disabled={loading}>
            {loading ? 'Cadastrando...' : 'Cadastrar entrada'}
          </Button>
        </form>
      </article>
    </section>
  )
}
