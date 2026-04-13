import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { ErrorMessage, LoadingMessage } from '../../components/common/Feedback'
import { parkingTaxService } from '../../services/parkingTaxService'
import type { ParkingTax } from '../../types/parkingTax'
import { PARKING_TAX_TYPE_LABELS } from '../../types/parkingTax'

export function ParkingTaxListPage() {
  const navigate = useNavigate()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [taxes, setTaxes] = useState<ParkingTax[]>([])
  const [deletingId, setDeletingId] = useState<number | null>(null)

  async function loadTaxes() {
    setLoading(true)
    setError(null)

    try {
      const result = await parkingTaxService.list()
      setTaxes(result)
    } catch (loadError) {
      if (loadError instanceof Error) {
        setError(loadError.message)
      } else {
        setError('Não foi possível carregar as tarifas.')
      }
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    void loadTaxes()
  }, [])

  async function handleDelete(tax: ParkingTax) {
    const label = `${PARKING_TAX_TYPE_LABELS[tax.type]}${tax.minutes ? ` (${tax.minutes} min)` : ''}`
    if (!window.confirm(`Deseja excluir a tarifa "${label}"?`)) return

    setDeletingId(tax.id)

    try {
      await parkingTaxService.remove(tax.id)
      setTaxes((prev) => prev.filter((t) => t.id !== tax.id))
    } catch (deleteError) {
      if (deleteError instanceof Error) {
        setError(deleteError.message)
      } else {
        setError('Não foi possível excluir a tarifa.')
      }
    } finally {
      setDeletingId(null)
    }
  }

  function handleEdit(tax: ParkingTax) {
    navigate(`/parkingtax/${tax.id}/edit`, { state: { tax } })
  }

  return (
    <section className="section">
      <header className="section-header">
        <div>
          <h2>Tarifas</h2>
          <p>Configuração de tarifas do estacionamento.</p>
        </div>
        <div className="header-actions">
          <Link className="btn btn-secondary" to="/parkingtax/new">
            Nova tarifa
          </Link>
          <button className="btn btn-primary" onClick={() => void loadTaxes()} type="button">
            Atualizar
          </button>
        </div>
      </header>

      {loading ? <LoadingMessage message="Carregando tarifas..." /> : null}
      {error ? <ErrorMessage message={error} /> : null}

      {!loading && !error && taxes.length === 0 ? (
        <p className="empty-state">Nenhuma tarifa cadastrada.</p>
      ) : null}

      {!loading && taxes.length > 0 ? (
        <div className="table-card">
          <table className="data-table">
            <thead>
              <tr>
                <th>Tipo</th>
                <th>Tempo Até (min)</th>
                <th>Valor</th>
                <th aria-label="Ações" />
              </tr>
            </thead>
            <tbody>
              {taxes.map((tax) => (
                <tr key={tax.id}>
                  <td>{PARKING_TAX_TYPE_LABELS[tax.type]}</td>
                  <td>{tax.minutes ?? '-'}</td>
                  <td>
                    {tax.value.toLocaleString('pt-BR', {
                      style: 'currency',
                      currency: 'BRL',
                    })}
                  </td>
                  <td className="table-actions">
                    <button
                      className="btn btn-secondary btn-sm"
                      onClick={() => handleEdit(tax)}
                      type="button"
                    >
                      Editar
                    </button>
                    <button
                      className="btn btn-danger btn-sm"
                      onClick={() => void handleDelete(tax)}
                      disabled={deletingId === tax.id}
                      type="button"
                    >
                      {deletingId === tax.id ? 'Excluindo...' : 'Excluir'}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : null}
    </section>
  )
}
